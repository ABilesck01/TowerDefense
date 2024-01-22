using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Units/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public float unlockCost;
    public float upgradeCost;
    public Sprite icon;
    public BaseUnit unitInWorld;
    public UpgradeSO[] possibleUpgrades;

    public StatSO GetStatSO() => unitInWorld.GetStat();

    public void Upgrade()
    {
        UpgradeSO chosenUpgrade = possibleUpgrades[Random.Range(0, possibleUpgrades.Length)];
        GetStatSO().AddUpgrade(chosenUpgrade);
    }

    public void ClearUpgrades()
    {
        GetStatSO().ClearUpgrades();
    }
}
