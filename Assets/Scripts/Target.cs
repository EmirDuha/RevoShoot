using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using Unity.Collections;

public enum TargetType
{
    Default,
    RightSided,
    Moving
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
                StartCoroutine(FallOverAnim(animAngle, "x"));
            else if (targetType == TargetType.RightSided)
                StartCoroutine(FallOverAnim(animAngle, "z"));
            else if (targetType == TargetType.Moving)
                StartCoroutine(FallOverAnim(-animAngle, "x"));

            TargetManager.Instance.RespawnTarget(this, Random.Range(minRespawnTime, maxRespawnTime));
        }
    }

    private IEnumerator FallOverAnim(float angle, string axis)
    {
        Quaternion startRot = pivotTransform.localRotation;
        Quaternion targetRot;

        if (axis == "z")
            targetRot = startRot * Quaternion.Euler(0f, 0f, angle / 2 + 10);
        else
            targetRot = startRot * Quaternion.Euler(angle, 0f, 0f);

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
