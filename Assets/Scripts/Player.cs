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
    
    public static readonly Vector3 SPAWN_POINT = new Vector3(0, 0.5f, 0);
    
    private bool isActive;
    
    private Rigidbody rb;
    private AudioSource jump;
    private bool allowJump;

    public bool IsActive
    {
        get => isActive;
        set => SetActive(value);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<AudioSource>();
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
            jump.Play();
        }
    }

    // collides with platform and self
    private void OnCollisionEnter(Collision other)
    {
        allowJump = true;
    }
    
    public void Out()
    {
        // prevents getting called more than once when 2 or more spike were touched
        if (enabled)
        {
            Freeze();
            StartCoroutine(Reset());
        }
        
        IEnumerator Reset()
        {
            yield return new WaitForSeconds(0.1f);
            CameraMain.reset.Invoke();
        }
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
        transform.position = SPAWN_POINT;
    }

    private void SetActive(bool value)
    {
        isActive = value;
        jump.enabled = value;
        rb.useGravity = value;
        enabled = value;
    }
}
