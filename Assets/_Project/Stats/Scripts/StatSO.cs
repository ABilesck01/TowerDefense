using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/New Stat")]
public class StatSO : ScriptableObject
{
    [SerializeField, Tooltip("stats that are shared for each entity instancianted")] private SerializableDictionary<StatEnum, float> stats;
    [SerializeField] private List<UpgradeSO> upgrades;
    public float GetValue(StatEnum stat)
    {
        if(stats.TryGetValue(stat, out var value)) 
            return GetUpgradedValue(stat, value);

        Debug.LogWarning($"The stat {this.name} has not the stat {stat}.");
        return 0f;
    }

    public void AddUpgrade(UpgradeSO upgrade)
    {
        upgrades.Add(upgrade);
    }

    private float GetUpgradedValue(StatEnum stat, float baseValue)
    {
        foreach (var upgrade in upgrades)
        {
            if (!upgrade.statsToAply.TryGetValue(stat, out float upgradeValue))
                continue;
            if (upgrade.isPercentage)
                baseValue *= (upgradeValue / 100f) + 1f;
            else
                baseValue += upgradeValue;
        }

        return baseValue;
    }

    public int GetUnitLevel()
    {
        return upgrades.Count + 1;
    }

    public void ClearUpgrades()
    {
        upgrades.Clear();
    }
}
