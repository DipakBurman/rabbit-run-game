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
    [SerializeField] private GameObject YouWinText;
    [SerializeField] private GameObject YouLoseText;


    void Start()
    {
        JoystickComp = joystick.GetComponent<Joystick>();
        YouWinText.SetActive(false);
        YouLoseText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        xinput = JoystickComp.Horizontal;
        yinput = JoystickComp.Vertical;

        transform.Translate(new Vector2 (xinput, yinput).normalized * speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Point")
        {
            Destroy(other.gameObject);
            score++;
            if (score == WinScore)
            {
                StartCoroutine(YouWin());
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            StartCoroutine(YouLose());
        }
    }

    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(2f);
        YouWinText.SetActive(true);
    }

    public IEnumerator YouLose()
    {
        yield return new WaitForSeconds(2f);
        YouLoseText.SetActive(true);
    }
}
