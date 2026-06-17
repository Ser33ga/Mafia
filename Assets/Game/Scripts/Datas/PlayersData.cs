using System.Collections.Generic;
[System.Serializable]
public class PlayersData:ISavable
{
    public List<Player> players=new List<Player>();
    public string SaveKey=>"Players";
    public void Save(SaveSystem saves)
        => saves.SaveToSaves(SaveKey, new Players { players=players});

    public void Load(SaveSystem saves)
    {
        var s = saves.LoadFromSaves(SaveKey, new Players { players = new List<Player>() });
        players.Clear();
        foreach (var p in s.players)
            players.Add(p);
    }
    public class Players
    {
        public List<Player> players;
    }
}