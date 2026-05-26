using UnityEngine;
using System.Collections.Generic;

public class DamageInfo
{
    //Source?
    //Damage Dealt

    public Agent Attacker;

    public Dictionary<DamageType, int> DamageNumbers = new Dictionary<DamageType, int> {



        [DamageType.Piercing] = 0,
        [DamageType.Bludgeoning] = 0,
        [DamageType.Slashing] = 0,
        [DamageType.Fire] = 0,
        [DamageType.Cold] = 0,
        [DamageType.Lightning] = 0,

    };





    public int DamageAmount;

}
