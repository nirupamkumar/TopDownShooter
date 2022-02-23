using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player move
    [SerializeField] private float playerSpeed = 5f;

    //Jump
    public Rigidbody rb;
    public bool isGrounded;
    [SerializeField] private float jumpSpeed = 3f;

    public GameObject playerMesh;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = playerMesh.GetComponent<Animator>();
    }

    private void OnCollisionEnter()
    {
        isGrounded = true;
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
            animator.SetBool("isMoving", true);
        else 
            animator.SetBool("isMoving", false);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                animator.SetBool("isJumping", true);
                PlayerJump();
                isGrounded = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) animator.SetBool("isJumping", false);
    }

    void PlayerMovement()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 playerMove = new Vector3(horizontal, 0f, vertical) * playerSpeed * Time.deltaTime;
        transform.Translate(playerMove, Space.World);
        
    }

    void PlayerJump()
    {
        rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpSpeed, ForceMode.Impulse);
    }

    
    
}
