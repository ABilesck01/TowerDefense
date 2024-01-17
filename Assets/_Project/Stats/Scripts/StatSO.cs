using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/New Stat")]
public class StatSO : ScriptableObject
{
    [SerializeField, Tooltip("stats that are single for each entity instancianted")] private SerializableDictionary<StatEnum, float> instanceStats;
    [SerializeField, Tooltip("stats that are shared for each entity instancianted")] private SerializableDictionary<StatEnum, float> stats;

    public SerializableDictionary<StatEnum, float> InstanceStats { get => instanceStats; }

    public float GetValue(StatEnum stat)
    {
        if(stats.TryGetValue(stat, out var value)) 
            return value;
        else if(instanceStats.TryGetValue(stat, out float instanceValue)) 
            return instanceValue;

        Debug.LogWarning($"The stat {this.name} has not the stat {stat}.");
        return 0f;
    }
}
