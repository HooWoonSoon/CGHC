using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTriggerGet : MonoBehaviour
{
    [SerializeField] private List<UiSkill> uiSkills;
    [SerializeField] private GameObject key;
    [SerializeField] private SkillType skillType;
    private bool playerInRange = false; // Flag to track if player is in range

    private void Start()
    {
        foreach (UiSkill search in Resources.FindObjectsOfTypeAll<UiSkill>())
        {
            uiSkills.Add(search);
        }
    }

    private void Update()
    {
        // Check if player is in range and presses "E" to unlock the skill
        if (playerInRange)
        {
            key.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnlockSkill();
            }
        }
        else
        {
            key.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            playerInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            playerInRange = false; 
        }
    }

    private void UnlockSkill()
    {
        SkillManager skillManager = SkillManager.instance;

        if (skillType == SkillType.ControlGravity && !skillManager.control.controlUnlocked)
        {
            UnlockUiSkill(skillType);
            skillManager.control.UnlockControl();
            Debug.Log("Control is unlocked");
        }
        else if (skillType == SkillType.DoubleJump && !skillManager.doubleJump.doubleJumpUnlock)
        {
            UnlockUiSkill(skillType);
            skillManager.doubleJump.UnlockDoubleJump();
            Debug.Log("Double Jump is unlocked");
        }
        else if (skillType == SkillType.Dash && !skillManager.dash.dashUnlocked)
        {
            UnlockUiSkill(skillType);
            skillManager.dash.UnlockDash();
            Debug.Log("Dash is unlocked");
        }
        else if (skillType == SkillType.Hook && !skillManager.hook.hookUnlocked)
        {
            UnlockUiSkill(skillType);
            skillManager.hook.UnlockControl();
            Debug.Log("Hook is unlocked");
        }
    }

    private void UnlockUiSkill(SkillType skillType)
    {
        foreach (var uiSkill in uiSkills)
        {
            if (uiSkill != null && uiSkill.GetSkillType() == skillType)
            {
                uiSkill.Unlock();
                return;
            }
        }
    }
}
