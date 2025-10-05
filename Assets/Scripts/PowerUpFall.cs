using UnityEngine;

public class PowerUpFall : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < 0f)
        {
            Destroy(gameObject);
        }
    }
}