using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed;


    Vector2 prevPos = Vector2.zero;

    public void OnDrag()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1)
        {
            if (prevPos == Vector2.zero)
            {
                prevPos = Input.GetTouch(0).position;
                return;
            }
            Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
            Vector3 vec = new Vector3(dir.x, 0, dir.y);

            transform.position -= vec * cameraSpeed * Time.deltaTime;
            prevPos = Input.GetTouch(0).position;
        }
    }


    public void ExitDrag()
    {
        prevPos = Vector2.zero;
    }




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
