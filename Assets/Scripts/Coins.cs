using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    bool isCollected = false;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            FindObjectOfType<Session>().AddToScore();
        }
    }
}
