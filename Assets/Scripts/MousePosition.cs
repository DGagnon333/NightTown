using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Sprite mouse;
    private Camera camMain;

    private void Awake()
    {
        //Cursor.visible;
    }
    void Update()
    {
        //Cursor.
        //transform.position = camMain.ScreenToWorldPoint(Cursor.);
    }
}
