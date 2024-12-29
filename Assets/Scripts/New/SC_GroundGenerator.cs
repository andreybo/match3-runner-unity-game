using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GroundGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; // Point from where ground tiles will start
    public SC_PlatformTile[] tilePrefabs; // Array of tile prefabs
    public int tilesToPreSpawn = 15; // How many tiles should be pre-spawned
    //public int tilesWithoutObstacles = 3; // How many tiles at the beginning should not have obstacles, good for warm-up

    List<SC_PlatformTile> spawnedTiles = new List<SC_PlatformTile>();
    [HideInInspector]
    public bool gameOver = false;
    static bool gameStarted = false;
    float score = 0;

    public static SC_GroundGenerator instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (GameStatus.Instance.isLevelNew)
        {
            genNew();
        }
        else
        {
            genOld();
        }
    }

    void genNew(){
        Vector3 spawnPosition = startPoint.position;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            int prefabIndex = Random.Range(0, tilePrefabs.Length);
            PlayerPrefs.SetInt("tile" + i, prefabIndex);
            spawnPosition -= tilePrefabs[prefabIndex].startPoint.localPosition;
            SC_PlatformTile spawnedTile = Instantiate(tilePrefabs[prefabIndex], spawnPosition, Quaternion.identity) as SC_PlatformTile;
            spawnedTile.ActivateRandomObstacle();
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTile.MoveToBack();
            spawnedTiles.Add(spawnedTile);
        }
    }

    void genOld(){
        Vector3 spawnPosition = startPoint.position;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            int prefabIndex = PlayerPrefs.GetInt("tile" + i);
            spawnPosition -= tilePrefabs[prefabIndex].startPoint.localPosition;
            SC_PlatformTile spawnedTile = Instantiate(tilePrefabs[prefabIndex], spawnPosition, Quaternion.identity) as SC_PlatformTile;
            spawnedTile.ActivateRandomObstacle();
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTile.MoveToBack();
            spawnedTiles.Add(spawnedTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            // Move the tile to the front if it's behind the Camera
            SC_PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            spawnedTiles.Add(tileTmp);
            tileTmp.MoveToBack();
        }
    }
}
