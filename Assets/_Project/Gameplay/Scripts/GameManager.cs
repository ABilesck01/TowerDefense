using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float pointsToWin = 5;
    [SerializeField] private Slider xpSlider;
    [Space]
    [SerializeField] private PlayerUnitManager player;
    [SerializeField] private BotUnitManager bot;
    [Space]
    public UnityEvent OnGameOver;
    public UnityEvent OnFinishRound;
    public UnityEvent OnNextRound;

    private float currentPoints;

    public static GameManager instance;

    private int currentRound = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        xpSlider.maxValue = pointsToWin;
        currentPoints = 0;
        currentRound = 1;
    }

    public float GetGold()
    {
        return player.Gold;
    }

    public void AddXp(StatSO unit)
    {
        currentPoints += unit.GetValue(StatEnum.Xp);
        xpSlider.value = currentPoints;
        player.Gold += unit.GetValue(StatEnum.Cost);
        if(currentPoints >= pointsToWin)
        {
            OnFinishRound?.Invoke();
            Invoke(nameof(ClearAllUnits), 1.5f);
        }
    }

    private void ClearAllUnits()
    {
        var allUnits = GameObject.FindObjectsOfType<BaseUnit>();
        foreach (BaseUnit item in allUnits)
        {
            Destroy(item.gameObject);
        }
    }

    public void NextRound()
    {
        currentRound++;
        pointsToWin *= 2;
        xpSlider.maxValue = pointsToWin;
        currentPoints = 0;
        xpSlider.value = 0;
        if ((currentRound %= 2) == 0)
            bot.UpgradeUnits();

        OnNextRound?.Invoke();
    }

    public void GameOver()
    {
        Invoke(nameof(ClearAllUnits), 1.5f);
        OnGameOver?.Invoke();
    }
}
