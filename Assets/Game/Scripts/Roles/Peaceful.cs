public class Peaceful : Role
{
    public override void Act(Player fromPlayer, Player toPlayer)
    {
        
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