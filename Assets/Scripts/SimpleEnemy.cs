using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    public Transform spriteTransform;
    public float scale = 3.0f;   
    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move the enemy
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        // Flip sprite direction based on movement
        if (direction > 0)
        {
            spriteTransform.localScale = new Vector3(-scale, scale, 1);
        }
        else if (direction < 0)
        {
            spriteTransform.localScale = new Vector3(scale, scale, 1);
        }

        // Reverse direction if moved far enough
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1;
        }
    }
}