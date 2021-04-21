using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
