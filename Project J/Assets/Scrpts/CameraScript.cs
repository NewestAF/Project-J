using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -11.7)
            {
                transform.Translate(Vector2.left * cameraSpeed * Time.deltaTime);
            }                
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 13.7)
            {
                transform.Translate(Vector2.right * cameraSpeed * Time.deltaTime);
            }                
        }
    }
}
