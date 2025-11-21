using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class Movement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float sideSpeed = 2f;
    public float startDelay = 5f;   
    public float deathDelay = 5f;   
    public TextMeshProUGUI countdownText;

    public float horizontalSpeed = 3;
    public float rightLimit = 2f;
    public float leftLimit = -2f;

    private bool isGameActive = false;
    private Animator animator;
    private float countdownTimer;

    public GameObject goPanel;


    void Start()
    {
        animator = GetComponent<Animator>();

        // Comienza en Idle
        animator.SetBool("Idle", true);
        animator.SetBool("Run", false);

        countdownTimer = startDelay;
    }

    void Update()
    {
        if (!isGameActive)
        {
            // Actualiza la cuenta regresiva
            countdownTimer -= Time.deltaTime;

            if (countdownTimer > 0)
            {
                countdownText.text = Mathf.Ceil(countdownTimer).ToString();
            }
            else
            {
                StartGame();
            }
        }
        else
        {
            // Mueve al personaje hacia adelante
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // Si tocó en la mitad izquierda de la pantalla
                    if (touch.position.x < Screen.width / 2)
                    {
                        MoveLeft();
                    }
                    else // Mitad derecha
                    {
                        MoveRight();
                    }
                }
            }
        }
    }

    void StartGame()
    {
        if (isGameActive) return; // evita que se llame dos veces

        isGameActive = true;
        countdownText.text = ""; // Limpia el texto
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);

    }


    void MoveLeft()
    {
        Debug.Log("Touch Left");
        if (transform.position.x > leftLimit)
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
    }

    void MoveRight()
    {
        Debug.Log("Touch Right");
        if (transform.position.x < rightLimit)
            transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle");
            animator.SetBool("Run", false);
            animator.SetBool("Die", true);
            // Detiene el movimiento y muestra animación de muerte
            isGameActive = false;

            // Espera antes de ir al GameOver
            Invoke(nameof(GoToGameOver), deathDelay);
        }
    }

    void GoToGameOver()
    {
        goPanel.gameObject.SetActive(true);
    }
}
