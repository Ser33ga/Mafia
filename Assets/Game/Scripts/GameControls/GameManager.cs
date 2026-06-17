using System.Collections.Generic;
using VContainer;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameManager
{
    [Inject] DataProviderForUI dataProviderForUI;

    public bool CanLaunchGame()
    {
        var RolesAmount=0;
        foreach(RoleInGame roleInGame in dataProviderForUI.rolesInGame)
        {
            if (roleInGame.isInGame)
            {
                RolesAmount+=roleInGame.amount;
            }
        }
        if (dataProviderForUI.players.Count == RolesAmount)
        {

            return true;
        }
        return false;
    }
    public void LaunchGame()
    {
        AssignRolesToPlayers();
    }
    public void AssignRolesToPlayers()
    {
        List<Type> rolePool = new List<Type>();

        foreach (RoleInGame rig in dataProviderForUI.rolesInGame)
        {
            if (rig.isInGame && rig.amount > 0 && rig.role != null)
            {
                for (int i = 0; i < rig.amount; i++)
                {
                    rolePool.Add(rig.role);
                }
            }
        }
        for (int i = rolePool.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Type temp = rolePool[i];
            rolePool[i] = rolePool[j];
            rolePool[j] = temp;
        }

        for (int i = 0; i < dataProviderForUI.players.Count; i++)
        {
            Type roleType = rolePool[i];
            Role instance = (Role)Activator.CreateInstance(roleType);
            dataProviderForUI.players[i].role = instance;

            RoleInGame source = dataProviderForUI.rolesInGame.FirstOrDefault(r => r.role == roleType);
            if (source != null)
            {
                instance.debug = source.name;
            }
        }
    }
}