using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBehaviour : MonoBehaviour
{
    public void DestroyGameObject()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            DestroyGameObject();
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.name == "Ella")
            other.GetComponent<Ela_Controller>().Die();
        else if (other.gameObject.name == "Ella_UD")
            other.GetComponent<Ela_UD>().Die();

    }

    public void ActivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<AudioSource>().Play();
    }
}
