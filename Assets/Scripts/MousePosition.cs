//Fait par Dérick

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam;
    private SnapToGrid snap;
    [SerializeField] private GameObject buildingLayout;
    

    private void Awake()
    {
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
        buildingLayout.transform.position = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength / 2));
        //On utilise le focal length, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. il a été divisé par deux pour que le facteur d'accélération ne soit pas trop grand.
        snap.SnapToTiles(buildingLayout.transform);
    }
}