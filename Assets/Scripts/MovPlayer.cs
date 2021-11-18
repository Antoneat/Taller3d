using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float AutoMovSpeed = 1f;
    public float speed = 5f;
    Vector3 Vec;

    public float jumpHeight = 7f;
    public bool isGrounded;
    public float NumberJumps = 0f;
    public float MaxJumps = 1;

    private Rigidbody rb;

    public float dashSpeed;
    public bool isDashing;

    public int maxDashing = 3;
    public int currentDashing;

    public float timeForDash;
    public float maxTimeForDash;

    public DashBar dashBar;
    public SlideBar slideBar;

    public float slideSpeed;
    public bool isSliding;

    public int maxSliding = 3;
    public int currentSliding;

    public float timeForSlide;
    public float maxTimeForSlide;

    public float modCRate = 1f;
    private float modChange = 0.0f;

    modelChange modCha;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        modCha = GetComponent<modelChange>();
        modCha.model1.SetActive(true);

        currentDashing = maxDashing;
        currentSliding = maxSliding;
        dashBar.SetMaxDash(maxDashing);
        slideBar.SetMaxSlide(maxSliding);
    }
  
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * AutoMovSpeed;
        if(isDashing)
        {
            Dash();
            currentDashing = 0;
            dashBar.SetDash(currentDashing);
        }

        if (isSliding)
        {
            Slide();
            modCha.model1.SetActive(false);
            modCha.model2.SetActive(true);

            currentSliding = 0;
            slideBar.SetSlide(currentSliding);
        }
        else if (Time.time > modChange) 
        {
            modChange = Time.time + modCRate;
            modCha.model1.SetActive(true);
            modCha.model2.SetActive(false);
        }
    }

    void Update()
    {
        Move();
        Jump();

        if (Input.GetMouseButtonDown(0) && currentDashing==3)
        {
            isDashing = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && isDashing == false && isGrounded == true && currentSliding == 3)
        {
            isSliding = true;
        }

        if (currentDashing < maxDashing)
        {
            RegeneracionDash();
            dashBar.SetDash(currentDashing);
        }

        if (currentSliding < maxSliding)
        {
            RegeneracionSlide();
            slideBar.SetSlide(currentSliding);
        }
    }

    void Move()
    {
        Vec = transform.localPosition;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.localPosition = Vec;
    }

    void Jump()
    {
        if (NumberJumps >= MaxJumps)
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(Vector3.up * jumpHeight);
                NumberJumps += 1;
            }
        }
    }

    void Dash()
    {
            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            isDashing = false;
    }

    void Slide()
    {
        rb.AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
        isSliding = false;
    }

    private void RegeneracionDash()
    {
        timeForDash = timeForDash + Time.deltaTime;
        if (timeForDash >= maxTimeForDash)
        {
            currentDashing++;
            timeForDash = 0;
        }
    }

    private void RegeneracionSlide()
    {
        timeForSlide = timeForSlide + Time.deltaTime;
        if (timeForSlide >= maxTimeForSlide)
        {
            currentSliding++;
            timeForSlide = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("walls"))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        NumberJumps = 0;
    }

    void OnCollisionExit(Collision other)
    {

    }
}
