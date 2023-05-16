using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArmor : MonoBehaviour
{
    // Start is called before the first frame update
    public int HpBonus = 100;
    public HealthBar healthBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            heromove HpHero = collision.gameObject.GetComponent<heromove>();
            if (HpHero != null)
            {
                HpHero.maxHealth += HpBonus;
            }
        }
    }
}
