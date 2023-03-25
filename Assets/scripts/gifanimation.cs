using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gifanimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = speed;
    }
}
