using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerView : MonoBehaviour
{
    public float sensitivity = 3000f;
    public Transform Player;

    float rotationInY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationInY -= y;
        rotationInY = Mathf.Clamp(rotationInY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationInY, 0f, 0f);
        Player.Rotate(Vector3.up * x);
    }
}
