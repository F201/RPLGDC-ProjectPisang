using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    public float doubleJumpSpeed = 3f;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public float ouch = 1f;
    public float stunTime = 1f;
    public float stunRate = 1f;
    public float invulnerabilityTime = 2f;

    //State
    bool isHurting = false;
    bool isAlive = true;
    bool canJump;

    //Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isHurting)
        {
            if (Time.time > stunTime)
            {
                isHurting = false;
                stunRate++;
                ouch = ouch * 2;
            }
            else { return; }
        }
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        Hurt();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // nilai diantara -1 dengan +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        //running animation
        bool ifHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", ifHasHorizontalSpeed);
    }

    private void Jump()
    {
        bool onTheGround = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Thin Ground"))
            || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle"));
        if (onTheGround)
        {
            canJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                //SoundManager.instance.PlaySingle(jumpSound);
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity = jumpVelocityToAdd;
                Debug.Log("JUMP");
            }
        }
        else if (canJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //SoundManager.instance.PlaySingle(jumpSound);
                canJump = false;
                Vector2 jumpVelocityToAdd = new Vector2(0f, doubleJumpSpeed);
                myRigidbody.velocity = jumpVelocityToAdd;
                Debug.Log("JUMP");
            }
        }
        myAnimator.SetBool("Jumping", !canJump);
    }
    private void FlipSprite()
    {
        bool ifHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (ifHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    private void Hurt()
    {
        if (Time.time > stunTime + invulnerabilityTime)
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle"))
                || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))
                || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle"))
                || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Death")))
            {
                //SoundManager.instance.PlaySingle(hitSound);
                myRigidbody.velocity = new Vector2(ouch, ouch);
                isHurting = true;
                stunTime = Time.time + stunRate;
            }
        }
        myAnimator.SetBool("Hurt", isHurting);
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Death")))
        {
            isAlive = false;
        }
    }
}