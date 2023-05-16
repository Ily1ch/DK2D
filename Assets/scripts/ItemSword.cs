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
            heromove attackDamage = collision.gameObject.GetComponent<heromove>();
            if (attackDamage != null)
            {
                attackDamage.attackDamage += damageBonus;
            }
        }
    }
}
