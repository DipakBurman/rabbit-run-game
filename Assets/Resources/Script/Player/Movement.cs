using System.Collections;
using UnityEngine;

// Drives the player with the joystick, tracks the score, and shows the
// win / lose screens when the game ends.
public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    public float speed;

    [SerializeField] private float score = 0f;
    [SerializeField] private float WinScore = 10f;

    public GameObject youwin;
    public GameObject youlose;

    private Joystick JoystickRef;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool isGameOver;

    private float xinput;
    private float yinput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        // Keep the rabbit upright when it bumps the enemy's solid collider.
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

    
        // Cache the result-screen objects so they can be shown when the game ends.
        if (youwin == null) youwin = GameObject.FindWithTag("Youwin");
        if (youlose == null) youlose = GameObject.FindWithTag("Youlose");

        // Keep the result screens hidden until the player wins or loses.
        if (youwin != null) youwin.SetActive(false);
        if (youlose != null) youlose.SetActive(false);
    }

    private void Start()
    {
        if (joystick != null)
        {
            JoystickRef = joystick.GetComponent<Joystick>();
        }
    }

    private void FixedUpdate()
    {
        if (isGameOver || JoystickRef == null || rb == null) return;

        // Read both joystick axes for movement.
        xinput = JoystickRef.Horizontal;
        yinput = JoystickRef.Vertical;

        Vector2 input = new Vector2(xinput, yinput);
        if (input.sqrMagnitude > 1f) input.Normalize();

        Vector2 nextPosition = rb.position + input * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }

    // Collectibles can be triggers; the enemy uses a solid collider.
    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleContact(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleContact(other.gameObject);
    }

    private void HandleContact(GameObject other)
    {
        if (isGameOver || other == null) return;

        // Add collected points to the score and trigger a win at the target score.
        if (other.CompareTag("Point"))
        {
            Destroy(other);
            score++;

            if (score >= WinScore)
            {
                StartCoroutine(YouWin());
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            StartCoroutine(YouLose());
        }
    }



    // Ends gameplay and shows the win screen on the next frame.
    private IEnumerator YouWin()
    {
        EndGameplay();
        yield return null;
        if (youwin != null) youwin.SetActive(true);
    }

    // Ends gameplay, hides the player, then shows the lose screen after a short delay.
    private IEnumerator YouLose()
    {
        EndGameplay();

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;
        if (playerCollider != null) playerCollider.enabled = false;

        yield return new WaitForSeconds(2f);
        if (youlose != null) youlose.SetActive(true);
    }

    private void EndGameplay()
    {
        if (isGameOver) return;

        isGameOver = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (SpawnerandMove.instance != null)
        {
            SpawnerandMove.instance.StopGameplay();
        }

        PointSpawner pointSpawner = FindAnyObjectByType<PointSpawner>();
        if (pointSpawner != null)
        {
            pointSpawner.StopGameplay();
        }
    }
}
