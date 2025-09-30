using UnityEngine;

public class MovingfPlatform : MonoBehaviour
{
    [SerializeField] private float moveTime = 4f;
    [SerializeField] private float addTime = 4f;
    [SerializeField] private float moveSpeed = 1.5f;
     private bool moveLeft = true;

    void Start()
    {

    }

    void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        if (Time.time <= moveTime && moveLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        else if (Time.time <= moveTime && !moveLeft)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        else
        {
            moveLeft = !moveLeft;
            moveTime = Time.time + addTime;
        }

    }
}
