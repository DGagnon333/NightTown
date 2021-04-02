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
    private int key; //Cette clée permet de retenir la touche appuyée sur le clavier
    [SerializeField] private  GameObject buildingLayout; //le "phantôme" du bâtiment sélectionné, qui retient sa positino et sa grosseur
    private GameObject[] tiles; //utilisé pour pouvoir afficher ou non les tuiles
    private GameObject selectedBuilding; //le bâtiment choisit
    public bool buildingModeState = false; //l'état du mode de construction, activé ou désactivé
    [SerializeField] private Material gridTexture;
    [SerializeField] private Material greenTexture;
    [SerializeField] private Renderer gridRenderer;
    public Dictionary<Point2D, GameObject> buildingTiles;
    public List<Vector3> wireQueue = new List<Vector3>();
    public List<GameObject> wireList = new List<GameObject>();
    Color baseColor;


    private void Awake()
    {
        selectedBuilding = buildingList[1]; //on donne un object par défaut à selectedBuilding pour que ça nous ne
        //retourne pas un null reference si aucun bâtiment n'est sélectionné.
        baseColor = buildingLayout.GetComponent<Renderer>().material.color;
        buildingTiles = new Dictionary<Point2D, GameObject>();
    }
    private void Start()
    {
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

            if (Input.anyKeyDown)
            {
                SelectedBuilding();
            }

            //cette ligne a été inspirée par Color.Lerp, Unity Documentation, [URL]: https://docs.unity3d.com/ScriptReference/Color.Lerp.html (consulté le 22 mars 2021)
            buildingListUI[key].color = Color.Lerp(Color.gray, Color.black, Mathf.PingPong(Time.time, (float)0.5));

            if (Input.GetKeyUp($"{key + 1}") && key!= 4)
                buildingListUI[key].color += new Color(50, 50, 50);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                    GetComponent<GridManager>().TileState(selectedBuilding, buildingLayout.transform, selectedBuilding.transform.localScale, buildingTiles, wireQueue, wireList);
            }
            if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftShift) && key==4)
            {
                GetComponent<GridManager>().TileState(selectedBuilding, buildingLayout.transform, selectedBuilding.transform.localScale, buildingTiles, wireQueue, wireList);
            }
            //on peut déselectioner le placement des fils au besoin avec un click droit de la souris
            //if (Input.GetKeyDown(KeyCode.Mouse1) || !Input.GetKeyDown(KeyCode.Mouse0))
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                wireQueue.Clear();
                wireList.Clear();
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
            key = 0;
            selectedBuilding = buildingList[key];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            key = 1;
            selectedBuilding = buildingList[key];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            key = 2;
            selectedBuilding = buildingList[key];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            key = 3;
            selectedBuilding = buildingList[key];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            key = 4;
            selectedBuilding = buildingList[key];
        }
        for (int i = 0; i < buildingList.Count; i++)
        {
            if (i != key)
                buildingListUI[i].color = Color.white;
        }
        buildingLayout.transform.localScale  = selectedBuilding.transform.localScale;
        if (selectedBuilding.CompareTag("Eraser"))
            buildingLayout.GetComponent<Renderer>().material.color = Color.red;
        else
            buildingLayout.GetComponent<Renderer>().material.color = baseColor;
        return selectedBuilding;
    }

    /// <summary>
    /// permet de cacher ou non la grille, le "phantôme" du bâtiment sélectioné, et le UI pour le mode de construction
    /// </summary>
    /// <param name="state">true - false</param>
    public void BuildingMode(bool state)
    {
        buildingModeState = state;
        buildingLayout.SetActive(state); 
        Canevas.SetActive(state);
        if (state)
        {
            gridRenderer.material = gridTexture;
        }
        if (!state)
            gridRenderer.material = greenTexture;

    }
}
