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
        // Find the "Youwin" and "Youlose" GameObjects in the scene using their tags
        youwin= GameObject.FindWithTag("Youwin");
        youlose= GameObject.FindWithTag("Youlose");

        // Set the "Youwin" and "Youlose" variables to inactive at the start of the game
        youwin.SetActive(false);
        youlose.SetActive(false);

    }

    void Start()
    {   
        // JoystickComp = joystick.GetComponent<Joystick>();
        JoystickComp = joystick.GetComponent<Joystick>();
    }


    void Update()
    {   
        // Get the horizontal and vertical input values from the joystick component
        xinput = JoystickComp.Horizontal;
        yinput = JoystickComp.Vertical;

        transform.Translate(new Vector2 (xinput, yinput).normalized * speed * Time.deltaTime);
    }


// This method is called when the player collides with points or enemies.
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collided object has the tag "Point"
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

        // Check if the collided object has the tag "Enemy"
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            StartCoroutine(YouLose());
        }
    }

// This coroutine is called when the player wins the game.
    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(2f); 
        youwin.SetActive(true);
    }

// This coroutine is called when the player loses the game.
     public IEnumerator YouLose()
    {
        yield return new WaitForSeconds(2f);
        youlose.SetActive(true);
    }
}
