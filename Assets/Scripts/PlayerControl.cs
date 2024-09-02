using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;

    private float inputX, inputZ;
    private bool inputJump;

    private Vector3 direction;

    private float speed = 6f;
    private float forceJump = 8f;

    private bool inGround = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputsLogic();
        FlipLogic();
        JumpLogic();
        AnimationsLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    public void InputsLogic()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        inputJump = Input.GetKeyDown(KeyCode.Space);
    }

    public void FlipLogic()
    {
        if (inputX > 0)
        {
            GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    public void MoveLogic()
    {
        direction = new Vector3(inputX * speed, rb.velocity.y, inputZ * speed);
        rb.velocity = direction;
    }

    public void JumpLogic()
    {
        if (inputJump && inGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, forceJump, rb.velocity.z);
        }
    }

    public void AnimationsLogic()
    {
        anim.SetFloat("Horizontal", rb.velocity.x);
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetFloat("Z", rb.velocity.z);
        anim.SetBool("InGround", inGround);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            inGround = true;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            inGround = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            inGround = false;
        }
    }
}
