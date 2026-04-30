using UnityEngine;

public class FormidableSolider : AgentTrait
{
    public FormidableSolider(int startingLevel, float expLevelUp) : base("Formidable Solider", startingLevel, expLevelUp)
    {

    }

    public override int DamageModifier(DamageContext damageContext)
    {
        return 0;
    }

    public override int StatModifier(Stat stat)
    {
        Debug.Log("Giving Agent Extra Strength and Consititution");

        if (stat.StatName == "Strength")
        {
            return 5;
        }
        else if (stat.StatName == "Consititution")
        {
            return 5;
        }
        else
        {
            return 0;
        }
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
