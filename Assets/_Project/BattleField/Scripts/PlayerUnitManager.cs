using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitManager : UnitManager
{
    [SerializeField] private StatSO stats;

    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private PlayerUnitManagerItem itemPrefab;
    [SerializeField] private TextMeshProUGUI txtGold;
    [Space]
    [SerializeField] private Slider healthBar;
    [SerializeField] private float gold;

    private float lifePoints;

    public float Gold
    {
        get { return gold; }
        set 
        { 
            gold = value; 
            txtGold.text = ((int)gold).ToString();
        }
    }

    private void Start()
    {
        txtGold.text = ((int)gold).ToString();
        lifePoints = stats.GetValue(StatEnum.LifePoints);
        healthBar.maxValue = lifePoints;
        healthBar.value = lifePoints;

        allUnits.Clear();
        foreach (UnitData unit in CampController.gameData.OwnedUnits)
        {
            allUnits.Add(unit);
        }

        for (int i = 0; i < allUnits.Count; i++)
        {
            int index = i;
            var item = Instantiate(itemPrefab, buttonsContainer);
            item.Fill(allUnits[index], () =>
            {
                BuyUnit(allUnits[index].unitInWorld);
            });
        }
    }

    private void BuyUnit(BaseUnit unit)
    {
        if (Gold >= unit.GetUnitCost())
        {
            Gold -= unit.GetUnitCost();
            unitToPlace = unit;
            if (unit is MoveUnit)
            {
                foreach (UnitSpawnPoint t in moveUnitSpawnPoints)
                {
                    t.ShowFeedback();
                }
            }
        }
    }

    protected override void HandlePlaceUnit()
    {
        if (Input.GetMouseButton(0) && unitToPlace != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform spawn = null;
            float shortDistance = float.MaxValue;

            foreach (UnitSpawnPoint t in moveUnitSpawnPoints)
            {
                float distance = Vector2.Distance(t.transform.position, mousePosition);
                if (distance < shortDistance)
                {
                    shortDistance = distance;
                    spawn = t.transform;
                }
            }

            foreach (UnitSpawnPoint t in moveUnitSpawnPoints)
            {
                t.HideFeedback();
            }

            if (spawn != null)
            {
                Instantiate(unitToPlace, spawn.position, spawn.rotation);
                unitToPlace = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.TryGetComponent(out BaseUnit unit))
        {
            lifePoints -= unit.GetLifePoints();
            healthBar.value = lifePoints;
            Destroy(unit.gameObject);
            if(lifePoints <= 0)
            {
                gold = 0;
                GameManager.instance.GameOver();
            }
        }
    }
}
