using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackPLayer : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private heromove playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<heromove>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && cooldownTimer > attackCooldown)
        {
            RangeAttack();

        }

        cooldownTimer += Time.deltaTime;
    }

    private void RangeAttack()
    {
        anim.SetTrigger("RangeAttackTrigger");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
