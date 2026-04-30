using UnityEngine;

public class Backstabber : AgentTrait
{
    public Backstabber(int startingLevel, float expLevelUp) : base("Backstabber", startingLevel, expLevelUp)
    {

    }

    public override int DamageModifier(DamageContext damageContext)
    {
        if (damageContext.Defender.agent == null)
        {
            return 0;
        }
        //Find if character is behind target agent
        //If yes, return modifier
        Agent attacker = damageContext.Attacker;
        Agent defender = damageContext.Defender.agent;

        if (defender.agentFacingDirection == Direction.Down && attacker.gridPos.y == defender.gridPos.y+1)
        {
            return 5;
        }
        if (defender.agentFacingDirection == Direction.Right && attacker.gridPos.x == defender.gridPos.x - 1)
        {
            return 5;
        }
        if (defender.agentFacingDirection == Direction.Up && attacker.gridPos.y == defender.gridPos.y - 1)
        {
            return 5;
        }
        if (defender.agentFacingDirection == Direction.Left && attacker.gridPos.y == defender.gridPos.x + 1)
        {
            return 5;
        }

        //if no return 0
        return 0;
    }

    public override int StatModifier(Stat stat)
    {
        return 0;
    }

    public override void TraitLevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override void TraitUniqueLevelUp()
    {
        throw new System.NotImplementedException();
    }

    
}
