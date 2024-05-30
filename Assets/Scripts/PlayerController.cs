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

    public bool IsCrouching { get; private set; } = false;
    public bool IsGrounded { get; set; } = false;

    Collider[] bodyGroundCollisions;
    Collider[] bodyBoxCollisions;
    Collider[] enemyCollisions;
    Collider[] groundCollisions;
    Collider[] laserCollisions;
    Collider[] droneCollisions;
    Collider[] lightCollisions;
    private readonly float groundCheckRadius = 0.2f;
    public LayerMask boxLayer;
    public LayerMask droneLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public LayerMask laserLayer;
    public LayerMask lightLayer;
    public Transform groundCheck;
    public Transform bodyGroundCheck;
    public Transform StartingPoint;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
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
        isAlive = true;
        transform.position = StartingPoint.position;
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

        // Check for collisions with drones, enemies, lasers, and lights
        droneCollisions = Physics.OverlapSphere(myRB.position, 1.2f, droneLayer);
        enemyCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, enemyLayer);
        laserCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, laserLayer);
        lightCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, lightLayer);

        if (enemyCollisions.Length > 0 && lightCollisions.Length > 0 ||
            laserCollisions.Length > 0)
        {
            isAlive = false;
        }

        myAnim.SetBool("grounded", IsGrounded);

        float move = Input.GetAxis("Horizontal");

        if (droneCollisions.Length > 0 && move != 0)
        {
            isAlive = false;
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
}
