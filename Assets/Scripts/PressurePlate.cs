﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PressurePlate : MonoBehaviour
{
    private AudioSource audioSource;
    public PlayerController player;
    public bool isEnabled = true;
    Collider[] boxCollisions;
    float boxCheckRadius = 0.5f;
    public LayerMask boxLayer;
    public DialogueTrigger dialogue; // Optional dialogue to trigger when the pressure plate is activated
    private bool dialogueTriggered = false;

    void Update()
    {
        if (isEnabled) {
            boxCollisions = Physics.OverlapSphere(transform.position, boxCheckRadius, boxLayer);

            if (boxCollisions.Length > 0) {
                isEnabled = false;
                PlaySound();
            } 
            else isEnabled = true;

            if (dialogue != null && !dialogueTriggered && !isEnabled)
            {
                dialogue.TriggerDialogue();
            }
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