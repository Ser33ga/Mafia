using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SaveSystem
{
    public const int CurrentVersion = 1;

    private const string FileName = "saves.json";

    private readonly string _filePath;
    private readonly List<ISavable> _savables = new();
    private readonly IReadOnlyList<ISaveMigration> _migrations;
    private readonly JsonSerializer _serializer;

    private JObject _data = new();

    public SaveSystem(IEnumerable<ISaveMigration> migrations = null,
                      IEnumerable<JsonConverter> extraConverters = null)
    {
        _filePath = Path.Combine(Application.persistentDataPath, FileName);
        _migrations = (migrations ?? Enumerable.Empty<ISaveMigration>())
            .OrderBy(m => m.FromVersion).ToArray();

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new Vector2Converter());
        settings.Converters.Add(new Vector3Converter());
        settings.Converters.Add(new QuaternionConverter());
        settings.Converters.Add(new ColorConverter());
        if (extraConverters != null)
            foreach (var c in extraConverters)
                settings.Converters.Add(c);

        _serializer = JsonSerializer.Create(settings);
    }


    public void Register(ISavable savable)
    {
        if (_savables.Any(s => s.SaveKey == savable.SaveKey))
            Debug.LogError($"[SaveSystem] Дублирующийся SaveKey '{savable.SaveKey}'");
        _savables.Add(savable);
    }

    public void Unregister(ISavable savable) => _savables.Remove(savable);


    public void SaveToSaves<T>(string key, T value)
        => _data[key] = value == null ? JValue.CreateNull() : JToken.FromObject(value, _serializer);


    public T LoadFromSaves<T>(string key, T defaultValue = default)
    {
        if (_data.TryGetValue(key, out var token) && token.Type != JTokenType.Null)
        {
            try
            {
                return token.ToObject<T>(_serializer);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Битые данные по ключу '{key}': {e.Message}");
            }
        }
        return defaultValue;
    }

    public bool HasKey(string key) => _data.ContainsKey(key);
    public void DeleteKey(string key) => _data.Remove(key);
    public void SaveAll()
    {
        foreach (var s in _savables) s.Save(this);
        WriteFile();
    }

    public void LoadAll()
    {
        ReadFile();
        foreach (var s in _savables) s.Load(this);
    }

    public void ResetToDefaults(ISavable savable)
    {
        _data.Remove(savable.SaveKey);
        savable.Load(this);
        WriteFile();
    }


    public void ResetToDefaults()
    {
        _data = new JObject();
        if (File.Exists(_filePath)) File.Delete(_filePath);
        foreach (var s in _savables) s.Load(this);
    }
    private void WriteFile()
    {
        var file = new JObject
        {
            ["version"] = CurrentVersion,
            ["data"] = _data
        };

        var tmp = _filePath + ".tmp";
        File.WriteAllText(tmp, file.ToString(Formatting.Indented));
        if (File.Exists(_filePath)) File.Delete(_filePath);
        File.Move(tmp, _filePath);
    }

    private void ReadFile()
    {
        _data = new JObject();
        if (!File.Exists(_filePath)) return;

        try
        {
            var file = JObject.Parse(File.ReadAllText(_filePath));
            int version = file.Value<int?>("version") ?? 1;
            var data = file["data"] as JObject ?? new JObject();
            _data = Migrate(data, version);
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Файл повреждён, стартуем с дефолтов: {e.Message}");
        }
    }

    private JObject Migrate(JObject data, int fromVersion)
    {
        if (fromVersion > CurrentVersion)
        {
            Debug.LogWarning("[SaveSystem] Сейв новее билда, грузим как есть");
            return data;
        }

        for (int v = fromVersion; v < CurrentVersion; v++)
        {
            var migration = _migrations.FirstOrDefault(m => m.FromVersion == v);
            if (migration == null)
            {
                Debug.LogError($"[SaveSystem] Нет миграции с версии {v}, сброс к дефолтам");
                return new JObject();
            }
            migration.Migrate(data);
        }
        return data;
    }
}
