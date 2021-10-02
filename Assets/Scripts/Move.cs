using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    public SphereCollider col;
    private float moveSpeed;
    private float dirX, dirZ;
    public float jumpForce;
    public LayerMask groundLayers;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3f;
        jumpForce = 5;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * moveSpeed;
        dirZ = Input.GetAxis("Vertical") * moveSpeed;

        if (IsGrounded())
        {
            Debug.Log("Grounded");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
