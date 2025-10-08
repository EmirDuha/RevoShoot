using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        Slider();
    }

    private void Slider()
    {
        slider.value = AudioListener.volume;
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
}
