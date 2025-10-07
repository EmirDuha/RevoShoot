using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private string targetScene;

    public void RestartGame()
    {
        SceneManager.LoadScene(targetScene);
        Time.timeScale = 1f;
    }
}
