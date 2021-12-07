using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalRuby.Tween;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpUpwardForce = 5f;
    [SerializeField] private float jumpForwardForce = 3f;
    [SerializeField] private Transform mesh;
    
    public static readonly Vector3 SPAWN_POINT = new Vector3(0, 0.5f, 0);
    
    private bool isActive;
    
    private Rigidbody rb;
    private AudioSource jump;
    private BoxCollider collider;
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
        collider = GetComponent<BoxCollider>();
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
            RotateOnce();
            jump.Play();
            allowJump = false;
        }
    }

    private void RotateOnce()
    {
        void Rotate(ITween<Quaternion> t)
        {
            if(mesh) mesh.gameObject.transform.rotation = t.CurrentValue;
        }

        void Finish(ITween t)
        {
            ResetRotation();
        }
        
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(new Vector3(0,180,0));
        mesh.gameObject.Tween($"{mesh.gameObject.GetInstanceID()}_rotate", from, to, 1, TweenScaleFunctions.CubicEaseOut,
            Rotate, Finish);
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
        ResetRotation();
    }

    public void Unfreeze()
    {
        IsActive = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        ResetRotation();
    }

    public void Reset()
    {
        ResetRotation();
        transform.position = SPAWN_POINT;
    }

    private void SetActive(bool value)
    {
        isActive = value;
        jump.enabled = value;
        rb.useGravity = value;
        enabled = value;
    }

    private void ResetRotation()
    {
        if(mesh) mesh.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
