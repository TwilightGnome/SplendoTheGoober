using UnityEngine;


public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    public float jumpForce;
    private Vector3 startPosition;
    public float dashForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.linearVelocity.y, Input.GetAxis("Vertical") * moveSpeed);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            theRB.linearVelocity = new Vector3(theRB.linearVelocity.x, jumpForce, theRB.linearVelocity.z);
         
        }

        if(theRB.transform.position.y < -10)
        {
            Respawn();
        }
    }

    bool IsGrounded()
    {
        float GroundedDistance = 2f;

        if (theRB.linearVelocity.y == 0)
        {
            return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GroundedDistance);
        }
        else return false;
    }

    void Respawn()
    {
        transform.position = startPosition; // Reset player to start position
        theRB.linearVelocity = Vector3.zero; // Stop any movement
    }

}
