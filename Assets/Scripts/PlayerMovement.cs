using UnityEngine;
using UnityEngine.XR;

using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    // Components
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    public bool isGrounded;
    public Animator animator;
    
    // Initialize variables
    private float movement;
    private float movementSpeed = 8f; 
    private bool facingRight = true;
    
    void Start()
    {
        // Grab components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        
        // Flip the player based on their direction
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
            
        } else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
        
        // Jump Mechanic
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            animator.SetBool("Jump", true);
        }
        
        // Animation
        if (Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("Walk", 1f);
        }
        else if (Mathf.Abs(movement) < 0.1f)
        {
            animator.SetFloat("Walk", 0f);
        }
        
        // Attacking
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack"); 
        }
    }
    
    private void FixedUpdate()
    {
        float deltaMovement = movement * Time.deltaTime * movementSpeed; // Cache the value
        transform.position += new Vector3(deltaMovement, 0f, 0f);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        isGrounded = true;
        animator.SetBool("Jump", false);
    }
}