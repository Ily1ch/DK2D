using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabatonsItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int Speed = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            heromove SpeedHero = collision.gameObject.GetComponent<heromove>();
            if (SpeedHero != null)
            {
                SpeedHero.speed += Speed;
            }
        }
    }
}
