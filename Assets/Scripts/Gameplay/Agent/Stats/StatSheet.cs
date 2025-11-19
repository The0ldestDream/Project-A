using UnityEngine;
using System.Collections.Generic;
public class StatSheet
{

    List<Stat> AgentStatSheet = new List<Stat>();
    int minValue = 7;

    public StatSheet(int StrengthValue, int ConsititutionValue, int IntelligenceValue, int DexterityValue)
    {
        

        Strength strength = new Strength("", StrengthValue, minValue);
        AgentStatSheet.Add(strength);
        Consititution consititution = new Consititution("", ConsititutionValue, minValue);
        AgentStatSheet.Add(consititution);
        Intelligence intelligence = new Intelligence("", IntelligenceValue, minValue);
        AgentStatSheet.Add(intelligence);
        Dexterity dexterity = new Dexterity("", DexterityValue, minValue);
        AgentStatSheet.Add(dexterity);


    }


    public void StatChange(Stat stat, int newValue)
    {
        stat.SetValue(newValue);
    }

    public void StatAdjust(Stat stat, int adjustAmount)
    {
        stat.AdjustValue(adjustAmount);
    }

    public void StatResetToMax(Stat stat)
    {
        stat.ResetToMax();
    }
    public void StatResetToMin(Stat stat)
    {
        stat.ResetToMin();
    }
}
