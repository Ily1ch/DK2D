using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private int damage;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        heromove player = collision.GetComponent<heromove>();
        if (player != null)
        {
            hit = true;
            coll.enabled = false;
            anim.SetTrigger("Explode");
            player.TakeDamage(damage);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
