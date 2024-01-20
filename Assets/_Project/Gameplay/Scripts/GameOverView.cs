using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject screen;

    public void ShowGameOver()
    {
        screen.SetActive(true);
    }

    public void BackToCamp()
    {
        SceneManager.LoadScene("Camp");
    }
}
