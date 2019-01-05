using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public GroundCell groundTilePrefab;
    public WallTile wallTilePrefab;
    public WallTile wallEastTilePrefab;

    private IntToVector2 sizeOfMaze;
    private IntToVector2 tileSize;
    GroundCell[,] cell;


    //East wall is green. This creates a simple wall on the east west side ( left/right or x axis)
    private void createEastWall(IntToVector2 coordinates)
    {
        WallTile wallEast = Instantiate(wallEastTilePrefab) as WallTile;
        wallEast.name = "WallEast" + coordinates.x + ", " + coordinates.z;
        wallEast.transform.parent = transform;
        wallEast.transform.position = new Vector3(tileSize.x * coordinates.x + 0.5f * tileSize.x, .5f, tileSize.z * coordinates.z);
        wallEast.transform.localScale = new Vector3(tileSize.z, wallEast.transform.localScale.y, wallEast.transform.localScale.z);
    }
    //North wall is blue.This creates a simple wall on the North south side ( foreward/backward or z axis)
    private void createNorthWall(IntToVector2 coordinates)
    {
        WallTile wallNorth = Instantiate(wallTilePrefab) as WallTile;
        wallNorth.name = "WallNorth" + coordinates.x + ", " + coordinates.z;
        wallNorth.transform.parent = transform;
        wallNorth.transform.position = new Vector3(tileSize.x * coordinates.x, .5f, tileSize.z * coordinates.z + 0.5f * tileSize.z);
        wallNorth.transform.localScale = new Vector3(tileSize.x, wallNorth.transform.localScale.y, wallNorth.transform.localScale.z);
    }

    //Start function
    public void GenerateMaze(IntToVector2 sOfMaze, IntToVector2 tSize)
    {
        sizeOfMaze = sOfMaze;
        tileSize = tSize;
        cell = new GroundCell[sizeOfMaze.x , sizeOfMaze.z];
        
        for (int j = 0;j < sizeOfMaze.z; j++)
        {
            for(int i = 0; i < sizeOfMaze.x; i++)
            {
                //Create each tiles and give them  info about their neighbours 
                CreateCell(new IntToVector2(i,j));
            }
        }
        cell[2, 0].visited = true;
        cell[2, 2].visited = true;
        cell[2, 1].visited = true;

        int xRand,zRand;
        do
        {
            //Starting with a random cell
            xRand = Random.Range(0, sizeOfMaze.x);
            zRand = Random.Range(0, sizeOfMaze.z);
        }
        while (cell[xRand, zRand].visited != false);
        
        //Making Passages wherever needed
        CreatePassages(xRand,zRand);
       
        for (int j = 0; j < sizeOfMaze.z; j++)
        {
            for (int i = 0; i < sizeOfMaze.x; i++)
            {
                    if (cell[i, j].wall[0]) createEastWall(new IntToVector2(i - 1, j));
                    if (cell[i, j].wall[1]) createNorthWall(new IntToVector2(i, j - 1));
                    if (cell[i, j].wall[2]) createEastWall(new IntToVector2(i, j));
                    if (cell[i, j].wall[3]) createNorthWall(new IntToVector2(i, j));
            }
        }
        
        
    }
    // Create cells for each grid positions. Give information to each cell about their neighbor
    private void CreateCell(IntToVector2 coordinates)
    {
         cell[coordinates.x,coordinates.z]= Instantiate(groundTilePrefab) as GroundCell;

        cell[coordinates.x,coordinates.z].name = "GroundTile " + coordinates.x + ", " + coordinates.z;
        cell[coordinates.x, coordinates.z].transform.parent = transform;

        //Tell cell which row and column it belongs to
        cell[coordinates.x,coordinates.z].setRowColumn(coordinates.x, coordinates.z);

        // neighbour 0 is west(left) z lower neighbour 1 is south (back) x lower
        //Here, we are trying to give info to a tile about its neighbour
        bool[] nebDet = {(coordinates.x == 0? false: true),(coordinates.z == 0 ? false: true),
             (coordinates.x < sizeOfMaze.x-1 ? true : false), (coordinates.z < sizeOfMaze.z-1 ? true : false) //false, false};
             };
        cell[coordinates.x,coordinates.z].setNebInfo(nebDet);

        cell[coordinates.x,coordinates.z].transform.position = new Vector3(tileSize.x * coordinates.x, 0, tileSize.z*coordinates.z);
        cell[coordinates.x,coordinates.z].transform.localScale = new Vector3(tileSize.x, tileSize.z ,
                                                                             cell[coordinates.x,coordinates.z].transform.localScale.y);
    }


    public Stack<IntToVector2> tileArrayStack = new Stack<IntToVector2>();
    
    int counter = 100;
    
    //This creates passages. Here Actual logic is implemented. Start from a random cell. Make passage to one of unvisited neighbour. 
    //Mark visited. Store in stack. And backtrack for dead ends. Stop when you reach the starting position.
    private void CreatePassages(int xRand, int zRand)
    {
        counter--;
        // neighbour 0 is west(left) z lower neighbour 1 is south (back) x lower
        // so 0 is left 1 is back 2 is right and 3 is foreward   

        //mark that position visited and push the location to stack
        IntToVector2 startingTile = new IntToVector2(xRand, zRand);
        cell[xRand, zRand].visited = true;
        
        //Get all the directions of a tile where we have unvisited neighbour
        int[] neighboursToPickFrom = GetUnvisitedNeighbours(xRand,zRand);
        
        //Pick one direction from the many directions to walk in. i.e walk in one of the directions
        int sum = 4;
        while(sum != 1 && counter >1)
        {
            sum = neighboursToPickFrom[0] + neighboursToPickFrom[1] + neighboursToPickFrom[2] + neighboursToPickFrom[3];

            // if sum is 0 no neighbour is left to visit. Then backtrack to a cell which has a neighbour to visit
            if (sum == 0)
            {
                if (tileArrayStack.Count > 0)
                {
                    
                    IntToVector2 backTrackTile = tileArrayStack.Pop();
                    if (counter > 1) CreatePassages(backTrackTile.x, backTrackTile.z);
                }
                else
                {
                    //stop doing everything
                    counter = 0;
                }
                sum = 1;
            }
            //Make sum 1 by eleminating one direction at random
            if (sum > 1)
            {
                int randomDirToEliminate = Random.Range(0, 4);
                while (neighboursToPickFrom[randomDirToEliminate] != 1)
                {
                    randomDirToEliminate = Random.Range(0, 4);
                }
                 neighboursToPickFrom[randomDirToEliminate]--;
            }
        }

        //Now walk to a new tile, where you will repeat everything
        IntToVector2 newTileToWorkOn = cell[xRand, zRand].GetNeighbourTile(neighboursToPickFrom);
        //push the old tile to stack 
        tileArrayStack.Push(startingTile);
        //Create the passage to the choosen neighbour one. 
        for (int i = 0;i <4; i++)
        {
            if(neighboursToPickFrom[i] == 1)
            {
                cell[xRand, zRand].wall[i] = false;
                cell[newTileToWorkOn.x, newTileToWorkOn.z].wall[(i + 2) % 4] = false;
            }
        }
        if (counter > 0) CreatePassages(newTileToWorkOn.x, newTileToWorkOn.z);
        
    }

    //Returns all unvisited neighbours of a cell. Cell is defined by its position(xRand,zRand) in the grid.
    int[] GetUnvisitedNeighbours(int xRand, int zRand)
    {
        int[] neighboursToPickFrom = { 0, 0, 0, 0 };
        int noOfUnvisitedNeighbours = 0;
        for (int i = 0; i < 4; i++)
        {
            if (cell[xRand, zRand].neighbour[i])
            {
                int tempVerA = 0, tempVerB = 0;
                if (i == 0) tempVerA--;
                if (i == 1) tempVerB--;
                if (i == 2) tempVerA++;
                if (i == 3) tempVerB++;
                if (!cell[xRand + tempVerA, zRand + tempVerB].visited)
                {
                    neighboursToPickFrom[i] = 1;
                    noOfUnvisitedNeighbours++;
                }
            }
        }
        return neighboursToPickFrom;
    }
    //A script which takes in a cell, chooses a direction, walks that direction and selects this tile as the new Tile and makes
    //passage between these two cells.

}
