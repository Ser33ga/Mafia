using System.Collections.Generic;
[System.Serializable]
public class RolesOrderData: ISavable
{
    public List<RoleOrder> rolesOrder=new List<RoleOrder>();
    public string SaveKey=>"RolesOrder";
    public void Save(SaveSystem saves)
        => saves.SaveToSaves(SaveKey, new RolesInGame { rolesOrder=rolesOrder});

    public void Load(SaveSystem saves)
    {
        var s = saves.LoadFromSaves(SaveKey, new RolesInGame { rolesOrder=new List<RoleOrder>{new RoleOrder(new RoleInGame(typeof(Peaceful),true,0, "", "Peaceful"),1),new RoleOrder(new RoleInGame(typeof(Maniak),true,0,"", "Maniak"),2)} }); // дефолты задаются тут
        rolesOrder.Clear();
        foreach (var p in s.rolesOrder)
            rolesOrder.Add(p);
    }
    public class RolesInGame
    {
        public List<RoleOrder> rolesOrder;
    }
}