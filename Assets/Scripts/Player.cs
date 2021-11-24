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
    
    private bool isActive;
    
    private Rigidbody rb;
    private bool allowJump;

    public bool IsActive
    {
        get => isActive;
        set => SetActive(value);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isActive)
            return;
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    public void Jump()
    {
        if (allowJump)
        {
            rb.AddForce(new Vector3(0, jumpUpwardForce, jumpForwardForce), ForceMode.Impulse);
            allowJump = false;
        }
    }

    // collides with platform
    private void OnCollisionEnter(Collision other)
    {
        allowJump = true;
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
        IsActive = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Unfreeze()
    {
        IsActive = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        Unfreeze();
    }

    private void SetActive(bool value)
    {
        isActive = value;
        rb.useGravity = value;
    }
}
