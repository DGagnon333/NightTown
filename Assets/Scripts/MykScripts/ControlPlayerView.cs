using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerView : MonoBehaviour
{
    //Script fait par Mykaël 
    // Un vidép qui m'a aidé à comprendre est celui-ci https://www.youtube.com/watch?v=_QajrabyTJc&t=934s
    public float sensitivity = 3000f;
    public Transform Player;

    float rotationInY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Permet de placer le curseur au centre de la vue
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;


        rotationInY -= y; //Sinon c'est inversé
        rotationInY = Mathf.Clamp(rotationInY, -50, 50);
        transform.localRotation = Quaternion.Euler(rotationInY, x, 0f);
        Player.Rotate(Vector3.up * x);

    }
}
