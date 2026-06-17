public class Sheriff : Role
{
    public override void Act(Player fromPlayer, Player toPlayer)
    {
        if (isGoingOnNight)
        {
            hasGone=true;
            toPlayer.playersHere.Add(fromPlayer);
        }
        if (toPlayer.role.isSheriffCheck)
        {
            debug="red";
        }
        else
        {
            debug="grey";
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