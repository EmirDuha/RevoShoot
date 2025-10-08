using UnityEngine;
using TMPro;
using System.Collections;
using NUnit.Framework;

public class CountDownText : MonoBehaviour
{
    [SerializeField] private float countDownTime = 3.0f;
    [SerializeField] private TextMeshProUGUI countDownText;

    public bool isCountingDown = false;

    private void Start()
    {
        StartCoroutine(CountDown());

    }

    private void Update()
    {

    }

    private IEnumerator CountDown()
    {
        float currentTime = countDownTime;
        isCountingDown = true;

        while (currentTime > 0)
        {
            countDownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }

        countDownText.text = "START!";
        yield return new WaitForSeconds(1.0f);
        isCountingDown = false;
        Debug.Log("Countdown finished!");
        countDownText.gameObject.SetActive(false);

    }
}
