using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The Enemy prefab to spawn")]
    public GameObject enemyPrefab;

    public Vector2 spawnOffset = Vector2.zero;

    public Tilemap tileMap;

    public Transform target;

    public AllowedTiles allowedTiles;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 spawnPosition = new Vector2(worldPosition.x, worldPosition.y) + spawnOffset;

            var obj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);    

            var chase = obj.GetComponent<Chaser>();

            chase.targetObject = target;
            chase.tilemap = tileMap;
            chase.allowedTiles = allowedTiles;
        }
    }
}
