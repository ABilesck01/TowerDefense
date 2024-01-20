using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitManager : MonoBehaviour
{
	[SerializeField] protected BaseUnit[] allUnits;
	[SerializeField] protected UnitSpawnPoint[] moveUnitSpawnPoints;
    
    protected BaseUnit unitToPlace;

    private void Update()
    {
		HandlePlaceUnit();
    }

	protected abstract void HandlePlaceUnit();
}
