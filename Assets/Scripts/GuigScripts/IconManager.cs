using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Fait par Guillaume
// Ce code nous permet d'accéder à différents icônes utilisés dans le jeu en tant que GameObject 
// ce qui facilite leur utilisation dans un interface utilisateur qui se mets régulièrement à jour.
public class IconManager : MonoBehaviour
{
    private static IconManager _iconManagerInstance;
    public static IconManager iconManagerInstance
    {
        get
        {
            if (_iconManagerInstance == null)
                _iconManagerInstance = Instantiate(Resources.Load<IconManager>("IconManager"));
            return _iconManagerInstance;
        }
    }
    [SerializeField]
    public Sprite backpackIcon;
    [SerializeField]
    public Sprite handTorchIcon;
    [SerializeField]
    public Sprite questObjectIcon;
    [SerializeField]
    public Sprite spearIcon;
    [SerializeField]
    public Sprite axeIcon;
    [SerializeField]
    public Sprite swordIcon;
    [SerializeField]
    public Sprite arrowIcon;
    [SerializeField]
    public Sprite bowIcon;
}
