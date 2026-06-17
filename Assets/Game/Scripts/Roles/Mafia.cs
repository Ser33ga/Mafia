public class Mafia : Role
{
    public override void Act(Player fromPlayer, Player toPlayer)
    {
        if (isGoingOnNight)
        {
            hasGone=true;
            toPlayer.playersHere.Add(fromPlayer);
        }
        if (toPlayer.playersHere.Count == 0)
        {
           toPlayer.role.GetDamage(); 
        }
        else
        {
            toPlayer.playersHere[toPlayer.playersHere.Count-1].role.GetDamage();
        }
    }

    public override void OnDeath()
    {
        isDead=true;
    }
    public override void GetDamage()
    {
        if (additionalNightLives == 0)
        {
            OnDeath();
        } else
        {
            additionalNightLives-=1;
        }
    }
}