//Classe qui permet de modifier la position d'un objet pour qu'il se place à des endroits précis
//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnapToGrid : MonoBehaviour
{
    public void SnapToTiles(Transform tf)
    {
        Vector3 step = new Vector3(2, 2, 2);//la grandeur entre chaque position posible de l'objet
        Vector3 stepDiff;//la différence entre la taille de l'objet et le "step" (servira à repositioner l'objet correctement selon sa taille)
        stepDiff = new Vector3(tf.localScale.x, 2, tf.localScale.z) - step;

        //la fonction SnapToGrid est inspirée de How to Snap Objects to a 
        //Custom Grid in 3 minutes - Unity Tutorial, par Saeed Prez, Youtube
        //
        //j'ai ajouter un stepDiff et d'autres intrants, comme ça, le jeu prend en compte si on la taille d'un 
        //objet change et prend en compte cette donnée pour la nouvelle position...sinon
        //l'objet se retrouve au milieu de deux cases
        //-------------------------------------------------------début inspiration

        var position = new Vector3(
            Mathf.Round(tf.position.x / step.x) * step.x,
            0,
            Mathf.Round(tf.position.z / step.z) * step.z);
            tf.position = new Vector3(position.x, 0, position.z);


        //--------------------------------------------------------fin d'inspiration
        

        if (stepDiff != Vector3.zero)
        {
            tf.position += new Vector3(1,0,1);
        }
        //ici le step sert à savoir si la taille de l'objet est la même taille que les cases de la matrice.
        //Si elle n'est pas de la même taille et qu'elle est plus grosse, l'objet se retrouve entre deux tuiles.
        //en vérifiant donc avec le step diff la taille de l'objet, si elle est différente des case on augmente 
        //sa position pour qu'elle reste dans les tuiles.
    }
}
