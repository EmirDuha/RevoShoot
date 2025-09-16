using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance { get; private set; }
    public int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void RespawnEnemy(Target enemy, float delay)
    {
        StartCoroutine(RespawnCoroutine(enemy, delay));
    }

    private IEnumerator RespawnCoroutine(Target enemy, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (enemy != null)
        {
            yield return enemy.StandUpAnim();
        }
    }
}
