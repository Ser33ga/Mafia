using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Newtonsoft не умеет Unity-типы из коробки: у Vector3 свойство normalized
// возвращает новый Vector3 -> self-referencing loop. Пишем их вручную.

public class Vector2Converter : JsonConverter<Vector2>
{
    public override void WriteJson(JsonWriter writer, Vector2 v, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x"); writer.WriteValue(v.x);
        writer.WritePropertyName("y"); writer.WriteValue(v.y);
        writer.WriteEndObject();
    }

    public override Vector2 ReadJson(JsonReader reader, Type type, Vector2 existing, bool hasExisting, JsonSerializer serializer)
    {
        var o = JObject.Load(reader);
        return new Vector2(o.Value<float>("x"), o.Value<float>("y"));
    }
}

public class Vector3Converter : JsonConverter<Vector3>
{
    public override void WriteJson(JsonWriter writer, Vector3 v, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x"); writer.WriteValue(v.x);
        writer.WritePropertyName("y"); writer.WriteValue(v.y);
        writer.WritePropertyName("z"); writer.WriteValue(v.z);
        writer.WriteEndObject();
    }

    public override Vector3 ReadJson(JsonReader reader, Type type, Vector3 existing, bool hasExisting, JsonSerializer serializer)
    {
        var o = JObject.Load(reader);
        return new Vector3(o.Value<float>("x"), o.Value<float>("y"), o.Value<float>("z"));
    }
}

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override void WriteJson(JsonWriter writer, Quaternion q, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x"); writer.WriteValue(q.x);
        writer.WritePropertyName("y"); writer.WriteValue(q.y);
        writer.WritePropertyName("z"); writer.WriteValue(q.z);
        writer.WritePropertyName("w"); writer.WriteValue(q.w);
        writer.WriteEndObject();
    }

    public override Quaternion ReadJson(JsonReader reader, Type type, Quaternion existing, bool hasExisting, JsonSerializer serializer)
    {
        var o = JObject.Load(reader);
        return new Quaternion(o.Value<float>("x"), o.Value<float>("y"), o.Value<float>("z"), o.Value<float>("w"));
    }
}

public class ColorConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color c, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("r"); writer.WriteValue(c.r);
        writer.WritePropertyName("g"); writer.WriteValue(c.g);
        writer.WritePropertyName("b"); writer.WriteValue(c.b);
        writer.WritePropertyName("a"); writer.WriteValue(c.a);
        writer.WriteEndObject();
    }

    public override Color ReadJson(JsonReader reader, Type type, Color existing, bool hasExisting, JsonSerializer serializer)
    {
        var o = JObject.Load(reader);
        return new Color(o.Value<float>("r"), o.Value<float>("g"), o.Value<float>("b"), o.Value<float>("a"));
    }
}
