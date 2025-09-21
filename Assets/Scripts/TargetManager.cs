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

    public void RespawnTarget(Target target, float delay)
    {
        StartCoroutine(RespawnCoroutine(target, delay));
    }

    private IEnumerator RespawnCoroutine(Target target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            yield return target.StandUpAnim();
        }
    }
}
