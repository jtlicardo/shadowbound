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

    private bool facingRight;
    private bool canDie = true;

    public bool IsCrouching { get; private set; } = false;
    public bool IsGrounded { get; set; } = false;

    public AudioClip moveSound;
    private AudioSource audioSource;

    Collider[] bodyGroundCollisions;
    Collider[] bodyBoxCollisions;
    Collider[] enemyCollisions;
    Collider[] groundCollisions;
    Collider[] laserCollisions;
    Collider[] droneCollisions;
    Collider[] lightCollisions;
    Collider[] securityCameraCollisions;
    private readonly float groundCheckRadius = 0.2f;
    public LayerMask boxLayer;
    public LayerMask droneLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public LayerMask laserLayer;
    public LayerMask lightLayer;
    public LayerMask securityCameraLayer;
    public Transform groundCheck;
    public Transform bodyGroundCheck;
    public Transform StartingPoint;
    private CapsuleCollider capsuleCollider;

    private float securityCameraDetectionTimer = 0f;
    private bool isDetectedByCamera = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        facingRight = true;
        respawn();
        //transform.Translate(StartingPoint ? StartingPoint.localPosition : Vector3.zero, Space.Self);
    }

    public void setCheckpoint(Vector3 position) {
        StartingPoint.position = position;
    }

    public void respawn() {
        transform.position = StartingPoint.position;
        isAlive = true;
        StartCoroutine(revive());
    }

    private void die() {
        if (canDie) {
            isAlive = false;
            canDie = false;
        }
    }

    IEnumerator revive()
    {
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("test"); 
        canDie = true;
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
        laserCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, laserLayer);
        lightCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, lightLayer);
        securityCameraCollisions = Physics.OverlapSphere(myRB.position, 1.2f, securityCameraLayer);

        if (enemyCollisions.Length > 0 && lightCollisions.Length > 0 ||
            laserCollisions.Length > 0)
        {
            die();
        }

        myAnim.SetBool("grounded", IsGrounded);

        float move = Input.GetAxis("Horizontal");

        if (move != 0 && IsGrounded) PlaySound(moveSound);
        else audioSource.Stop();

        if (droneCollisions.Length > 0 && move != 0)
        {
            die();
        }

        // The player is detected by the security camera if standing in the camera's field of view for more than 1 second
        if (lightCollisions.Length > 0 && securityCameraCollisions.Length > 0)
        {
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
        if (bodyGroundCollisions.Length < 1 || groundCollisions.Length > 0) myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }

        void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up, 180.0f, Space.World);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}
