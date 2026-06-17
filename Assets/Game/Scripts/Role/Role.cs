using UnityEngine;

public abstract class Role
{
    public int additionalNightLives;
    public bool isDead;
    public bool isCancelledTurn;      
    public bool isSheriffCheck;        
    public string debug;              
    public bool isGoingOnNight;
    public bool hasGone;
    public abstract void Act(Player fromPlayer, Player toPlayer);
    public abstract void OnDeath();
    public abstract void GetDamage();
}