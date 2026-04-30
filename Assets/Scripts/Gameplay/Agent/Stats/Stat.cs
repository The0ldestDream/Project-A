using UnityEngine;

public abstract class Stat
{

    public string StatName;

    //Max Value is the actual value of this agent's stat (isn't affected by buffs or debuffs)
    public int baseValue;
    //Current Value is the current value which is affected by conditions
    public int finalValue;
    //The lowest value that the stat will allow 
    public int minValue;

    public Stat(string _StatName, int _value, int _minValue)
    {
        StatName = _StatName;
        baseValue = _value;
        minValue = _minValue;

        finalValue = baseValue;


    }



    //Used when stat is changed permanently
    public void SetValue(int newValue)
    {
        
        if (newValue > minValue)
        {
            baseValue = newValue;

            if (finalValue == baseValue)
            {
                finalValue = baseValue;
            }
        }
    }

    //Used when stat is affected temporarily
    public void AdjustValue(int amount)
    {
        int valueCheck = finalValue + amount;
        if (valueCheck >= minValue)
        {
            finalValue = valueCheck;
        }
    }


    public void ResetToMax()
    {
        finalValue = baseValue;
    }
    public void ResetToMin()
    {
        finalValue = minValue;
    }

}
