using System;

public class RoleOrder
{
    public RoleInGame role;
    public int priority;
    public RoleOrder(RoleInGame _role, int _priority)
    {
        role=_role;
        priority=_priority;
    }
}