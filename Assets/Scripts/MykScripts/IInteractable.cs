//Fait par Mykaël
//Vidéo ayant permis de comprendre le fondtionnement d'un système d'intéraction : https://www.youtube.com/watch?v=saM9D1V6uNg


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    float MaxRange { get; }
    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}
