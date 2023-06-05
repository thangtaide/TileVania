using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumbSpeed = 25f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] int countJumb = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    private int cJumb;
    bool isAlive;
    Animator anim;
    Vector2 moveInput;
    Rigidbody2D rig;
    CapsuleCollider2D bodyPlayerCollider;
    BoxCollider2D feetPlayerCollider;
    float gravicityAtStart;
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        bodyPlayerCollider = GetComponent<CapsuleCollider2D>();
        feetPlayerCollider = GetComponent<BoxCollider2D>();
        cJumb = countJumb;
        gravicityAtStart = rig.gravityScale;
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive) { return; }
            Run();
            ChangeStateSprite();
            ClimbLadder();
        if (rig.IsTouchingLayers(LayerMask.GetMask("Hazards"))){ Die(); }
    }
    void OnFire()
    {
        if(!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnJumb(InputValue value)
    {

        if (!isAlive) { return; }
        if (!feetPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            if (cJumb <= 0 ) return;
        }
        else { cJumb = countJumb; }
        if (value.isPressed)
        {
            rig.velocityY = jumbSpeed;
            cJumb -= 1;
        }
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void Run()
    {
        Vector2 playerVelocity = new (moveInput.x*runSpeed, rig.velocityY);
        rig.velocity = playerVelocity;
    }
    void ChangeStateSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rig.velocityX) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new(Mathf.Sign(rig.velocityX), 1f);
        }
        anim.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (!bodyPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            rig.gravityScale = gravicityAtStart;
            anim.SetBool("IsClimbing", false);
            return; }
        rig.gravityScale = 0f;
        Vector2 climbVelocity = new (rig.velocity.x, climbSpeed * moveInput.y);
        rig.velocity = climbVelocity;
        bool playerHasVertical = Mathf.Abs(rig.velocityY) > Mathf.Epsilon;
        anim.SetBool("IsClimbing", playerHasVertical);
    }
    public void Die()
    {
        isAlive = false;
        anim.SetTrigger("Dying");
        FindObjectOfType<Session>().ProcessPlayerDeath();
    }
    
}
