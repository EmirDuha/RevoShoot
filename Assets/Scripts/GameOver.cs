using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalScoreText.text = "Final Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NumberRaise()
    {
        
    }
}
