using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUnitManagerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private Button button;

    public void Fill(UnitData unit, UnityAction onClick)
    {
        txtName.text = unit.unitName;
        txtCost.text = unit.unitInWorld.GetStat().GetValue(StatEnum.Cost).ToString();
        button.onClick.AddListener(onClick);
    }
}
