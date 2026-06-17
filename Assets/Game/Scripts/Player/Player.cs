using System.Collections.Generic;

public class Player
{
    public List<Player> playersHere;
    public Role role;
    public string id;
    public string name;
    public string description;
    public Player(string _id, string _name)
    {
        id=_id;
        name=_name;
    }
}