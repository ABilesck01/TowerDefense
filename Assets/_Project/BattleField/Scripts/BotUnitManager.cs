using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotUnitManager : UnitManager
{
    [SerializeField] private float timeToSpawn = 1.33f;

    private float nextTimeToSpawn;

    protected override void HandlePlaceUnit()
    {
        nextTimeToSpawn -= Time.deltaTime;

        if(nextTimeToSpawn < 0)
        {
            nextTimeToSpawn = timeToSpawn;
            unitToPlace = allUnits[Random.Range(0, allUnits.Length)];
            PlaceUnit();
        }
    }

    private void PlaceUnit()
    {
        Transform spawn = moveUnitSpawnPoints[Random.Range(0, moveUnitSpawnPoints.Length)].transform;

        Instantiate(unitToPlace, spawn.position, spawn.rotation);
        unitToPlace = null;
    }
}
