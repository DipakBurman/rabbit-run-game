using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;   

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        
    }
}
