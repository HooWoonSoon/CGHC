using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, Damageble
{
    [Header("Settings")]
    [SerializeField] private bool instantKill;

    public void Damage(Player player)
    {
        if (player != null)
        {
            if (instantKill)
            {
                player.KillPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();

        if (player != null)
        {
            Damage(player);
        }
    }
}
