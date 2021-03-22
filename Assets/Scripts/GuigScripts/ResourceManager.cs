using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auteur: Guillaume Varin
// heures dédiés: 3h + ... à remplir ...
// date début: lundi 15 mars 2021
// date fin: ... à remplir ...

class ResourceCountInvalidException : Exception { }
/// <summary>
/// Le ResourceManager a comme objectif de gérer les différentes ressources dans le jeu. 
/// Cela comprend: - la quantité des ressources de chaque type dans la base (l'or, le bois, la pierre et l'énergie électrique)
///                - la quantité des outils de chaque type dans l'inventaire du joueur ( les armes, les munitions et les accessoires)
///                - la valeur d'achat de nouveaux outils selon l'offre et la demande
/// </summary>
/// <returns></returns>
public class ResourceManager : MonoBehaviour
{
    
    public enum ResourceType { Gold, Wood, Stone, EnergyCapacity, EnergyCost, NbResourceType }
    public enum ItemType { Backpack, Torch, Quest, NbToolType }
    public enum WeaponType { Spear, Axe, Sword, ArrowBow, NbWeaponType}
    // Guillaume: pour tous les set, je devrai m'assurer que l'item n'existe pas déjà. le cas échéant, je devrai lancer un
    //            exception et le plus rapidement possible gérer cette exception ou empêcher la création de cette item
    public class PlayerItem : MonoBehaviour
    {
        private int iD;
        public int ID
        {
            get
            {
                return iD;
            }
            private set
            {
                iD = value;
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        private Sprite icon;
        public Sprite Icon
        {
            get
            {
                return icon;
            }
            private set
            {
                icon = value;
            }
        }
    }
    public class Resource
    {
        private int resourceID;
        public int ResourceID
        {
            get
            {
                return resourceID;
            }
            private set
            {
                resourceID = value;
            }
        }
        // Guillaume : la description de la ressource permettra de décrire les différentes utilités de la ressource.
        private string resourceDescription;
        public string ResourceDescription
        {
            get
            {
                return resourceDescription;
            }
            private set
            {
                resourceDescription = value;
            }
        }
        private Sprite resourceIcon;
        public Sprite ResourceIcon
        {
            get
            {
                return resourceIcon;
            }
            private set
            {
                resourceIcon = value;
            }
        }
        public Resource()
        {

        }
    }
    public class BaseResourceInventory
    {
        const int INITIAL_RESOURCE_COUNT = 0;
        int goldCount;
        public int GoldCount
        {
            get
            {
                return goldCount;
            }
            private set
            {
                if (value < 0) throw new ResourceCountInvalidException();
                goldCount = value;
            }
        }
        int woodCount;
        public int WoodCount
        {
            get
            {
                return woodCount;
            }
            private set
            {
                if (value < 0) throw new ResourceCountInvalidException();
                woodCount = value;
            }
        }
        int stoneCount;
        public int StoneCount
        {
            get
            {
                return stoneCount;
            }
            private set
            {
                if (value < 0) throw new ResourceCountInvalidException();
                stoneCount = value;
            }
        }
        int energyRateCapacity; // Guillaume: se décrit comme un taux d'énergie électrique (Joules/seconde)
        public int EnergyRateCapacity
        {
            get
            {
                return energyRateCapacity;
            }
            private set
            {
                if (value < 0) throw new ResourceCountInvalidException();
                energyRateCapacity = value;
            }
        }
        int energyRateCost; // Guillaume: le cout actuel en énergie électrique
        public int EnergyRateCost
        {
            get
            {
                return energyRateCost;
            }
            private set
            {
                if (value < 0) throw new ResourceCountInvalidException();
                energyRateCost = value;
            }
        }
        public int[] ResourceCount;
        public BaseResourceInventory()
        {
            GoldCount = INITIAL_RESOURCE_COUNT;
            WoodCount = INITIAL_RESOURCE_COUNT;
            StoneCount = INITIAL_RESOURCE_COUNT;
            EnergyRateCapacity = INITIAL_RESOURCE_COUNT;
            EnergyRateCost = INITIAL_RESOURCE_COUNT;
            ResourceCount = new int[(int)ResourceType.NbResourceType] { GoldCount, WoodCount, StoneCount, EnergyRateCapacity, EnergyRateCost };
        }
        // Guillaume: devra éventuellement être afficher de façon interactive sur le GameScreen (avec icône et information claire)
        public override string ToString()
        {
            string message;
            message = "Inventaire de la base: " + "Gold: " + GoldCount.ToString() +
                                                  "Wood: " + WoodCount.ToString() +
                                                  "Stone: " + StoneCount.ToString() +
                                                  "Energy Capacity: " + EnergyRateCapacity.ToString() +
                                                  "Energy Cost: " + EnergyRateCost.ToString();
            return message;
        }
    }
    public class PlayerInventory
    {
        const int PLAYER_INVENTORY_SIZE = 8;
        const int BACKPACK_SIZE = 16;

        string name;
        bool ownsBackpack = false;
        List<PlayerItem> inventory;


    }
}
