using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSword : MonoBehaviour
{
    public int damageBonus = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Get the player's character stats component
            heromove attackDamage = collision.gameObject.GetComponent<heromove>();
            if (attackDamage != null)
            {
                // Add the sword's damage and speed bonus to the player's stats
                attackDamage.attackDamage += damageBonus;

                // Destroy the sword item
                Destroy(gameObject);
            }
        }
    }
}
