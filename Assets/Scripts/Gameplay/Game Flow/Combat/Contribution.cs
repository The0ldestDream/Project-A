using UnityEngine;

public class Contribution
{
    public DamageType damageType;
    public float value;
    public ModifierType modifierType;

    public Contribution(DamageType dT, float V, ModifierType mT)
    {
        damageType = dT;
        value = V;
        modifierType = mT;
    }
}
