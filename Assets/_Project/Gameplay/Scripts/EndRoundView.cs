using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRoundView : MonoBehaviour
{
    [SerializeField] private GameObject screen;

    public void ShowRoundFinished()
    {
        screen.SetActive(true);
    }

    public void NextRound()
    {
        screen.SetActive(false);
        GameManager.instance.NextRound();
    }

    public void BackToCamp()
    {
        screen.SetActive(false);
        CampController.AddGoldInCache(GameManager.instance.GetGold());
        SceneManager.LoadScene(CampController.sceneName);
    }

}
