using UnityEngine;

public class FormidableSolider : AgentTrait
{
    public FormidableSolider(string name, int startingLevel, float expLevelUp) : base(name, startingLevel, expLevelUp)
    {

    }



    public override void Trait()
    {
        Debug.Log("Giving Agent Extra Strength and Consitition");
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
