using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Customs()
    {
        SceneManager.LoadScene("Customs");
    }


    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameOut()
    {
        Application.Quit();
    }
}
