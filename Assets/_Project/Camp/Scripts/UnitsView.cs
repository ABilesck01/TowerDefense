using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitsView : MonoBehaviour
{
    [SerializeField] private List<UnitData> units = new List<UnitData>();
    [Space]
    [SerializeField] private Transform container;
    [SerializeField] private UnitsViewItem itemPrefab;

    public void Fill()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }

        List<UnitData> ownedUnits = CampController.gameData.OwnedUnits;
        foreach (UnitData unitData in units)
        {
            UnitsViewItem newItem = Instantiate(itemPrefab, container);
            if (ownedUnits.Contains(unitData))
            {
                newItem.SetUnitData(unitData, true, () => UpgradeUnit(unitData));
            }
            else
            {
                newItem.SetUnitData(unitData, false, () => BuyUnit(unitData));
            }
        }
    }

    private void OnEnable()
    {
        Fill();
    }

    private void UpgradeUnit(UnitData unit)
    {
        if(CampController.gameData.Gold < unit.upgradeCost)
        {
            return;
        }

        CampController.gameData.Gold -= unit.upgradeCost;
    }

    private void BuyUnit(UnitData unit)
    {
        if (CampController.gameData.Gold < unit.unlockCost)
        {
            return;
        }

        CampController.gameData.OwnedUnits.Add(unit);
        Fill();
    }
}
