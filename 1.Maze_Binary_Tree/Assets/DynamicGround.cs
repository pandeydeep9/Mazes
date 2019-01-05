using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGround : MonoBehaviour {

    public GameObject groundTilePrefab;
    private GameObject groundTileInstance;

    public DynamicPlayerController playerPrefab;
    private DynamicPlayerController playerInstance;

    int noOfCol = 5;
    int noOfRows = 5;
    // Use this for initialization
    void Start()
    {
        ToGenerateMap();
    }



    public void ToGenerateMap()
    {
        StartCoroutine(BuildMaze());
    }

    private IEnumerator BuildMaze()
    {
        //mazeInstance = Instantiate(mazePrefab) as Maze;
        //StartCoroutine(mazeInstance.GenerateMaze());
        yield return StartCoroutine(GenerateMaze());
        playerInstance = Instantiate(playerPrefab) as DynamicPlayerController;
        playerInstance.transform.position = (new Vector3((noOfRows + 1)/2, 0.5f, (noOfCol+1)/2));
    }
    private void RebuildMaze()
    {
        StopAllCoroutines();
        //Destroy(mazeInstance.gameObject);
        StartCoroutine(BuildMaze());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RebuildMaze();
        }
        if(playerInstance != null)
        {
            //foreward direction one tile crossed
            if (playerInstance.zNewNeeded == -1)
            {
                Debug.Log("Make new Column j (z-1) val " + j);
                DestroyRow(j-5);
                MakeRow(j++);
                playerInstance.resetzNewNeeded();
            }
            //backward direction one tile crossed
            if (playerInstance.zNewNeeded == 1)
            {
                Debug.Log("Make new Column j val" + j);
                j--;
                DestroyRow(j);
                MakeRow(j-5);
                playerInstance.resetzNewNeeded();
            }
            //side x + direction one tile crossed
            if (playerInstance.xNewNeeded == -1)
            {
                Debug.Log("Make new row rowCount (z-1) val " + rowCount);
                DestroyColumn(rowCount - 5);
                MakeColumn(rowCount++);
                
                playerInstance.resetxNewNeeded();
            }
            //side x - direction one tile crossed
            if (playerInstance.xNewNeeded == 1)
            {
                Debug.Log("Make new Column j val" + rowCount);
                rowCount--;
                DestroyColumn(rowCount);
                MakeColumn(rowCount - 5);
                playerInstance.resetxNewNeeded();
            }
        }
       
    }
    int j = 0;
    int rowCount = 5,colCount =5;
    public IEnumerator GenerateMaze()
    {
        WaitForSeconds delay = new WaitForSeconds(.2f);
        //five columns
        for (j = 0; j < noOfCol; j++)
        {

            yield return delay;
            //make row
            MakeRow(j);
        }
        yield return delay;
    }

    public void MakeRow(int colNum)
    {
        for (int i = 0; i< 5; i++)
        {
            CreateCell(i, colNum);
}
    }
    public void DestroyRow(int colNum)
    {
        for (int i = 0; i < 5; i++)
        {
            DestroyCell(i, colNum);
        }
    }
    public void MakeColumn(int rowNum)
    {
        for (int i = 0; i < 5; i++)
        {
            CreateCell(rowNum, i);
        }
    }
    public void DestroyColumn(int rowNum)
    {
        for (int i = 0; i < 5; i++)
        {
            DestroyCell(rowNum, i);
        }
    }
    private void CreateCell(int x , int z)
    {
        GameObject tile = Instantiate(groundTilePrefab) as GameObject;
        tile.name = "GroundTile " + x + ", " + z;
        tile.transform.parent = transform;
        tile.transform.position = new Vector3(x, 0, z);
        //tile.transform.localScale = new Vector3(tileSize.x, tileSize.z, tile.transform.localScale.y);
    }
    private void DestroyCell(int x, int z)
    {
        string name = "GroundTile " + x + ", " + z;
        GameObject tile = transform.Find(name).gameObject;
        Destroy(tile);
        //tile.transform.localScale = new Vector3(tileSize.x, tileSize.z, tile.transform.localScale.y);
    }
}
