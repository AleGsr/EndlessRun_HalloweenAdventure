using UnityEngine;
using TMPro; // Para el texto UI moderno

public class CandyCollector : MonoBehaviour
{
    public TextMeshProUGUI candyText; 
    private int candyCount = 0;

    private void Start()
    {
        UpdateCandyText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candy")) 
        {
            candyCount++;
            UpdateCandyText();

           
            Destroy(other.gameObject);
        }
    }

    void UpdateCandyText()
    {
        candyText.text = " " + candyCount;
    }
}
