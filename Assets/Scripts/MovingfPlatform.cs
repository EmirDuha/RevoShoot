using UnityEngine;

public class MovingfPlatform : MonoBehaviour
{
    private float moveTime = 4f;
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
            transform.Translate(Vector3.left * Time.deltaTime * 1.5f);
        }
        else if (Time.time <= moveTime && !moveLeft)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
        }
        else
        {
            moveLeft = !moveLeft;
            moveTime = Time.time + 4f;
        }

    }
}
