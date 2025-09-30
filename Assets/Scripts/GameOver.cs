using UnityEngine;
using TMPro;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private int textScore = 0;

    void Start()
    {
        
    }

    void Update()
    {
        StartScoreCount();
    }

    public void StartScoreCount()
    {
        StartCoroutine(ScoreCount());
    }

    private IEnumerator ScoreCount()
    {
        yield return new WaitForSeconds(1f);

        while (textScore <= PlayerPrefs.GetInt("Score", 0))
        {
            finalScoreText.text = "Your Score: " + textScore.ToString();
            textScore++;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
