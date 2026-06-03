using UnityEngine;
using System.Collections.Generic;
public class Backstabber : AgentTrait
{
    public Backstabber(int startingLevel, float expLevelUp) : base("Backstabber", startingLevel, expLevelUp)
    {

    }

    public override List<Contribution> DamageModifier(DamageContext damageContext)
    {
        if (damageContext.Defender.agent == null)
        {
            return null;
        }
        List<Contribution> contributions = new List<Contribution>();

        //Find if character is behind target agent
        //If yes, return modifier
        Agent attacker = damageContext.Attacker;
        Agent defender = damageContext.Defender.agent;

        float damageBuff = 0.3f;

        Contribution c1 = new Contribution(DamageType.Piercing, damageBuff, ModifierType.Multiplier);
        Contribution c2 = new Contribution(DamageType.Bludgeoning, damageBuff, ModifierType.Multiplier);
        Contribution c3 = new Contribution(DamageType.Slashing, damageBuff, ModifierType.Multiplier);

        contributions.Add(c1);
        contributions.Add(c2);
        contributions.Add(c3);

        if (defender.agentFacingDirection == Direction.Down && attacker.gridPos.y == defender.gridPos.y+1)
        {
            return contributions;
        }
        if (defender.agentFacingDirection == Direction.Right && attacker.gridPos.x == defender.gridPos.x - 1)
        {
            return contributions;
        }
        if (defender.agentFacingDirection == Direction.Up && attacker.gridPos.y == defender.gridPos.y - 1)
        {
            return contributions;
        }
        if (defender.agentFacingDirection == Direction.Left && attacker.gridPos.y == defender.gridPos.x + 1)
        {
            return contributions;
        }

        //if no return 0
        List<Contribution> eContributions = new List<Contribution>();
        return eContributions;
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
