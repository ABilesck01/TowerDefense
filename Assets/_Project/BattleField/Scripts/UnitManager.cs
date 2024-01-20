using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitManager : MonoBehaviour
{
	[SerializeField] private BaseUnit[] allUnits;
	[SerializeField] private UnitSpawnPoint[] moveUnitSpawnPoints;

	//private float gold;
	//private BaseUnit unitToPlace;

	//public float Gold
	//{
	//	get { return gold; }
	//	set { gold = value; }
	//}

	//public void BuyUnit(BaseUnit unit)
	//{
	//	if(Gold >= unit.GetUnitCost())
	//	{
	//		Gold -= unit.GetUnitCost();
	//		unitToPlace = unit;
	//		if(unit is MoveUnit)
	//		{
 //               foreach (UnitSpawnPoint t in moveUnitSpawnPoints)
 //               {
 //                   t.ShowFeedback();
 //               }
 //           }
	//	}
	//}

    private void Update()
    {
		HandlePlaceUnit();

  //      if(Input.GetMouseButton(0) && unitToPlace != null)
		//{
		//	Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	Transform spawn = null;
		//	float shortDistance = float.MaxValue;
		//	foreach(UnitSpawnPoint t in moveUnitSpawnPoints)
		//	{
		//		float distance = Vector2.Distance(t.transform.position, mousePosition);
		//		if(distance < shortDistance)
		//		{
		//			shortDistance = distance;
		//			spawn = t.transform;
		//		}
		//	}

  //          foreach (UnitSpawnPoint t in moveUnitSpawnPoints)
  //          {
  //              t.HideFeedback();
  //          }

  //          if (spawn != null)
		//	{
		//		Instantiate(unitToPlace, spawn.position, spawn.rotation);
		//		unitToPlace = null;
		//	}
		//}
    }

	protected abstract void HandlePlaceUnit();
}
