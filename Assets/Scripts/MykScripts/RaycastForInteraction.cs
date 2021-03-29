//Fait par Mykaël
//Vidéo ayant permis de comprendre le fondtionnement d'un système d'intéraction : https://www.youtube.com/watch?v=saM9D1V6uNg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForInteraction : MonoBehaviour
{
    [SerializeField] private float range;
    // Update is called once per frame
    private Camera mainCamera;
    IInteractable currentTarget;

    private void Awake()
    {
        mainCamera = Camera.main; //pour utiliser la caméra principale
    }

    void Update()
    {
        FindInteractableObject(); //permet de trouver s'il y a un object avec lequel nous pouvons intéragir. 
        if (Input.GetKeyDown(KeyCode.V)) // Détermine la touche qu'il faudra peser afin d'intéragir avec un objet.
        {
            if (currentTarget != null) // Vérifie qu'il y a présentement un objet avec lequel nous pouvons intéragir.
            {
                currentTarget.OnInteract(); // Intéragit avec l'objet.
            }
        }
    }

    private void FindInteractableObject()
    {
        RaycastHit objectHit; // Garde l'information de l'objet que le raycast touche.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Le rayon qui se fait à partir de la caméra principale. 
        if (Physics.Raycast(ray, out objectHit, range)) //Si le rayon touche quelque chose.
        {
            IInteractable interactable = objectHit.collider.GetComponent<IInteractable>();

            if (interactable != null) 
            {
                if (objectHit.distance <= interactable.MaxRange) //Regarder si l'objet est plus loin que la distance maximale pour l'objet intéractible.
                {
                    if (interactable == currentTarget) //Si l'objet capté par le Raycast est le même objet que le dernier objet capté.
                    {
                        return;
                    }
                    else if (currentTarget != null) //Si le Raycast avait capté un objet dans le dernier objet et qu'ensuite il en capte un autre alors nous rendons cet objet capté comme étant l'objet capté présentement jusqu'à ce qu'un autre soit peut-être capt au prochain "Update".                    
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                    else //S'il n'y avait pas eu d'objet capté par le Raycast auparavent, nous rendons l'objet qui vient d'être capté comme étant l'objet capté présentement jusqu'à ce qu'un autre soit peut-être capt au prochain "Update".
                    {
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else //Si l'objet touché par le RayCast est plus loin que la distance maximale, alors nous ne pouvons pas intéragir avec un objet.
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }
            else //S'il n'y a pas d'objet qui est intéractible, alors nous ne pouvons pas intéragir avec un objet. 
            {
                if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else //Si le rayon ne touche aucun objet.
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }
}
