using UnityEngine;
using System;
using System.Collections.Generic;
public class EncounterSystem
{

    private Spawner spawner;

    public EncounterSystem(Spawner AgentSpawner)
    {
        spawner = AgentSpawner;
    }


    public void StartEncounter(Room RoomEncounter)
    {
        EncounterDescription description = MakeAgentDescriptions(RoomEncounter.TypeOfRoom);


        foreach (AgentDescription agentDescription in description.descriptions)
        {
            spawner.SpawnAgent(RoomEncounter, agentDescription);
        }
    }
    

    public EncounterDescription MakeAgentDescriptions(RoomType roomType)
    {
        EncounterDescription encounter = new EncounterDescription();

        int numberOfAgents = UnityEngine.Random.Range(1,2);

        Array classtypes = Enum.GetValues(typeof(ClassType));
            


        switch (roomType)
        {
            case RoomType.NormalRoom:
                for (int i = 0; i < numberOfAgents; i++)
                {
                    int randomIndex = UnityEngine.Random.Range(0, classtypes.Length - 1);
                    ClassType randomClass = (ClassType)classtypes.GetValue(randomIndex);

                    AgentDescription agentDescription = new AgentDescription(RaceType.Human, randomClass, AgentAlignment.Enemy);

                    encounter.descriptions.Add(agentDescription);
                }
                break;
            case RoomType.BossRoom:
                AgentDescription bossDescription = new AgentDescription(RaceType.Human, ClassType.Fighter, AgentAlignment.Enemy);
                Debug.Log("BOSS HAS BEEN GENERATED");
                bossDescription.ClassLevel = 5;

                encounter.descriptions.Add(bossDescription);
                break;

        }






        return encounter;
    }

}
