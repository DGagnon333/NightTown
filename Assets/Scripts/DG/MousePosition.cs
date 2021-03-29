//Classe permettant de contrôler la position d'un objet par la souris
//Fait par Dérick

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam; //la caméra principale
    private SnapToGrid snap; //on instancie et attache le script SnapToGrid à snap pour accéder à ses propriétés
    [SerializeField] private GameObject buildingLayout; // dans ce cas, c'est le "phantôme" du bâtiment sélectionner qui sera déplacé
    float mouseX; //la position de la souris en x
    float mouseY; //la position de la souris en y
    void Start()
    {
        snap = GetComponent<SnapToGrid>();
        cam = Camera.main;
    }
    void Update()
    {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        buildingLayout.transform.position = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength * (float)0.75));
        //On utilise le focal length, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. il a été divisé par deux pour que le facteur d'accélération ne soit pas trop grand.

        snap.SnapToTiles(buildingLayout.transform);
    }
}