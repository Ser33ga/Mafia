using System;
public class RoleAction
{
    public Player forwarder;
    public Player receiver;
    public Action<Player,Player> act;
}