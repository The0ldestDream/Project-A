using UnityEngine;
using System.Collections.Generic;
public class AgentCreator
{
    //Every possible race
    List<AgentRace> AllRaces = new List<AgentRace> { new Human() };
    //Races that would only appear as enemies
    List<AgentRace> EnemyRaces = new List<AgentRace> { new Human() };
    //Races that can be played/recruited by the player
    List<AgentRace> PlayableRaces = new List<AgentRace> { new Human() };

    List<AgentClass> AllClasses = new List<AgentClass> { new Fighter(1,0) };




}
