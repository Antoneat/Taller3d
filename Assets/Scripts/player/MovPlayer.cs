using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float AutoMovSpeed;
    public float speed;

    public float jumpHeight;
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

    modelChange modCha;

    public Animator anim;
    Vector3 Vec;
   // Vector3 moveF;
    //Vector3 moveS;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        modCha = GetComponent<modelChange>();
        modCha.model1.SetActive(true);

        currentDashing = maxDashing;
        currentSliding = maxSliding;
        dashBar.SetMaxDash(maxDashing);
        slideBar.SetMaxSlide(maxSliding);

        isBouncing = false;
        
     //   Physics.gravity = new Vector3(0, -7.0F, 0);
    }

    IEnumerator isAttacking()
    {
        dashCollision = true;

        yield return new WaitForSeconds(2);

        dashCollision = false;

        yield return null;
    }

    void ModelChangeD()
    {
        modCha.model3.SetActive(false);
        modCha.model1.SetActive(true);
    }

    void ModelChangeS()
    {
        modCha.model2.SetActive(false);
        modCha.model1.SetActive(true);
    }

    
    void FixedUpdate()
    {

        Move();
       

        if (isDashing)
        {
            Dash();
            StartCoroutine(isAttacking());
            modCha.model1.SetActive(false);
            modCha.model3.SetActive(true);
            Invoke("ModelChangeD", 0.8f);

            isDashing = false;
            currentDashing = 0;
            dashBar.SetDash(currentDashing);
        }


        if (isSliding)
        {
            Slide();
            modCha.model1.SetActive(false);
            modCha.model2.SetActive(true);
            Invoke("ModelChangeS", 1);
            
            isSliding = false;
            currentSliding = 0;
            slideBar.SetSlide(currentSliding);
        }
 
    }

    void Update()
    {
        // moveF = new Vector3(rb.velocity.x, rb.velocity.y, 1);
        //moveS = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, rb.velocity.z);
        MoveLados();
        Jump();
      
        if (Input.GetMouseButtonDown(0) && currentDashing == 3)
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
        //rb.AddForce(Vector3.forward * AutoMovSpeed);
        if (!isBouncing) rb.transform.position += rb.transform.forward * AutoMovSpeed * Time.deltaTime;
    }

    void MoveLados()
    {

        Vec = transform.localPosition;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.localPosition = Vec;

        //rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        // rb.velocity = new Vector3(hmove, rb.velocity.y, rb.velocity.z);
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            //hmove = -speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {     
            //hmove = speed;
        }*/

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
                Invoke("FallJump", 3.5f);
                NumberJumps += 1;  
            }
        }
    }

    void FallJump()
    {
        GetComponent<ConstantForce>().force = new Vector3(0, -5.0f, 0);
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
        NumberJumps = 0;


        if (other.gameObject.CompareTag("obstaculo"))
        {
            anim.SetTrigger("Tired");
            if (rb.position.y<= 4.70 && !isBouncing)
            {
                float bounce = 700f; //cant d fuerza aplicada al bounce
                rb.AddForce(other.contacts[0].normal * bounce);
                isBouncing = true;
                Invoke("StopBounce", .5f);
            }
        }

        if (other.gameObject.CompareTag("paredesInvisibles"))
        {
            float bounce = 700f; //cant d fuerza aplicada al bounce
            rb.AddForce(other.contacts[0].normal * bounce);
            isBouncing = true;
            Invoke("StopBounce", .005f);
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
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
