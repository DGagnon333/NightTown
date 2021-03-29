using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auteur: Guillaume Varin

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
    // Guillaume: pour tous les set, je devrai m'assurer que l'item n'existe pas déjà. le cas échéant, je devrai lancer un
    //            exception et le plus rapidement possible gérer cette exception ou empêcher la création de cette item
    //public int[,] WeaponCosts = new int[(int)WeaponType.NbWeaponType + 1, (int)ResourceType.NbResourceType - 2] 
    //{ { 1, 1, 0 }, { 4, 2, 0 }, { 2, 4, 0 }, { 4, 4, 0 }, { 0, 4, 2 } };
    
    //public class Resource
    //{
    //    private int resourceID;
    //    public int ResourceID
    //    {
    //        get
    //        {
    //            return resourceID;
    //        }
    //        private set
    //        {
    //            resourceID = value;
    //        }
    //    }
    //    // Guillaume : la description de la ressource permettra de décrire les différentes utilités de la ressource.
    //    private string resourceDescription;
    //    public string ResourceDescription
    //    {
    //        get
    //        {
    //            return resourceDescription;
    //        }
    //        private set
    //        {
    //            resourceDescription = value;
    //        }
    //    }
    //    private Sprite resourceIcon;
    //    public Sprite ResourceIcon
    //    {
    //        get
    //        {
    //            return resourceIcon;
    //        }
    //        private set
    //        {
    //            resourceIcon = value;
    //        }
    //    }
    //    public Resource()
    //    {

    //    }
    //}
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
            // Guillaume: le tableau à l'air inutile...
            //ResourceCount = new int[(int)ResourceType.NbResourceType] { GoldCount, WoodCount, StoneCount, EnergyRateCapacity, EnergyRateCost };
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
        static void ManageTowerCost(int level)
        {
            
        }
    }
}
