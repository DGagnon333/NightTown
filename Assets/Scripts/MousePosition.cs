// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.

using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    private Camera cam;
    private Vector3 MousePos;
    private Vector3 tf;
    void Start()
    {
        cam = Camera.main;
    }
    private void Awake()
    {
    }

    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        MousePos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.focalLength / 2));
        tf = transform.position;
        tf.x = MousePos.x;
        tf.z = MousePos.z;
        transform.position = tf;
        GetComponent<SnapToGrid>().SnapToTiles(transform);

        //sp.SnapToTiles(transform.position);
        //le focal Lenghth n'a pas été trouvé par internet, juste avec beaucoup d'essaie de chacun 
        //des difféfenrents éléments de Camera pour voir ce qui est le mieux lors du déplacement de l'objet.
        //Selon moi, focal length est parfait, car plus le curseur sera éloigné du champ de vision, plus il
        //accélérera. Je l'ai divisé par deux pour que le facteur d'accélération ne soit pas trop grand.
    }
}