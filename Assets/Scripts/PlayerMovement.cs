using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float jumpingPower = 4f;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float boostCapacity;
    [SerializeField] private float boostCount;
    [SerializeField] private float boostRegenRate;
    [SerializeField] private float boostCooldownTime = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float SpeedMultiplier = 1;
    public float JumpMult = 1;

    bool isBoostCooldown;
    float boostCooldownTimer;
    bool isBoosting;
    bool isJumping;
    float jumpCounter;

    Vector2 vecGravity;

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * JumpMult);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += currentJumpM * Time.deltaTime * vecGravity;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= fallMultiplier * Time.deltaTime * vecGravity;
        }

        Boost();
        Flip();
    }

    private void FixedUpdate()
    {
        float targetVelocityX = horizontal * speed * SpeedMultiplier;
        float accelerationRate = horizontal == 0 ? deceleration : acceleration;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, targetVelocityX, accelerationRate * Time.deltaTime), rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    private void Boost()
    {
        if (Input.GetKeyDown(KeyCode.Space) && boostCount > 0 && !isBoostCooldown)
        {
            isBoosting = true;
            speed *= 1.3f;
            jumpingPower *= 1.3f;
        }

        if ((Input.GetKeyUp(KeyCode.Space) || boostCount <= 0) && !isBoostCooldown)
        {
            isBoosting = false;
            speed = 4f * SpeedMultiplier; 
            jumpingPower = 4f * JumpMult;
        }

        if (!isBoosting && boostCount < boostCapacity)
        {
            boostCount += boostRegenRate * Time.deltaTime;
            boostCount = Mathf.Clamp(boostCount, 0f, boostCapacity);
        }

        if (isBoosting)
        {
            boostCount -= Time.deltaTime;
            boostCount = Mathf.Clamp(boostCount, 0f, boostCapacity);

            if (boostCount <= 0f)
            {
                isBoosting = false;
                speed = 4f * SpeedMultiplier;
                jumpingPower = 4f * JumpMult;
                isBoostCooldown = true;
                boostCooldownTimer = boostCooldownTime;
            }
        }

        if (isBoostCooldown)
        {
            boostCooldownTimer -= Time.deltaTime;
            if (boostCooldownTimer <= 0f)
            {
                isBoostCooldown = false;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
