using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public float pauseBetweenEach = 0.01f;
    public GroundCell groundTilePrefab;
    public WallTile wallTilePrefab;
    public WallTile wallEastTilePrefab;

    public IntToVector2 sizeOfMaze;
    public IntToVector2 tileSize;

    public IEnumerator GenerateMaze()
    {
        WaitForSeconds delay = new WaitForSeconds(.02f);
        for(int j = 0;j < sizeOfMaze.z; j++)
        {
            for(int i = 0; i < sizeOfMaze.x; i++)
            {
                yield return delay;//for looking at how the map is generated 
                CreateCell(new IntToVector2(i,j));

				//Binary Treee Way
				//if( j != sizeOfMaze.z -1 && i!= sizeOfMaze.x -1) CreateWalls(new IntToVector2(i, j));
            }
        }
        yield return delay;
    }

    private void CreateCell(IntToVector2 coordinates)
    {
        GroundCell cell = Instantiate(groundTilePrefab) as GroundCell;

        cell.name = "GroundTile " + coordinates.x + ", " + coordinates.z;
        cell.transform.parent = transform;
        cell.setRowColumn(coordinates.x, coordinates.z);
        bool[] nebDet = { (coordinates.z < sizeOfMaze.z-1 ? true : false), (coordinates.x < sizeOfMaze.x-1 ? true : false), //false, false};
            (coordinates.z == 0 ? false: true), (coordinates.x == 0? false: true)};
        cell.nebInfo(nebDet);
        cell.transform.position = new Vector3(tileSize.x * coordinates.x, 0, tileSize.z*coordinates.z);
        cell.transform.localScale = new Vector3(tileSize.x, tileSize.z , cell.transform.localScale.y);

		if(cell.neighbour[0]) CreateWalls(coordinates);
    }
    public Stack<IntToVector2> tileArrayStack = new Stack<IntToVector2>();
    int randomOneToSkip,counterForRandom =1;
    private void CreateWalls(IntToVector2 coordinates)
    {
        // flip a coin(east or north). If east, push the northen wall to stack. 
		//If north, ignoring one northen wall, make all other northen walls
		float randomVal = Random.Range(0f, 1f);
        bool walkEast = randomVal > .5f ? true : false;
        //Wall Logic
        if (walkEast)
        {
			tileArrayStack.Push(coordinates);
			if(coordinates.x == sizeOfMaze.x-1){
				//East Edge reached
				makeNorthenWalls ();
			}
				
        }
        else if (!walkEast)
        {
			if(coordinates.x != sizeOfMaze.x- 1)createEastWall(coordinates);
            tileArrayStack.Push(coordinates);
			makeNorthenWalls();
        }
    }
	private void makeNorthenWalls(){
		randomOneToSkip = Random.Range(1, tileArrayStack.Count + 1);
		while(tileArrayStack.Count != 0)
		{
			counterForRandom++;
			IntToVector2 coord = tileArrayStack.Pop();
			if (randomOneToSkip == tileArrayStack.Count+1) continue;
			createNorthWall(coord);

		}
	}
	//East wall is green 
    private void createEastWall(IntToVector2 coordinates)
    {
        WallTile wallEast = Instantiate(wallEastTilePrefab) as WallTile;
        wallEast.name = "WallEast" + coordinates.x + ", " + coordinates.z;
        wallEast.transform.parent = transform;
        wallEast.transform.position = new Vector3(tileSize.x * coordinates.x + 0.5f * tileSize.x, .5f, tileSize.z * coordinates.z);
        wallEast.transform.localScale = new Vector3(tileSize.z, wallEast.transform.localScale.y, wallEast.transform.localScale.z);
    }
	//North wall is blue
    private void createNorthWall(IntToVector2 coordinates)
    {
        WallTile wallNorth = Instantiate(wallTilePrefab) as WallTile;
        wallNorth.name = "WallNorth" + coordinates.x + ", " + coordinates.z;
        wallNorth.transform.parent = transform;
        wallNorth.transform.position = new Vector3(tileSize.x * coordinates.x, .5f, tileSize.z * coordinates.z + 0.5f * tileSize.z);
        wallNorth.transform.localScale = new Vector3(tileSize.x, wallNorth.transform.localScale.y, wallNorth.transform.localScale.z);
    }
}
