//Cette classe s'occupe de toutes les actions possibles pour le mode de construction du jeu
//avec les touches de claviers
//
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
    public bool buildingModeState = false;
    private void Awake()
    {
        selectedBuilding = buildingList[0]; //on donne un object par défaut à selectedBuilding pour que ça nous ne
        //retourne pas un null reference si aucun bâtiment n'est sélectionné.
    }
    private void Start()
    {
        buildingLayout = GameObject.FindGameObjectsWithTag("BuildingLayout")[0];//ici je préfère trouver un objet par son tag
        //directement avec le script plutôt que d'avoir à faire un SerializeField et de placer des objets au Script dans Unity à chaque fois.
        tiles = GameObject.FindGameObjectsWithTag("Grid"); //On trouve tous les objets avec le tag Grid. On crée cette fonction
        //dans le start et non Awake pour que tous les autres cases de la grille est le temps d'être instanciées.
        BuildingMode(buildingModeState);
    }
    private void Update()
    {
        BuildingInputManager();
    }

    /// <summary>
    /// Les touche de clavier permettant de désactiver ou d'activer le mode de construction, ainsi que de choisir la tour voulue et de la placé
    /// </summary>
    private void BuildingInputManager()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            BuildingMode(false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            BuildingMode(true);
        }
        if (buildingModeState)
        {
            //Ici, on met même la sélection de bâtiment désactivée si le mode de construction est désactivé pour laisser place à d'autres touches
            //pour les attaques ou autre
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectedBuilding();
            }
            //Si le mode de construction est désactvié, on ne veut pas que le jouer puisse pouvoir placé des bâtiments quand même
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<GridManager>().TileState(selectedBuilding, buildingLayout.transform, selectedBuilding.transform.localScale);
            }
        }
            
    }

    /// <summary>
    /// permet de trouver quel bâtiment est sélectionner, avec numéros sur le clavier
    /// </summary>
    /// <returns></returns>
    private GameObject SelectedBuilding()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBuilding = buildingList[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBuilding = buildingList[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBuilding = buildingList[2];
        }
        buildingLayout.transform.localScale = selectedBuilding.transform.localScale;
        return selectedBuilding;
    }

    /// <summary>
    /// permet de cacher ou non la grille et le "buildingLayout"
    /// </summary>
    /// <param name="state">true - false</param>
    public void BuildingMode(bool state)
    {
        buildingModeState = state;
        //car lors de la première recherche les nouvelles tiles n'étaient pas encore crées
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().enabled = state; //on active ou désactive le Renderer puisqu'on a besoin de garder l'état de l'objet
        }
        buildingLayout.SetActive(state); // on active ou désactive le l'objet même puisqu'on a pas besoin de garder l'état de cet objet actif
        //et cela sauvera de la mémoire
    }
}
