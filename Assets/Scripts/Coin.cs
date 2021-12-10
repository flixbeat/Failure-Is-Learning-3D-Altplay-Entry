using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float rotationSpeed = 300f;
    private AudioSource audioSource;
    private MeshRenderer mesh;
    private Collider collider;
    
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        mesh.enabled = false;
        collider.enabled = false;
    }

    public void Enable()
    {
        mesh.enabled = true;
        collider.enabled = true;
    }
}
