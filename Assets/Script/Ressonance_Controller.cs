using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressonance_Controller : MonoBehaviour
{
    public Ela_Controller ela;
    public Ela_UD ela_ud;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ela.gameObject.activeSelf)
            transform.position = new Vector3(ela.transform.position.x, transform.position.y, 0);
        else
            transform.position = new Vector3(ela_ud.transform.position.x, transform.position.y, 0);
        
        if (ela.state == "moving")
            animator.SetTrigger("Landed");

        if (ela.rigidbody2d.velocity.x != 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        animator.SetFloat("AbsVelocityY", Mathf.Abs(ela.rigidbody2d.velocity.y));
    }
}
