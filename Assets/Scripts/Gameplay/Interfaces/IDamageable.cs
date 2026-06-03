using UnityEngine;

public interface IDamageable
{
    void DealDamage(DamageInfo context);

    DamageInfo ResolveDamage(DamageInfo context);

}
