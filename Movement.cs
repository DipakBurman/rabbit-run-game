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
        // Cache both result panels to avoid repeated scene lookups.
        youwin= GameObject.FindWithTag("Youwin");
        youlose= GameObject.FindWithTag("Youlose");

        // Hide the result UI until the player wins or loses.
        youwin.SetActive(false);
        youlose.SetActive(false);

    }

    void Start()
    {   
        JoystickComp = joystick.GetComponent<Joystick>();
    }


    void Update()
    {   
        // Read the joystick direction used for this frame's movement.
        xinput = JoystickComp.Horizontal;
        yinput = JoystickComp.Vertical;

        transform.Translate(new Vector2 (xinput, yinput).normalized * speed * Time.deltaTime);
    }


    /// <summary>
    /// Updates the score when a point is collected and starts the appropriate
    /// result sequence when the player reaches the target or hits an enemy.
    /// </summary>
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Remove the collected point before updating the player's score.
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

        // Keep the player object alive so the lose coroutine can finish.
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(YouLose());
        }
    }

    /// <summary>
    /// Displays the win panel after a short delay.
    /// </summary>
    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(2f); 
        youwin.SetActive(true);
    }

    /// <summary>
    /// Hides and disables the player, then displays the lose panel after a short delay.
    /// </summary>
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
