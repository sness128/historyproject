using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [Header ("Stuff")]
    public float moveSpeed;
    public float friction;

    public float jumpForce;
    public float cantJump; //cooldown for the jump
    public float airSpeedMultiplier;
    bool canJump = true;
    public KeyCode jumpButton = KeyCode.Space;

    public bool applyFrictionAndLimitSpeed;

    public Transform orientation;
    public TextMeshProUGUI test;

    float inputX;
    float inputY;

    public float height;
    public LayerMask Ground;
    bool grounded;

    Vector3 movementD; //the direction

    Rigidbody rB;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        rB.freezeRotation = true;
        

    }

    private void SetText()
    {
        test.text = "Grounded: " + grounded.ToString() + " Can Jump: " + canJump.ToString();
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.2f, Ground);
        InputMan();
        if (applyFrictionAndLimitSpeed != false)
        {
            speedLimit();
        }
        
        if (grounded == true && applyFrictionAndLimitSpeed == true && grounded != false && applyFrictionAndLimitSpeed != false && grounded == applyFrictionAndLimitSpeed && grounded == !false  && applyFrictionAndLimitSpeed == !false)
        {
            rB.drag = friction;

        } else
        {
            rB.drag = 0;
        }
        SetText();
        

        
    }

    private void FixedUpdate()
    {
        MoveTheMan();
    }

    private void InputMan()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpButton) && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(CanJump), cantJump);
        }
    }

    void MoveTheMan()
    {
        movementD = orientation.forward * inputY + orientation.right * inputX;
        if (grounded)
        {
            rB.AddForce(movementD.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (grounded == false)
        {
            rB.AddForce(movementD.normalized * moveSpeed * 10f * airSpeedMultiplier, ForceMode.Force);
        }


    }
    
    void speedLimit()
    {
        Vector3 flatVelocity = new Vector3(rB.velocity.x, 0f, rB.velocity.y);

        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rB.velocity = new Vector3(limitedVelocity.x, rB.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rB.velocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);
        rB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void CanJump()
    {
        canJump = true;
    }
}
