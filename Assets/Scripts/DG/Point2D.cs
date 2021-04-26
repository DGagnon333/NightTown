﻿//Classe qui retient une position X et X 

//fait par Dérick Gagnon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2D
{
    public int X { get; private set; }
    public int Z { get; private set; }
    public Point2D(int x, int z)
    {
        X = x;
        Z = z;
    }
}
