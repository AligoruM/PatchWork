using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fields
{       
    private static int[,] lightFieldMatrix;
    private static int[,] darkFieldMatrix;

    public static GameObject lightField;
    public static GameObject darkField;

    public static int test = 5;

    public Fields()
    {
        lightField = GameObject.Find("LightField");
        darkField = GameObject.Find("DarkField");
        lightFieldMatrix = new int[9,9];
        darkFieldMatrix = new int[9, 9];
    }
}
