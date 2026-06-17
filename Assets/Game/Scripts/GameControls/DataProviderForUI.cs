using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using VContainer;
public class DataProviderForUI
{
    [Inject]public RolesOrderData rolesOrderData;
    [Inject]public RolesInGameData rolesInGameData;
    [Inject]public PlayersData playersData;
    [Inject] SaveSystem saveSystem;
    public System.Action OnPlayersChanged;
    public System.Action OnRoleInGameChanged;
    public List<Player> players=>playersData.players;
    public List<RoleInGame> rolesInGame=>rolesInGameData.rolesInGame;
    public List<RoleOrder> rolesOrder=>rolesOrderData.rolesOrder;
    public void RemoveFromPlayers(Player player)
    {
        players.Remove(player);
        OnPlayersChanged?.Invoke();
    }
    public void AddEmptyForPlayers()
    {
        players.Add(new Player("",""));
        OnPlayersChanged?.Invoke();
    }
    public void ResetPlayers()
    {
        saveSystem.ResetToDefaults(playersData);
        playersData.Load(saveSystem);
    }
    public void SavePlayers()
    {
        playersData.Save(saveSystem);
        saveSystem.SaveAll();
    }
    public void SaveRolesInGame()
    {
        rolesInGameData.Save(saveSystem);
        saveSystem.SaveAll();
    }
    public void ResetRolesInGame()
    {
        saveSystem.ResetToDefaults(rolesInGameData);
        rolesInGameData.Load(saveSystem);
    }
    public void SaveRolesOrder()
    {
        rolesOrderData.Save(saveSystem);
        saveSystem.SaveAll();
    }
    public void ResetRolesOrder()
    {
        saveSystem.ResetToDefaults(rolesOrderData);
        rolesOrderData.Load(saveSystem);
    }
}