using UnityEngine;

public class PowerUpFall : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;

    void Update()
    {
        // Aşağı hareket (y ekseni negatif yönde)
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Belirli bir yüksekliğin altına düşünce yok et
        if (transform.position.y < 0f)
        {
            Destroy(gameObject);
        }
    }
}