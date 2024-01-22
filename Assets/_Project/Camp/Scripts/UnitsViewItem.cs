using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitsViewItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtButtonLabel;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private Image icon;

    public void SetUnitData(UnitData unitData, bool hasUnit, UnityAction onClick)
    {
        txtName.text = unitData.unitName;
        icon.sprite = unitData.icon;
        txtButtonLabel.text = hasUnit ? "Upgrade" : "Unlock";
        txtCost.text = hasUnit ? unitData.upgradeCost.ToString() : unitData.unlockCost.ToString();
        button.onClick.AddListener(onClick);
    }
}
