using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public float pauseBetweenEach = 0.01f;
    public GroundTile groundTilePrefab;
    public WallTile wallTilePrefab;
    public WallTile wallEastTilePrefab;

    public IntToVector2 sizeOfMaze;
    public IntToVector2 tileSize;

    public IEnumerator GenerateMaze()
    {
        WaitForSeconds delay = new WaitForSeconds(pauseBetweenEach);
        for(int i = 0;i < sizeOfMaze.x; i++)
        {
            for(int j = 0; j < sizeOfMaze.z; j++)
            {
                //yield return delay;//for looking at how the map is generated 
                CreateCell(new IntToVector2(i,j));
                if(i != sizeOfMaze.x-1 && j != sizeOfMaze.z -1) CreateWalls(new IntToVector2(i, j));
                //GameObject gTile = Instantiate(groundTile) as GameObject;

            }
        }
        yield return delay;
    }

	private void CreateCell(IntToVector2 coordinates)
    {
        GroundTile tile = Instantiate(groundTilePrefab) as GroundTile;
        tile.name = "GroundTile " + coordinates.x +  ", " + coordinates.z;
        tile.transform.parent = transform;
        tile.transform.position = new Vector3(tileSize.x * coordinates.x, 0, tileSize.z*coordinates.z);
        tile.transform.localScale = new Vector3(tileSize.x, tileSize.z , tile.transform.localScale.y);
    }
    private void CreateWalls(IntToVector2 coordinates)
    {

        float randomVal = Random.Range(0f, 1f);
        //Debug.Log(randomVal);
        bool NorthWall = randomVal > .5f ? true : false;
        //Make walls
        if (NorthWall)
        {
            WallTile wallNorth = Instantiate(wallTilePrefab) as WallTile;
            wallNorth.name = "WallNorth" + coordinates.x + ", " + coordinates.z;
            wallNorth.transform.parent = transform;
            wallNorth.transform.position = new Vector3(tileSize.x * coordinates.x, .5f, tileSize.z * coordinates.z + 0.5f * tileSize.z);
            wallNorth.transform.localScale = new Vector3(tileSize.x, wallNorth.transform.localScale.y, wallNorth.transform.localScale.z);
        }
        else if (!NorthWall)
        {
            WallTile wallEast = Instantiate(wallEastTilePrefab) as WallTile;
            wallEast.name = "WallEast" + coordinates.x + ", " + coordinates.z;
            wallEast.transform.parent = transform;
            //wallEast.transform.rotation 
            wallEast.transform.position = new Vector3(tileSize.x * coordinates.x + 0.5f * tileSize.x, .5f, tileSize.z * coordinates.z);
            wallEast.transform.localScale = new Vector3(tileSize.z, wallEast.transform.localScale.y, wallEast.transform.localScale.z);
        }



    }
}
