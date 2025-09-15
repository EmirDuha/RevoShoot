using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RespawnTarget(GameObject target, float delay)
    {
        StartCoroutine(RespawnCoroutine(target, delay));
    }

    private IEnumerator RespawnCoroutine(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(true);

        Animator anim = target.GetComponent<Animator>();
        if (anim != null && gameObject.CompareTag("Target"))
        {
            anim.Play("TargetSpawn", -1, 0f);
        }
        else if (anim != null && gameObject.CompareTag("RightSidedTarget"))
        {
            anim.Play("RightSidedTargetSpawn");
        }
    }
}
