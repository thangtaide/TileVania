using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRig;
    [SerializeField] float moveSpeed = 1f;
    void Start()
    {
        enemyRig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        enemyRig.velocity = new(moveSpeed, 0f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Ground")) { return; }
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }
    void FlipEnemy()
    {
        transform.localScale = new(Mathf.Sign(moveSpeed), 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = GameObject.FindFirstObjectByType<PlayerMovement>();
            player.Die();
        }
    }
}
