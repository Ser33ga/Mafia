using System;

public class RoleInGame
{
    public Type role;
    public bool isInGame;
    public int amount;
    public string description;
    public string name;
    public RoleInGame(Type _role, bool _isInGame, int _amount, string _desk, string _name)
    {
        role=_role;
        isInGame=_isInGame;
        amount=_amount;
        description=_desk;
        name=_name;
    }
}