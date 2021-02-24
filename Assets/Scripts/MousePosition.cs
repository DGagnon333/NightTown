// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePosBuilding;
    private Vector3 mousePos;
    private Vector3 tf;
    private GameObject mouse;
    private List<GameObject> buildingList;

    private void Awake()
    {
        mouse = GameObject.FindGameObjectsWithTag("Mouse")[0];
        BuidlingTypes();
    }
    void Start()
    {
        cam = Camera.main;
        
    }
    private void BuidlingTypes()
    {
        //buildingList = GetComponent<BuildingManager>().buildingList;
    }
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.nearClipPlane));
        mousePosBuilding = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength/2));
        transform.position = mousePosBuilding;
        ////////////////////////////////////////////////ajouter une option pour désactiver le MousePos
        //le focal Lenghth n'a pas été trouvé par internet, juste avec beaucoup d'essaie de chacun 
        //des difféfenrents éléments de Camera pour voir ce qui est le mieux lors du déplacement de l'objet.
        //Selon moi, focal length est parfait, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. Je l'ai divisé par deux pour que le facteur d'accélération ne soit pas trop grand.

        if (!Input.GetKey(KeyCode.LeftShift))
        {

            mouse.transform.position = mousePos;
            GetComponent<SnapToGrid>().SnapToTiles(transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           // Instantiate(buildingList[0], transform, true);
        }
    }
}