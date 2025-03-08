using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    private Vector3 startPos;
    private int direction = 1;

    private Vector3 moveDirection;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
        moveDirection.y = -0.1f;

        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1; // Reverse direction
        }
    }
}
