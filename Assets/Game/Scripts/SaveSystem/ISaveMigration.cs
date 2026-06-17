using Newtonsoft.Json.Linq;

public interface ISaveMigration
{
    int FromVersion { get; }
    void Migrate(JObject data);
}
