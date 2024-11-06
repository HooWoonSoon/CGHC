using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTriggerGet : MonoBehaviour
{
    [SerializeField] private List<UiSkill> uiSkills;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject particle;
    [SerializeField] private SkillType skillType;
    [SerializeField] private float circleRange = 5;
    [SerializeField] private int spawnsCount = 20;
    private bool activated = false;
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
        if (playerInRange && activated == false)
        {
            key.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnlockSkill();
                SpawnObject();
            }
        }
        else
            key.SetActive(false);
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
        }
        else if (skillType == SkillType.DoubleJump && !skillManager.doubleJump.doubleJumpUnlock)
        {
            UnlockUiSkill(skillType);
            skillManager.doubleJump.UnlockDoubleJump();
        }
        else if (skillType == SkillType.Dash && !skillManager.dash.dashUnlocked)
        {
            UnlockUiSkill(skillType);
            skillManager.dash.UnlockDash();
        }
        else if (skillType == SkillType.Hook && !skillManager.hook.hookUnlocked)
        {
            UnlockUiSkill(skillType);
            skillManager.hook.UnlockControl();
        }

        activated = true;
    }

    private void SpawnObject()
    {
        for (int i = 0; i < spawnsCount; i++)
        {
            Instantiate(particle, RandomVector2InCircleRange(), Quaternion.identity);
        }
    }

    private Vector2 RandomVector2InCircleRange()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0, circleRange);
        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        return new Vector2(transform.position.x + x, transform.position.y + y);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, circleRange);
    }
}
