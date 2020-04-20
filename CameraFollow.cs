using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float smoothSpeed;
    [SerializeField]
    private Vector3 offset;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 mousePosition;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < playerTransform.position.x)
        {
            offset.x = -Mathf.Abs(offset.x);
        }
        else if (mousePosition.x > playerTransform.position.x)
        {
            offset.x = Mathf.Abs(offset.x);
        }
        if (mousePosition.y < playerTransform.position.y)
        {
            offset.y = -Mathf.Abs(offset.y);
        }
        else if (mousePosition.y > playerTransform.position.y)
        {
            offset.y = Mathf.Abs(offset.y);
        }

        Vector3 desiredPos = playerTransform.position + offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(gameObject.transform.position, desiredPos, ref cameraVelocity, smoothSpeed * Time.deltaTime);
        gameObject.transform.position = smoothedPos;
    }
}
