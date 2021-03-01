//Cette classe s'occupe de toutes les actions possibles pour le mode de construction du jeu
//avec les touches de claviers
//
//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> buildingList; //liste des bâtiments
    [SerializeField] public List<Image> buildingListUI; //liste des images pour chaque bâtiment dans le UI
    [SerializeField] private GameObject Canevas; //le Canevas du UI qui sera afficher à l'écran
    private int key = 0; //Cette clée permet de retenir la touche appuyée sur le clavier
    [SerializeField] private  GameObject buildingLayout; //le "phantôme" du bâtiment sélectionné, qui retient sa positino et sa grosseur
    private GameObject[] tiles; //utilisé pour pouvoir afficher ou non les tuiles
    private GameObject selectedBuilding; //le bâtiment choisit
    public bool buildingModeState = false; //l'état du mode de construction, activé ou désactivé
    private void Awake()
    {
        selectedBuilding = buildingList[0]; //on donne un object par défaut à selectedBuilding pour que ça nous ne
        //retourne pas un null reference si aucun bâtiment n'est sélectionné.
    }
    private void Start()
    {
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

        //Si le mode de construction est désactvié, on ne veut pas que le jouer puisse pouvoir placé des bâtiments quand même
        if (buildingModeState)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectedBuilding();
            }
            if (Input.GetKeyUp($"{key + 1}"))
                buildingListUI[key].color += new Color(50, 50, 50);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<GridManager>().TileState(selectedBuilding, buildingLayout.transform, selectedBuilding.transform.localScale);
            }
        }

    }

    /// <summary>
    /// permet de trouver quel bâtiment est sélectionner avec les numéros sur le clavier et de changer la couleur du UI
    /// pour le bâtiment choisit.
    /// </summary>
    /// <returns></returns>
    private GameObject SelectedBuilding()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBuilding = buildingList[0];
            key = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBuilding = buildingList[1];
            key = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBuilding = buildingList[2];
            key = 2;
        }
        buildingLayout.transform.localScale = selectedBuilding.transform.localScale;
        if (selectedBuilding == buildingList[key])
        {
            buildingListUI[key].color += new Color(-50, -50, -50);
        }
        return selectedBuilding;
    }

    /// <summary>
    /// permet de cacher ou non la grille, le "phantôme" du bâtiment sélectioné, et le UI pour le mode de construction
    /// </summary>
    /// <param name="state">true - false</param>
    public void BuildingMode(bool state)
    {
        buildingModeState = state;
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().enabled = state; 
        }
        buildingLayout.SetActive(state); 
        Canevas.SetActive(state);
    }
}
