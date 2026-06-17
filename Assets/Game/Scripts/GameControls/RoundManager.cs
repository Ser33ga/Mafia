using System.Collections.Generic;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    [Header("Game Setup")]
    public List<Role> executingRoleOrder = new();
    public List<Player> players = new();

    public void CreateGame(List<Role> roleOrder, List<Player> playerList)
    { 
        executingRoleOrder = roleOrder;
        players = playerList;
    }
}