using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;

    public float runSpeed;

    public float jumpHeight;

    private Rigidbody myRB;
    private Animator myAnim;

    public bool facingRight;
    private bool canDie = true;

    public bool IsCrouching { get; private set; } = false;
    public bool IsGrounded { get; set; } = false;

    public AudioClip[] deathSounds;
    private int currentDeathSoundIndex = 0; 
    public AudioClip moveSound;
    private AudioSource audioSource;

    Collider[] bodyGroundCollisions;
    Collider[] bodyBoxCollisions;
    Collider[] enemyCollisions;
    Collider[] groundCollisions;
    Collider[] laserCollisions;
    Collider[] droneCollisions;
    Collider[] lightCollisions;
    Collider[] darknessCollisions;
    Collider[] securityCameraCollisions;
    private readonly float groundCheckRadius = 0.2f;
    public LayerMask boxLayer;
    public LayerMask droneLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public LayerMask laserLayer;
    public LayerMask lightLayer;
    public LayerMask darknessLayer;
    public LayerMask securityCameraLayer;
    public Transform groundCheck;
    public Transform bodyGroundCheck;

    private float securityCameraDetectionTimer = 0f;
    private bool isDetectedByCamera = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        Debug.Log("Player Start: Setting initial checkpoint at player at " + transform.position);
        GameManager.Instance.SetCheckpoint(transform.position, transform.rotation, facingRight);

        respawn();
    }

    public void respawn()
    {
        isAlive = true;
        StartCoroutine(revive());
    }

    private void die() {
        PlayDeathSound();
        if (canDie) {
            isAlive = false;
            canDie = false;
        }
    }

    IEnumerator revive()
    {
        yield return new WaitForSecondsRealtime(1);
        canDie = true;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Update()
    {
        IsCrouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    void FixedUpdate()
    {
        bodyGroundCollisions = Physics.OverlapSphere(bodyGroundCheck.position, 1.2f, groundLayer);
        bodyBoxCollisions = Physics.OverlapSphere(groundCheck.position, 0.5f, boxLayer);

        if (IsGrounded && Input.GetAxis("Jump") > 0 && bodyBoxCollisions.Length < 1)
        {
            IsGrounded = false;
            myAnim.SetBool("grounded", IsGrounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        IsGrounded = groundCollisions.Length > 0 || bodyBoxCollisions.Length > 0;

        // Check for collisions with security cameras, drones, enemies, lasers, and lights
        droneCollisions = Physics.OverlapSphere(myRB.position, 1.2f, droneLayer);
        enemyCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, enemyLayer);

        laserCollisions = Physics.OverlapBox(new Vector3(myRB.position.x, myRB.position.y * 1.05f, myRB.position.z), new Vector3(1, 1, 1), myRB.rotation, laserLayer);

        lightCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, lightLayer);
        darknessCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, darknessLayer);
        securityCameraCollisions = Physics.OverlapSphere(myRB.position, 1.2f, securityCameraLayer);

        if (enemyCollisions.Length > 0 && lightCollisions.Length > 0 ||
            laserCollisions.Length > 0)
        {
            die();
        }

        myAnim.SetBool("grounded", IsGrounded);

        float move = Input.GetAxis("Horizontal");

        if (move != 0 && IsGrounded && isAlive) PlaySound(moveSound);
        else if (isAlive) audioSource.Stop();

        if (droneCollisions.Length > 0 && move != 0)
        {
            die();
        }

        // The player is detected by the security camera if standing in the camera's field of view for more than 1 second
        // If the player has an overlap with the darkness layer, the camera should not detect the player
        if (securityCameraCollisions.Length > 0 && darknessCollisions.Length == 0)
        {
            Debug.Log("Detected by camera");
            if (!isDetectedByCamera)
            {
                isDetectedByCamera = true;
                securityCameraDetectionTimer = Time.fixedTime;
            }
            else if (Time.fixedTime - securityCameraDetectionTimer > 1.0f)
            {
                die();
            }
        }
        else
        {
            isDetectedByCamera = false;
            securityCameraDetectionTimer = 0f;
        }

        myAnim.SetFloat("speed", Mathf.Abs(move));
        if (bodyGroundCollisions.Length < 1 || groundCollisions.Length > 0)
        {
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
        }

        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.volume = 0.2f; 
        audioSource.pitch = 1.7f; 
        if (audioSource.isPlaying && audioSource.clip == clip)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        if (deathSounds.Length == 0)
        {
            Debug.LogWarning("No death sounds assigned!");
            return;
        }

        audioSource.volume = 0.7f; 
        audioSource.pitch = 1.0f; 
        audioSource.clip = deathSounds[currentDeathSoundIndex];
        audioSource.Play();

        currentDeathSoundIndex = (currentDeathSoundIndex + 1) % deathSounds.Length;
    }
}
