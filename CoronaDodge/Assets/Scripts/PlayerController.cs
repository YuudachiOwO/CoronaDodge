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
    [SerializeField] Animator myAnimator;
    private Vector3 startPos;

    private bool isBlocked = false;

    void Start()
    {
        //Get Component rigidbody of the player
        //rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        myAnimator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isBlocked)
        {

            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            SoundManager.Instance.ToggleFootSteps(moveInput.magnitude > 0);
            Animate(moveInput);
            moveVelocity = moveInput * moveSpeed;
        }
    }

    void FixedUpdate()
    {
        if (!isBlocked)
        {
            rb.velocity = moveVelocity;
        }
    }

    public void EndGame()
    {
        isBlocked = true;
        //return "Congratz, you survived " + GetDistance().ToString() + "m!";
    }

    public int GetDistance()
    {
        Vector3 distance = transform.position - startPos;
        return (int)distance.magnitude;
    }

    void moveChar(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // this needs to be rewritten, condition to show UI is no longer a trigger
    /*
    private void OnTriggerEnter(Collider col)
    {
        Respawn hit = col.gameObject.GetComponent<Respawn>();
        if (hit != null)
        {
            menuContainer.SetActive(true);
   
        }
    }*/

    private void Animate(Vector3 _moveInput)
    {
        float normalizedValue = _moveInput.magnitude > 0 ? 1 : 0;
        myAnimator.SetFloat("Blend", normalizedValue);
        transform.rotation = Quaternion.LookRotation(_moveInput);
    }
}
