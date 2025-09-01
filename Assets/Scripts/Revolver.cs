using UnityEngine;

public class Revolver : MonoBehaviour
{
    [Header("Rotation Data")]
    [SerializeField] private Transform revolverTransform;
    [SerializeField] private float revolverRotationSpeed = 10f;

    [Header("Aim Data")]
    [SerializeField] private Transform aimTransform;          // nişan küresi
    [SerializeField] private LayerMask aimColliderLayerMask;  // hedeflerin layer’ı

    [Header("Shoot Data")]
    [SerializeField] private float shootDistance = 100f;      // mermi menzili

    private RaycastHit currentHit; // o an bakılan nokta (cache)

    [Header("Sound Data")]
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource breakAudioSource;

    private void Start()
    {

    }
    private void Update()
    {
        AimUpdate();
        CheckInputs();
        RevolverRotation();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

private void Shoot()
{
    shootAudioSource.Play();

    if (currentHit.collider != null && currentHit.collider.CompareTag("TargetHead"))
    {
        Transform parent = currentHit.collider.transform.parent;

        breakAudioSource.Play();
        Destroy(parent.gameObject);
    }
    else
    {
        //Misfire
    }
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
            // boşluğa bakıyorsa küreyi ileri sabit noktaya koy
            aimTransform.position = ray.GetPoint(shootDistance);
            currentHit = new RaycastHit(); // resetle
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
}
