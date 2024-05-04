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
    bool grounded = false;
    Collider[] bodyGroundCollisions;
    Collider[] bodyBoxCollisions;
    Collider[] enemyCollisions;
    Collider[] groundCollisions;
    Collider[] laserCollisions;
    Collider[] lightCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask boxLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public LayerMask laserLayer;
    public LayerMask lightLayer;
    public Transform groundCheck;
    public Transform bodyGroundCheck;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        IsCrouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    void FixedUpdate()
    {
        bodyGroundCollisions = Physics.OverlapSphere(bodyGroundCheck.position, 1.2f, groundLayer);
        bodyBoxCollisions = Physics.OverlapSphere(groundCheck.position, 0.5f, boxLayer);
        if (grounded && Input.GetAxis("Jump")>0 && bodyBoxCollisions.Length < 1) {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        enemyCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, enemyLayer);
        laserCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, laserLayer);
        lightCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, lightLayer);
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0 || bodyBoxCollisions.Length > 0) grounded = true;
        else grounded = false;

        if (enemyCollisions.Length > 0 && lightCollisions.Length > 0) isAlive = false;
        if (laserCollisions.Length > 0) isAlive = false;

        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        if (bodyGroundCollisions.Length < 1 || groundCollisions.Length > 0) myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
        
        if (move>0 && !facingRight) Flip();
        else if (move<0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}
