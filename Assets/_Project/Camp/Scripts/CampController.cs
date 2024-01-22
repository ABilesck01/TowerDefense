using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampController : MonoBehaviour
{
    public static readonly string sceneName = "Camp";

    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private List<UnitData> initialUnits;
    [SerializeField] private GameObject unitsPanel;

    public static GameData gameData;
    private static float goldInCache = 0;

    private void Start()
    {
        gameData = new GameData();

        gameData.Gold += goldInCache;
        goldInCache = 0;
        txtGold.text = gameData.Gold.ToString();
        gameData.OwnedUnits = initialUnits;
    }

    public static void AddGoldInCache(float amount)
    {
        goldInCache += amount;
    }    

    public void GoToBattle()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowMyUnits()
    {
        unitsPanel.SetActive(true);
    }

    public void HideMyUnits()
    {
        unitsPanel.SetActive(false);
    }
}
