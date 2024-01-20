using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject feedback;

    public void ShowFeedback()
    {
        feedback.SetActive(true);
    }

    public void HideFeedback()
    {
        feedback.SetActive(false);
    }
}
