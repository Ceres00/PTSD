using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float boostCapacity;
    [SerializeField] private float boostCount;
    [SerializeField] private float boostRegenRate;
    [SerializeField] private float boostCooldownTime = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float SpeedMultiplier = 1;

    bool isBoostCooldown;
    float boostCooldownTimer;
    bool isBoosting;

    private Vector2 moveDirection;

    private void Start()
    {
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        ProcessInputs();
        Boost();
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Boost()
    {
        if (Input.GetKeyDown(KeyCode.Space) && boostCount > 0 && !isBoostCooldown)
        {
            isBoosting = true;
            moveSpeed *= 1.3f;
        }

        if ((Input.GetKeyUp(KeyCode.Space) || boostCount <= 0) && !isBoostCooldown)
        {
            isBoosting = false;
            moveSpeed = 4f * SpeedMultiplier; 
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
                moveSpeed = 4f * SpeedMultiplier;
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


    private void Flip()
    {
        if (!isFacingRight && horizontal < 0f || isFacingRight && horizontal >= 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x * -1f;
            transform.localScale = localScale;
        }
    }
}
