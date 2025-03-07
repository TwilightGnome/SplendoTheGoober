using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool verticle = true; 
    public float speed = 2f;
    public float moveDistance = 3f;
    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (verticle){
            transform.position += Vector3.up * direction * speed * Time.deltaTime;

            if (Mathf.Abs(transform.position.y - startPos.y) >= moveDistance)
            {
                direction *= -1; // Reverse direction
            }
        }else{
            transform.position += Vector3.right * direction * speed * Time.deltaTime;

            if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
            {
                direction *= -1; // Reverse direction
            }
        }         
    }
}
