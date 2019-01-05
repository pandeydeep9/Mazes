using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour {

    public Maze mazePrefab;
    private Maze mazeInstance;

    public PlayerController playerPrefab;
    private PlayerController playerInstance;

	// Use this for initialization
	void Start () {

        ToGenerateMap();
	}

    public void ToGenerateMap()
    {
        StartCoroutine(BuildMaze());
    }

    private IEnumerator BuildMaze()
    {
        //Camera.main.clearFlags = CameraClearFlags.Skybox;
        //Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        

        mazeInstance = Instantiate(mazePrefab) as Maze;
        //StartCoroutine(mazeInstance.GenerateMaze());
        yield return StartCoroutine(mazeInstance.GenerateMaze());
        //playerInstance = Instantiate(playerPrefab) as PlayerController;
        //playerInstance.transform.position = (new Vector3(0,0.5f,0));
        //Camera.main.clearFlags = CameraClearFlags.Depth;
        //Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }
    private void RebuildMaze()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        StartCoroutine(BuildMaze());
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RebuildMaze();
        }
		
	}
}
