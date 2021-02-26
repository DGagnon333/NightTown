//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> buildingList;
    private GameObject buildingLayout;
    private GameObject[] tiles;
    private GameObject selectedBuilding;
    private void Start()
    {
        buildingLayout = GameObject.FindGameObjectsWithTag("BuildingLayout")[0];
        
        tiles = GameObject.FindGameObjectsWithTag("Grid");
    }
    private void Update()
    {
        BuildingInputManager();
        //SelectedBuilding();
    }
    /// <summary>
    /// Les inputs permettant de désactiver ou d'activer le mode de construction, ainsi que de choisir la tour voulue et de la placé
    /// </summary>
    private void BuildingInputManager()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BuildingMode(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildingMode(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBuilding = buildingList[0];
            Debug.Log(selectedBuilding.name);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBuilding = buildingList[1];
            Debug.Log(selectedBuilding.name);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBuilding = buildingList[2];
            Debug.Log(selectedBuilding.name);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<GridManager>().TileState(selectedBuilding, buildingLayout.transform);
        }
    }

    private GameObject SelectedBuilding()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    //return buildingList[0];
        //    selectedBuilding = buildingList[0];
        //    Debug.Log(selectedBuilding.name);

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //return buildingList[1];
        //    selectedBuilding = buildingList[1];
        //    Debug.Log(selectedBuilding.name);

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    //return buildingList[2];

        //    selectedBuilding = buildingList[2];
        //    Debug.Log(selectedBuilding.name);

        //}
        //return selectedBuilding;
        return null;
    }
    /// <summary>
    /// permet de cacher ou non la grille et le "buildingLayout"
    /// </summary>
    /// <param name="state">true - false</param>
    public void BuildingMode(bool state)
    {
        //car lors de la première recherche les nouvelles tiles n'étaient pas encore crées
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().enabled = state; //on active ou désactive le Renderer puisqu'on a besoin de garder l'état de l'objet
        }
        buildingLayout.SetActive(state); // on active ou désactive le l'objet même puisqu'on a pas besoin de garder l'état de cet objet actif
        //et cela sauvera de la mémoire
    }
}
