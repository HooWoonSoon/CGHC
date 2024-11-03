using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillText;
    
    public void ShowToolTip(string _skillName, string _description)
    {
        skillName.text = _skillName;
        skillText.text = _description;
        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
