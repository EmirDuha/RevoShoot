using UnityEngine;

public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(VolumeChanger);
    }

    private void VolumeChanger(float value)
    {
        AudioListener.volume = value;
    }
}