using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCell : MonoBehaviour {

    //x corresponds to row, z to column. effect on x affects row
    public int rowNum, colNum;
    // neighbour 0 is west(left) z lower neighbour 1 is south (back) x lower
    public bool[] neighbour;
    public bool[] wall;//if wall then no passage
    public bool visited;

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
        wall = new bool[4] { true, true, true, true};
        visited = false;
    }

    //Here the cell gets to know in which directions does it have a neighbour
    public void setNebInfo(bool[] N)
    {
         System.Array.Copy(N, neighbour,N.Length);
    }
      
    //This function returns a new tile based on the direction of walking
    public IntToVector2 GetNeighbourTile(int [] walkDirecrion)
    {
        IntToVector2 coord = new IntToVector2();
        coord.x = rowNum;
        coord.z = colNum;
        if (walkDirecrion[0] == 1) coord.x--;
        if (walkDirecrion[1] == 1) coord.z--;
        if (walkDirecrion[2] == 1) coord.x++;
        if (walkDirecrion[3] == 1) coord.z++;
        return coord;
    }    
    
}
