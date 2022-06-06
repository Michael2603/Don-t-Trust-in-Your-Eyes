using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    Animator animator;

    AudioSource audiosource;
    public AudioClip activateSound;
    public AudioClip openningSound;

    public float openTimer;

    MajorScript majorScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        
        audiosource.clip = openningSound;
        audiosource.Play();

        openTimer = Random.Range(1,3);
    }

    void FixedUpdate()
    {
        openTimer -= Time.deltaTime;

        if (openTimer <= 0)
        {
            animator.SetTrigger("Close");
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Activate(GameObject player)
    {
        animator.SetTrigger("Activate");
        player.gameObject.SetActive(false);

        audiosource.clip = activateSound;
        audiosource.Play();

        GameObject.Find("MajorController").GetComponent<MajorScript>().pointed++;
    }

    public void HideObjects()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}