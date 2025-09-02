using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    private float lifetime = 2f; // sahnede kalacağı süre
    private bool isCountingDown = false;
    public bool isHit = false;

    private void OnEnable()
    {
        // Her spawn olduğunda lifetime resetlenir
        lifetime = 2f;
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
            lifetime -= Time.deltaTime;

            if (lifetime <= 0f || isHit)
            {
                // Rastgele respawn süresi seç
                float respawnTime = Random.Range(2f, 10f);
                TargetManager.Instance.RespawnTarget(gameObject, respawnTime);

                Animator anim = GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("Disappear");
                    StartCoroutine(DisableAfterAnim());
                }
                else
                {
                    gameObject.SetActive(false);
                }


                // Reset
                isHit = false;
                isCountingDown = false;
            }
        }
    }

    private IEnumerator DisableAfterAnim()
    {
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}
