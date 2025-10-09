using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image VolumeIcon;
    [SerializeField] private Sprite[] volumeIcons;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        VolumeIconChanger();
    }

    private void VolumeIconChanger()
    {
        switch (AudioListener.volume)
        {
            case 0f:
                ChangeSprite(0);
                break;
            case > 0f and < 0.3f:
                ChangeSprite(1);
                break;
            case >= 0.3f and < 0.6f:
                ChangeSprite(2);
                break;
            case >= 0.6f and <= 1f:
                ChangeSprite(3);
                break;
        }
    }

    private void ChangeSprite(int index)
    {
        VolumeIcon.sprite = volumeIcons[index];
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
}
