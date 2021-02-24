//Fait par Dérick

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePosBuilding;
    private Vector3 mousePos;
    private GameObject mouse;
    private SnapToGrid snap;

    private void Awake()
    {
        mouse = GameObject.FindGameObjectsWithTag("Mouse")[0];
        snap = new SnapToGrid();
    }
    void Start()
    {
        cam = Camera.main;

    }
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.nearClipPlane));
        mousePosBuilding = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength / 2));
        transform.position = mousePosBuilding;
        //On utilise le focal length, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. il a été divisé par deux pour que le facteur d'accélération ne soit pas trop grand.

        mouse.transform.position = mousePosBuilding;
        snap.SnapToTiles(transform);
    }
}