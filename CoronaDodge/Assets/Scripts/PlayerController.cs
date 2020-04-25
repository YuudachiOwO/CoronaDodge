using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;



    void Start()
    {
        //Get Component rigidbody of the player
        //rb = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    void moveChar(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }




}
