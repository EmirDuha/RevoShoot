using UnityEngine;
using System.Collections;

public enum TargetType
{
    Default,
    RightSided
}

public class Target : MonoBehaviour
{


    [Header("Target Lifetime")]
    [SerializeField] private float lifetime = 4f;
    private float currentLifetime;
    private bool isCountingDown = false;
    public bool isDown = false;

    [Header("Time Settings")]
    [SerializeField] private float minRespawnTime = 2f;
    [SerializeField] private float maxRespawnTime = 5f;

    [Header("Colliders and Animation")]
    private Collider col;
    private Transform pivotTransform;
    private Quaternion initialRotation;
    private float animAngle = 90f;

    [Header("Target Type")]
    [SerializeField] private TargetType targetType;

    private void Awake()
    {
        col = GetComponent<Collider>();

        pivotTransform = transform.parent;
    }

    private void OnEnable()
    {
        initialRotation = pivotTransform.localRotation;
        ResetTarget();
    }

    private void Update()
    {
        if (isCountingDown && !isDown)
        {
            currentLifetime -= Time.deltaTime;

            if (currentLifetime <= 0f)
            {
                TargetDown();
            }
        }
    }

    private void TargetDown()
    {
        if (!isDown)
        {
            isDown = true;
            isCountingDown = false;

            if (col != null) col.enabled = false;

            if (targetType == TargetType.Default)
                StartCoroutine(FallOverAnim());
            else if (targetType == TargetType.RightSided)
                StartCoroutine(FallOverAnimRight());

            TargetManager.Instance.RespawnTarget(this, Random.Range(minRespawnTime, maxRespawnTime));
        }
    }

    private IEnumerator FallOverAnim()
    {
        Quaternion startRot = pivotTransform.localRotation;
        Quaternion targetRot = startRot * Quaternion.Euler(animAngle, 0f, 0f);

        float animTime = 0f;
        while (animTime < 1f)
        {
            animTime += Time.deltaTime * 10f;
            pivotTransform.localRotation = Quaternion.Slerp(startRot, targetRot, animTime);
            yield return null;
        }
    }

    private IEnumerator FallOverAnimRight()
    {
        Quaternion startRot = pivotTransform.localRotation;
        Quaternion targetRot = startRot * Quaternion.Euler(0f, 0f, animAngle / 2 + 10);

        float animTime = 0f;
        while (animTime < 1f)
        {
            animTime += Time.deltaTime * 10f;
            pivotTransform.localRotation = Quaternion.Slerp(startRot, targetRot, animTime);
            yield return null;
        }
    }

    public IEnumerator StandUpAnim()
    {
        Quaternion startRot = pivotTransform.localRotation;
        Quaternion targetRot = initialRotation;

        float animTime = 0f;
        while (animTime < 1f)
        {
            animTime += Time.deltaTime * 10f;
            pivotTransform.localRotation = Quaternion.Slerp(startRot, targetRot, animTime);
            yield return null;
        }

        ResetTarget();
    }

    public void ResetTarget()
    {
        currentLifetime = lifetime;
        isDown = false;
        isCountingDown = true;

        if (col != null) col.enabled = true;
    }

    public void GetShot()
    {
        if (!isDown)
        {
            TargetDown();
            TargetManager.Instance.score += 100;
        }
    }
}
