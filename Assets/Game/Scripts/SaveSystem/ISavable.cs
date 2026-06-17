public interface ISavable
{
    string SaveKey { get; }
    void Save(SaveSystem saves);
    void Load(SaveSystem saves);
}
