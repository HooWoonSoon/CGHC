using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTriggerGet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            SkillManager skillManager = SkillManager.instance;

            if (!skillManager.dash.dashUnlocked)
            {
                skillManager.dash.UnlockDash();
                Debug.Log("Dash is unlock");
            }
        }
    }
}
