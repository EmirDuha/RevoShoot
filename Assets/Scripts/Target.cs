using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private float currentLifeTime;
    [SerializeField] private float minRespawnTime = 2f;
    [SerializeField] private float maxRespawnTime = 10f;
    private bool isCountingDown = false;
    public bool isHit = false;

    private void OnEnable()
    {
        currentLifeTime = lifeTime;
        isCountingDown = true;
    }

    private void Update()
    {
        TargetDisappear();
    }

    private void TargetDisappear()
    {
        if (isCountingDown)
        {
            currentLifeTime -= Time.deltaTime;

            if (currentLifeTime <= 0f || isHit)
            {
                float respawnTime = Random.Range(minRespawnTime, maxRespawnTime);
                TargetManager.Instance.RespawnTarget(gameObject, respawnTime);

                Animator anim = GetComponent<Animator>();
                if (anim != null)
                {
                    anim.Play("TargetDisappear");
                    StartCoroutine(DisableAfterAnim());
                }
                else
                {
                    gameObject.SetActive(false);
                }
                isHit = false;
                isCountingDown = false;
            }
        }
    }

    private IEnumerator DisableAfterAnim()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
