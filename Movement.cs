using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    private Joystick JoystickComp;
    float xinput;
    float yinput;
    public float speed;

    [SerializeField] private float score = 0f;
    [SerializeField] private float WinScore = 10f;

    public GameObject youwin;
    public GameObject youlose;

    public float TotalScore;


    void Awake()
    {   
        // Cache the result-screen objects so they can be shown when the game ends.
        youwin= GameObject.FindWithTag("Youwin");
        youlose= GameObject.FindWithTag("Youlose");

        // Keep the result screens hidden until the player wins or loses.
        youwin.SetActive(false);
        youlose.SetActive(false);

    }

    void Start()
    {   
        JoystickComp = joystick.GetComponent<Joystick>();
    }


    void Update()
    {   
        // Read both joystick axes for movement.
        xinput = JoystickComp.Horizontal;
        yinput = JoystickComp.Vertical;

        transform.Translate(new Vector2 (xinput, yinput).normalized * speed * Time.deltaTime);
    }


    // Responds when the player collides with a collectible or an enemy.
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Add collected points to the score and trigger a win at the target score.
        if (other.gameObject.tag == "Point")
        {
            Destroy(other.gameObject);
            score++;
            TotalScore = score;

            if (TotalScore == WinScore)
            {
                StartCoroutine(YouWin());
            }
        }

        // End the run when the player touches an enemy.
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(YouLose());
        }
    }

    // Waits briefly before displaying the win screen.
    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(2f); 
        youwin.SetActive(true);
    }

    // Waits briefly before displaying the lose screen.
    // The player is hidden instead of destroyed so this coroutine can keep running;
    // destroying the GameObject would also kill this coroutine before the screen shows.
    public IEnumerator YouLose()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(2f);
        youlose.SetActive(true);
    }
}
