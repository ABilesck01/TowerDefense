using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/New Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public SerializableDictionary<StatEnum, float> statsToAply;
    public bool isPercentage;
}
