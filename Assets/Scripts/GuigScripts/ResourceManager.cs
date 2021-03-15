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
    public enum ToolType { Spear, Axe, Sword, Bow, Arrow, Backpack, Torch, NbToolType}

    public class BaseResourceInventory
    {
        const int INITIAL_RESOURCE_COUNT = 0;
        public string Name;
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
        public BaseResourceInventory(string n)
        {
            Name = n;
            GoldCount = INITIAL_RESOURCE_COUNT;
            WoodCount = INITIAL_RESOURCE_COUNT;
            StoneCount = INITIAL_RESOURCE_COUNT;
            EnergyRateCapacity = INITIAL_RESOURCE_COUNT;
            EnergyRateCost = INITIAL_RESOURCE_COUNT;
            ResourceCount = new int[(int)ResourceType.NbResourceType] { GoldCount, WoodCount, StoneCount, EnergyRateCapacity, EnergyRateCost };
        }
        // Guillaume: méthode à compléter
        //public override string ToString()
        //{
            
        //}
    }
    public class PlayerInventory
    {
        string name;
        public bool ownsBackpack = false;

    }
}
