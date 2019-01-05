using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCell : MonoBehaviour {

    public int rowNum, colNum;
    public bool[] neighbour;

    public GroundCell()
    {
        rowNum = 0;
        colNum = 0;
    }

    //Here we set the row and column identity of each cell
    //Additionally we initialize the neighbour property
    public void setRowColumn(int rNum,int cNum)
    {
        rowNum = rNum;
        colNum = cNum;
        neighbour = new bool[4] { false, false, false, false };
    }
    //Here the cell gets to know in which directions does it have a neighbour
    public void nebInfo(bool[] N)
    {
         System.Array.Copy(N, neighbour,N.Length);
    }

    
}
