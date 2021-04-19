//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Obstacles : MonoBehaviour
//{
//    private bool[,] tileStateCopy;
//    // Update is called once per frame
//    void Awake()
//    {
//        StateCopyCreation();
//    }
//    private void StateCopyCreation()
//    {
//        int tileStateLength = GetComponent<GridManager>().tileState.Length;
//        tileStateCopy = new bool[tileStateLength, tileStateLength];
//        for (int z = 0; z <= tileStateLength; z++)
//        {
//            for (int x = 0; x <= tileStateLength; x++)
//            {
//                tileStateCopy[x,z] = GetComponent<GridManager>().tileState[x,z];
//            }
//        }
//        tileStateCopy = GetComponent<GridManager>().tileState;
//    }
//}
