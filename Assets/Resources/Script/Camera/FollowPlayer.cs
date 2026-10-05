using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;

    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 followplayer = player.position + offset;
        Vector3 smoothfollow = Vector3.Lerp(transform.position, followplayer, smoothSpeed * Time.deltaTime);
        transform.position = smoothfollow;
    }
}