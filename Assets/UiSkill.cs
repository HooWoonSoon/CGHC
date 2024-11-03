using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SkillType
{
    Dash,
    Control
}

public class UiSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IsSaveManager
{
    private Image skillImage;
    private UISkillToolTip skillToolTip;
    [SerializeField] private SkillType skillType; 
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    public bool unlocked;

    private void OnValidate()
    {
        gameObject.name = "UiSkill - " + skillName;
    }

    private void OnEnable()
    {
        skillImage = GetComponent<Image>();
        skillToolTip = FindObjectOfType<UISkillToolTip>();
        UpdateSkillVisual();
    }

    private void UpdateSkillVisual()
    {
        skillImage.color = unlocked ? Color.white : Color.gray;
    }

    public SkillType GetSkillType() => skillType;  

    public void Unlock()
    {
        unlocked = true;
        UpdateSkillVisual();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillToolTip.ShowToolTip(skillName,skillDescription);

        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        if (mousePosition.x > 600)
            xOffset = -150;
        else
            xOffset = 150;

        skillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y - 100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillToolTip.HideToolTip();
    }

    public void LoadData(PlayerInfo _data)
    {
        
    }

    public void SaveData(ref PlayerInfo _data)
    {
        if (_data.skill.TryGetValue(skillName, out bool value))
        {
            _data.skill.Remove(skillName);
            _data.skill.Add(skillName, unlocked);
        }
        else
        {
            _data.skill.Add(skillName, unlocked);
        }
    }
}
