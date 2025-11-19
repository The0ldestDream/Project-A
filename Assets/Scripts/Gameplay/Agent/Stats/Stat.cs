using UnityEngine;

public abstract class Stat
{

    public string StatName;

    //Max Value is the actual value of this agent's stat (isn't affected by buffs or debuffs)
    public int maxValue;
    //Current Value is the current value which is affected by conditions
    public int currentValue;
    //The lowest value that the stat will allow 
    public int minValue;

    public Stat(string _StatName, int _value, int _minValue)
    {
        StatName = _StatName;
        maxValue = _value;
        minValue = _minValue;

        currentValue = maxValue;


    }



    //Used when stat is changed permanently
    public void SetValue(int newValue)
    {
        
        if (newValue > minValue)
        {
            maxValue = newValue;

            if (currentValue == maxValue)
            {
                currentValue = maxValue;
            }
        }
    }

    //Used when stat is affected temporarily
    public void AdjustValue(int amount)
    {
        int valueCheck = currentValue + amount;
        if (valueCheck >= minValue)
        {
            currentValue = valueCheck;
        }
    }


    public void ResetToMax()
    {
        currentValue = maxValue;
    }
    public void ResetToMin()
    {
        currentValue = minValue;
    }

}
