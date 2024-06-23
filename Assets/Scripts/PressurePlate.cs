﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PressurePlate : MonoBehaviour
{
    private AudioSource audioSource;
    public PlayerController player;
    public bool isEnabled = true;
    public GameObject Box;
    Collider[] boxCollisions;
    float boxCheckRadius = 0.5f;
    public LayerMask boxLayer;

    void Update()
    {
        if (isEnabled) {
            boxCollisions = Physics.OverlapSphere(transform.position, boxCheckRadius, boxLayer);

            if (boxCollisions.Length > 0) {
                isEnabled = false;
                PlaySound();
            } 
            else isEnabled = true;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}