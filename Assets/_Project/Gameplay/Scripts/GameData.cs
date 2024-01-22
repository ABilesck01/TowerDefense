using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	[SerializeField] private List<UnitData> ownedUnits;
	[SerializeField] private float gold;
	
	
	public List<UnitData> OwnedUnits
	{
		get { return ownedUnits; }
		set { ownedUnits = value; }
	}

	public float Gold
	{
		get { return gold; }
		set { gold = value; }
	}

}
