using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This is the main controller script. This script gets references of maze, player, destination, mazeSize and tilesize.
//It passes mazeSize and tileSize to maze script.
public class MazeController : MonoBehaviour
{

    public Maze mazePrefab;
    private Maze mazeInstance;

    public PlayerController playerPrefab;
    private PlayerController playerInstance;

    public GameObject goalToReach;
    private GameObject goalInstance;

    public IntToVector2 sizeOfMaze;

    public IntToVector2 tileSize;
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
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);


        mazeInstance = Instantiate(mazePrefab) as Maze;
        //StartCoroutine(mazeInstance.GenerateMaze());
        yield return null;//WaitForSeconds(0.0) 
        mazeInstance.GenerateMaze(sizeOfMaze, tileSize);
        playerInstance = Instantiate(playerPrefab) as PlayerController;
        playerInstance.transform.position = (new Vector3((sizeOfMaze.x - 1) * tileSize.x, 0.5f, (sizeOfMaze.z - 1) * tileSize.z));

      //  goalInstance = Instantiate(goalToReach) as GameObject;
      //  goalInstance.transform.position = new Vector3(0, 0.5f, tileSize.z);
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }
    private void RebuildMaze()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        StartCoroutine(BuildMaze());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RebuildMaze();
        }

    }
}
