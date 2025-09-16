using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections;

public class Revolver : MonoBehaviour
{
    [Header("Rotation Data")]
    [SerializeField] private Transform revolverTransform;
    [SerializeField] private float revolverRotationSpeed = 10f;

    [Header("Aim Data")]
    [SerializeField] private Transform aimTransform;
    [SerializeField] private LayerMask aimColliderLayerMask;  

    [Header("Shoot Data")]
    [SerializeField] private float shootDistance = 100f;
    [SerializeField] private float shootCooldown = 0.5f;
    private float lastShootTime = -999f;
    private float startDelay = 2f;

    private RaycastHit currentHit;

    [Header("Sound Data")]
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource breakAudioSource;

    [Header("Text Data")]
    [SerializeField] private float gameTime = 30f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        lastShootTime = Time.time + startDelay;
    }

    private void Update()
    {
        AimUpdate();
        CheckInputs();
        RevolverRotation();
        TimerUpdate();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }


    private void Shoot()
    {
        shootAudioSource.Play();

        if (currentHit.collider != null && currentHit.collider.CompareTag("TargetHead"))
        {
            Transform parent = currentHit.collider.transform.parent;

            StartCoroutine(PlayBreakSoundDelayed(0.1f));

            Target targetScript = parent.GetComponent<Target>();
            if (targetScript != null)
            {
                targetScript.GetShot();
                scoreText.text = "Score: " + TargetManager.Instance.score;
            }
        }
        else
        {
            //Misfire
        }
    }

    private IEnumerator PlayBreakSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        breakAudioSource.Play();
    }

    private void AimUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out currentHit, shootDistance, aimColliderLayerMask))
        {
            aimTransform.position = currentHit.point;
        }
        else
        {
            aimTransform.position = ray.GetPoint(shootDistance);
            currentHit = new RaycastHit();
        }
    }

    private void RevolverRotation()
    {
        Vector3 direction = aimTransform.position - revolverTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        revolverTransform.rotation = Quaternion.Slerp(
            revolverTransform.rotation,
            targetRotation,
            revolverRotationSpeed * Time.deltaTime
        );
    }

    private void TimerUpdate()
    {
        float time = Mathf.Max(0f, gameTime - Time.timeSinceLevelLoad);
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (time <= 0f)
        {
            timeText.text = "00:00";
            PlayerPrefs.SetInt("Score", TargetManager.Instance.score);
            PlayerPrefs.Save();

            GameOverLoad();
        }
    }

    private void GameOverLoad()
    {
        SceneManager.LoadScene("GameOver");
    }
}
