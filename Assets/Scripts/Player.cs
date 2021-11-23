using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpUpwardForce = 5f;
    [SerializeField] private float jumpForwardForce = 3f;
    [HideInInspector] public bool isActive;
    private Rigidbody rb;
    private int jumpCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isActive = true;
    }

    void Update()
    {
        if (!isActive)
            return;
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
            Jump();
    }

    private void Jump()
    {
        // allows double jump
        jumpCount += 1;
        if (jumpCount > 1) jumpForwardForce = 0;
        if (jumpCount < 3) rb.AddForce(new Vector3(0, jumpUpwardForce, jumpForwardForce), ForceMode.Impulse);
    }

    // collides with platform
    private void OnCollisionEnter(Collision other)
    {
        jumpCount = 0;
    }
    
    public void Out()
    {
        isActive = false;
        enabled = false;
        Freeze();
        CameraMain.zoomOut.Invoke();
    }

    public void Freeze()
    {
        isActive = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Unfreeze()
    {
        isActive = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
