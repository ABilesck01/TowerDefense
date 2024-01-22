using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampController : MonoBehaviour
{
    public static readonly string sceneName = "Camp";

    public static CampController instance;

    public GameData gameData;

    public static event EventHandler OnGameDataChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (UnitData item in gameData.OwnedUnits)
        {
            item.ClearUpgrades();
        }
    }

    public void AddGoldInCache(float amount)
    {
        gameData.Gold += amount;
        OnGameDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveGold(float amount)
    {
        gameData.Gold -= amount;
        OnGameDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddUnit(UnitData unit)
    {
        gameData.OwnedUnits.Add(unit);
        OnGameDataChanged?.Invoke(this, EventArgs.Empty);
    }
}
