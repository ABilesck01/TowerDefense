using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public float unlockCost;
    public float upgradeCost;
    public Sprite icon;
    public BaseUnit unitInWorld;
}
