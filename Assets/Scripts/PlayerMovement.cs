using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float normalSpeed = 5f;
    public float boostSpeed = 5f;
    public float boostDuration = 2f;
    public float boostCooldown = 5f;

    private float currentSpeed;
    private float boostTimer;
    private bool isBoosting;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        normalSpeed = 3f;
        currentSpeed = normalSpeed;
        isBoosting = false;
    }

    void Update()
    {
        HandleBoostInput();

        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                EndBoost();
            }
        }
        else if (boostTimer < boostCooldown)
        {
            boostTimer += Time.deltaTime;
        }

        UpdateUI(); // Implement your UI update logic here
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // Apply movement with speed
        Vector3 movementDelta = m_Movement * currentSpeed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movementDelta);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void HandleBoostInput()
    {
        if (Input.GetKey(KeyCode.Space) && boostTimer >= boostCooldown)
        {
            StartBoost();
        }
    }

    void StartBoost()
    {
        isBoosting = true;
        currentSpeed = boostSpeed;
        boostTimer = boostDuration;
    }

    void EndBoost()
    {
        isBoosting = false;
        currentSpeed = normalSpeed;
    }

    void UpdateUI()
    {
        // Implement your UI update logic here
        // You can use isBoosting and boostTimer values to update the UI icon or display
    }
}


