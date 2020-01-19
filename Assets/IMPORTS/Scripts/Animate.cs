using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public float forwardMove;
    public float sideMove;
    public float rotateSpeed;
    public Rigidbody rb;

    Animator anim;
    //CharacterController controller;

    Vector3 moveDir = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetInteger("Walking", 0);

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("Condition", 1);
  
            moveDir = new Vector3(0, 0, 1);
            moveDir *= forwardMove;
            moveDir = transform.TransformDirection(moveDir);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveDir = new Vector3(0, 0, 0);
            anim.SetInteger("Condition", 0  );

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            moveDir = new Vector3(0, 0, .5f*Time.deltaTime);
            anim.SetInteger("isRunning", 1);
        }
         if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetInteger("isRunning", 0);
        }
           
        }
    }

