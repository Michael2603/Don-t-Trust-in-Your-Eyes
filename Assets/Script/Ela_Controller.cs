using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ela_Controller : MonoBehaviour
{
    public MajorScript majorScript;

    [HideInInspector]public Rigidbody2D rigidbody2d;
    public float moveSpeed;
    public float jumpHeight;
    public float blinkDistance;
    
    [HideInInspector]public string state = "moving";

    Vector2 savedVelocity;
    Animator animator;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rigidbody2d.velocity.x > .1f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 0);
        else if (rigidbody2d.velocity.x < -.1f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 1, 0);

        if (state == "moving")
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rigidbody2d.AddForce(transform.up * jumpHeight);
                state = "jumping";
            }
        }
        else if (state == "jumping")
        {
            if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded())
            {
                savedVelocity = rigidbody2d.velocity;

                state = "blinking";
                animator.SetTrigger("Blinked");
                audioSource2.Play();
            }
            if (IsGrounded() && rigidbody2d.velocity.y < 0)
            {
                animator.SetTrigger("Landed");
                state = "moving";
            }
        }
        else if (state == "grounding")
        {
            if (IsGrounded())
            {
                animator.SetTrigger("Landed");
                state = "moving";
            }
        }

        animator.SetFloat("AbsVelocityY", Mathf.Abs(rigidbody2d.velocity.y));
        animator.SetFloat("VelocityY", rigidbody2d.velocity.y);
        animator.SetFloat("VelocityX", rigidbody2d.velocity.x);

        if (Mathf.Abs(rigidbody2d.velocity.x) > 0)
            animator.SetBool("Running", true);
        else   
            animator.SetBool("Running", false);
    }

    void FixedUpdate()
    {
        if (state == "moving")
        {
            rigidbody2d.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigidbody2d.velocity.y, 0);
            if (!audioSource1.isPlaying && rigidbody2d.velocity.x != 0)
                audioSource1.Play();
            else if (rigidbody2d.velocity.x == 0)
                audioSource1.Stop();
        }
    }

    bool IsGrounded()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        // Creates a small box just bellow the player, that constantly check for colliders in specifics layers
        RaycastHit2D raycastGround = Physics2D.BoxCast( playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f,
            1 << LayerMask.NameToLayer("Platform"));

        return raycastGround.collider != null;
    }

    void Blink()
    {
        rigidbody2d.velocity = savedVelocity;
        
        transform.localPosition += new Vector3( (System.Math.Sign(savedVelocity.x) * blinkDistance), (System.Math.Sign(savedVelocity.y) * blinkDistance), 0);
        
        state = "grounding";
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            other.transform.gameObject.GetComponent<PortalBehaviour>().Activate(this.gameObject);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        majorScript.Defeat();
    }
}