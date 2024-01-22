using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampView : MonoBehaviour
{
    [SerializeField] private GameObject unitsPanel;
    [SerializeField] private TextMeshProUGUI txtMoney;

    private void Start()
    {
        txtMoney.text = CampController.instance.gameData.Gold.ToString();
    }

    private void OnEnable()
    {
        CampController.OnGameDataChanged += CampController_OnGameDataChanged;
    }
    private void OnDisable()
    {
        CampController.OnGameDataChanged -= CampController_OnGameDataChanged;
    }

    private void CampController_OnGameDataChanged(object sender, System.EventArgs e)
    {
        txtMoney.text = CampController.instance.gameData.Gold.ToString();
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
