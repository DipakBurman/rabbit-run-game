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
        // Locate the result panels once during initialization.
        youwin= GameObject.FindWithTag("Youwin");
        youlose= GameObject.FindWithTag("Youlose");

        // Start with both panels hidden until the game reaches an outcome.
        youwin.SetActive(false);
        youlose.SetActive(false);

    }

    void Start()
    {   
        JoystickComp = joystick.GetComponent<Joystick>();
    }


    void Update()
    {   
        // Read the current joystick direction each frame.
        xinput = JoystickComp.Horizontal;
        yinput = JoystickComp.Vertical;

        transform.Translate(new Vector2 (xinput, yinput).normalized * speed * Time.deltaTime);
    }


    // Handles collectible and enemy collisions.
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Collect the point and check whether the winning score has been reached.
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

        // Begin the lose sequence after an enemy collision.
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(YouLose());
        }
    }

    // Gives the final game moment time to play before showing the win panel.
    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(2f); 
        youwin.SetActive(true);
    }

    // Hides the player immediately, then displays the lose panel after a short delay.
    // Keeping this GameObject alive allows the coroutine to complete.
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
