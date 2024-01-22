using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotUnitManager : UnitManager
{
    [SerializeField] private float timeToSpawn = 1.33f;

    private float nextTimeToSpawn;
    private bool canSpawn = true;

    private void Start()
    {
        GameManager.instance.OnFinishRound.AddListener(OnFinishRound);
        GameManager.instance.OnNextRound.AddListener(OnNextRound);
    }

    

    protected override void HandlePlaceUnit()
    {
        if (!canSpawn) return;

        nextTimeToSpawn -= Time.deltaTime;

        if(nextTimeToSpawn < 0)
        {
            nextTimeToSpawn = timeToSpawn;
            unitToPlace = allUnits[Random.Range(0, allUnits.Count - 1)].unitInWorld;
            PlaceUnit();
        }
    }

    private void PlaceUnit()
    {
        Transform spawn = moveUnitSpawnPoints[Random.Range(0, moveUnitSpawnPoints.Length)].transform;

        var unit = Instantiate(unitToPlace, spawn.position, spawn.rotation);
        unit.OnUnitDeath.AddListener(OnUnitDeath);
        unitToPlace = null;

    }

    private void OnUnitDeath(BaseUnit unit)
    {
        GameManager.instance.AddXp(unit.GetStat());
    }

    private void OnFinishRound()
    {
        canSpawn = false;
    }

    private void OnNextRound()
    {
        canSpawn = true;
        timeToSpawn *= 0.9f;
        if (timeToSpawn < 0.3)
            timeToSpawn = 0.3f;
    }
}
