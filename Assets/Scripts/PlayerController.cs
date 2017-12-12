using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public float speed = 0.5f;
    public int forceConst = 5;
    public float distToGround;
    private bool canJump;
    public bool IsGrounded;
    private Rigidbody selfRigidbody;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        selfRigidbody = GetComponent<Rigidbody>();

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        IsGrounded = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        IsGrounded = false;
    }

    void FixedUpdate()
    {
        if (canJump)
        {
            canJump = false;
            selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }

        //Character Walk
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("Sprint", true);
            anim.SetBool("Idle", false);
        }

        //Character Stop 
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Sprint", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= Vector3.forward * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position -= Vector3.left * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Sprint", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

            if (Input.GetKeyDown(KeyCode.K))
            {
                anim.SetBool("Kick", true);
                anim.SetBool("Idle", false);
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                anim.SetBool("Kick", false);
                anim.SetBool("Idle", true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && (IsGrounded == true))
        {
            canJump = true;
        }
      
    }

}