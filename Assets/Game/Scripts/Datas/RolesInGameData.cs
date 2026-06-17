using System.Collections.Generic;
[System.Serializable]
public class RolesInGameData:ISavable
{
    public List<RoleInGame> rolesInGame=new List<RoleInGame>();
    public string SaveKey=>"RolesInGame";
    public void Save(SaveSystem saves)
        => saves.SaveToSaves(SaveKey, new RolesInGame { rolesInGame=rolesInGame});

    public void Load(SaveSystem saves)
    {
        var s = saves.LoadFromSaves(SaveKey, new RolesInGame { rolesInGame=new List<RoleInGame>{new RoleInGame(typeof(Peaceful),true,0, "", "Peaceful"),new RoleInGame(typeof(Maniak),true,0,"", "Maniak")} }); // дефолты задаются тут
        rolesInGame.Clear();
        foreach (var p in s.rolesInGame)
            rolesInGame.Add(p);
    }
    public class RolesInGame
    {
        public List<RoleInGame> rolesInGame;
    }
}