using System.Collections.Generic;
using System;
using System.Linq;
public class ActionManager
{
    //Inject
    //private List<Role> executingRoleOrder;
    public GameManager gameManager;
    //temp field
    public string cancelledDebug;
    public List<RoleAction> actions=new List<RoleAction>();

    public void AddInteraction(RoleAction action)
    {
        actions.Add(action);
    }
    /*
    public static void SortActionsByRoleOrder(List<RoleAction> actions, List<Role> executingRoleOrder)
    {
        if (actions == null || executingRoleOrder == null || executingRoleOrder.Count == 0)
            return;

        var roleOrderDict = executingRoleOrder
            .Select((role, index) => new { role, index })
            .ToDictionary(x => x.role, x => x.index);

        actions.Sort((a, b) =>
        {
            int orderA = roleOrderDict.TryGetValue(a.forwarder.role, out int idxA) ? idxA : int.MaxValue;
            int orderB = roleOrderDict.TryGetValue(b.forwarder.role, out int idxB) ? idxB : int.MaxValue;
            return orderA.CompareTo(orderB);
        });
    }*/
    public void ApplyInteraction()
    {
        RoleAction action=actions[actions.Count-1];
        if (!action.forwarder.role.isCancelledTurn && !action.forwarder.role.isDead)
            {
                action.act(action.forwarder, action.receiver);
                if (action.forwarder.role.debug != null)
                {
                    InvokeDebug(action.forwarder.role.debug);
                }
            }
            else
            {
                InvokeDebug(cancelledDebug);
            }
    }
    /*public void GetInteractionsResults()
    {
        SortActionsByRoleOrder(actions,gameManager.executingRoleOrder);
        foreach (RoleAction action in actions)
        {
            if (!action.forwarder.role.isCancelledTurn && !action.forwarder.role.isDead)
            {
                action.act(action.forwarder, action.receiver);
                if (action.forwarder.role.debug != null)
                {
                    InvokeDebug(action.forwarder.role.debug);
                }
            }
            else
            {
                InvokeDebug(cancelledDebug);
            }
        }
    }*/
    public void InvokeDebug(string debug)
    {
        
    }
}