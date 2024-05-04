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

    private bool IsGrounded { get; set; } = false;
    public Transform groundCheck;
    private readonly float groundCheckRadius = 0.2f;

    public bool IsCrouching { get; private set; } = false;

    private Collider[] enemyCollisions;
    private Collider[] groundCollisions;
    private Collider[] laserCollisions;
    private Collider[] lightCollisions;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public LayerMask laserLayer;
    public LayerMask lightLayer;

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
        if (IsGrounded && Input.GetAxis("Jump") > 0) {
            IsGrounded = false;
            myAnim.SetBool("grounded", IsGrounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        enemyCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, enemyLayer);
        laserCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, laserLayer);
        lightCollisions = Physics.OverlapSphere(myRB.position, groundCheckRadius, lightLayer);
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0) IsGrounded = true;
        else IsGrounded = false;

        if (enemyCollisions.Length > 0 && lightCollisions.Length > 0) isAlive = false;
        if (laserCollisions.Length > 0) isAlive = false;

        myAnim.SetBool("grounded", IsGrounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}
