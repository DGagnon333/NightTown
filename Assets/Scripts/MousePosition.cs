//Fait par Dérick

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePosBuilding;
    private SnapToGrid snap;
    private GameObject buildingLayout;

    private void Awake()
    {
        //mouse = GameObject.FindGameObjectsWithTag("Mouse")[0];
        snap = new SnapToGrid();
    }
    void Start()
    {
        cam = Camera.main;
        buildingLayout = GameObject.FindGameObjectWithTag("BuildingLayout");
    }
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        mousePosBuilding = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength / 2));
        buildingLayout.transform.position = mousePosBuilding;
        //On utilise le focal length, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. il a été divisé par deux pour que le facteur d'accélération ne soit pas trop grand.

        buildingLayout.transform.position = mousePosBuilding;
        snap.SnapToTiles(buildingLayout.transform);
        //if (!GetComponent<BuildingManager>().buildingModeState)
        //{

        //}
    }
}