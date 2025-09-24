using UnityEngine;

public class MovingfPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();

    }

    private void HorizontalMovement()
    {
        transform.Translate(Vector3.left * Time.deltaTime);


    }
}
