using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public bool smoothFollow = true;
    public float smoothSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned to CameraFollowX script!");
            return;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y+5, player.position.z - 10);
        
        transform.position = targetPosition;
        
    }
}
