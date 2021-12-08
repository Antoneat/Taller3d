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

    public Rigidbody rb;

    public float dashSpeed;
    public bool isDashing;
    public GameObject dashPos;
    public GameObject dashVFX;

    public bool dashCollision;

    public int maxDashing = 3;
    public int currentDashing;

    public float timeForDash;
    public float maxTimeForDash;

    public DashBar dashBar;
    public SlideBar slideBar;

    public float slideSpeed;
    public bool isSliding;
    public GameObject slidePos;
    public GameObject slideVFX;

    public int maxSliding = 3;
    public int currentSliding;

    public float timeForSlide;
    public float maxTimeForSlide;

    public bool isBouncing;

    public float modCRate = 0.5f;
    private float modChange = 0.0f;

    modelChange modCha;
    Jugador ju;

    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        modCha = GetComponent<modelChange>();
        modCha.model1.SetActive(true);

        ju = GetComponent<Jugador>();

        currentDashing = maxDashing;
        currentSliding = maxSliding;
        dashBar.SetMaxDash(maxDashing);
        slideBar.SetMaxSlide(maxSliding);

        isBouncing = false;
    }
  
    IEnumerator isAttacking()
    {
        dashCollision = true;

        yield return new WaitForSeconds(2);

        dashCollision = false;

        yield return null;
    }

    void FixedUpdate()
    {
        //transform.position += transform.forward * Time.deltaTime * AutoMovSpeed;
        rb.transform.position += rb.transform.forward * AutoMovSpeed * Time.deltaTime;
        if (isDashing)
        {
            Dash();
            StartCoroutine(isAttacking());
            modCha.model1.SetActive(false);
            modCha.model3.SetActive(true);
            isDashing = false;
            currentDashing = 0;
            dashBar.SetDash(currentDashing);
        }
        else if (Time.time > modChange)
        {
            modChange = Time.time + modCRate;
            modCha.model3.SetActive(false);
            modCha.model1.SetActive(true);
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

            if (!isBouncing) rb.MovePosition(rb.position + Vec * speed * Time.fixedDeltaTime);
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
        GameObject obj = Instantiate(dashVFX);
        obj.transform.position = dashPos.transform.position;
        obj.transform.parent = dashPos.transform;
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = true;
    }

    void Slide()
    {
        GameObject obj = Instantiate(slideVFX);
        obj.transform.position = slidePos.transform.position;
        obj.transform.parent = slidePos.transform;
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


        if (other.gameObject.CompareTag("obstaculo"))
        {
            anim.SetTrigger("Tired");
            float bounce = 1200f; //cant d fuerza aplicada al bounce
            rb.AddForce(other.contacts[0].normal * bounce);
            isBouncing = true;
            Invoke("StopBounce", 1.5f);
            
            if (ju.vida <= 0)
            {
                Destroy(gameObject); // Cuando la vida llegue a 0 el jugador morira.
            }
        }
    }

    void StopBounce()
    {
        isBouncing = false;
    }

    void OnCollisionExit(Collision other)
    {

    }


}
