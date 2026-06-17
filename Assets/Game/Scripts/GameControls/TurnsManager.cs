using System.Collections.Generic;
public class TurnsManager
{
    public List<TurnSnapshot> snapshots;
}
public class TurnSnapshot
{
    public Dictionary<Player, RoleAction> turnCondition;
}