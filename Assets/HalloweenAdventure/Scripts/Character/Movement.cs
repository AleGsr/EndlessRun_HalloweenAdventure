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

    private bool isGameActive = false;
    private Animator animator;
    private float countdownTimer;

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
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
                    MovLateral();
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

    void MovLateral()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * sideSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);
        }
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
        SceneManager.LoadScene("GameOver");
    }
}
