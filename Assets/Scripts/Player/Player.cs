using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("The player speed")]
    public float speed = 1;


    [Tooltip("The key to press for moving left.")]
    public KeyCode left;

    [Tooltip("The key to press for moving right.")]
    public KeyCode right;

    [Tooltip("The key to press for moving up.")]
    public KeyCode up;

    [Tooltip("The key to press for moving down.")]
    public KeyCode down;


    [Tooltip("How many tiles the player needs to be connected to.")]
    [Range(1, 500)]
    public int count;

    [Tooltip("Tile map.")]
    public Tilemap tilemap = null;
    [Tooltip("Allowed tiles.")]
    public AllowedTiles allowedTiles = null;

    private TilemapGraph tilegraph = null;

    private Vector2 targetPosition;

    private bool isMoving = false;

    private bool isValidSpawn = false;

    void Start()
    {
        targetPosition = transform.position;
        tilegraph = new TilemapGraph(tilemap, allowedTiles.Get());
    }

    void ValidateSpawn()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        if (!BFS.CheckConnectedToXTiles(tilegraph, startNode, 100))
        {
            transform.position = tilemap.CellToWorld(new Vector3Int(Random.Range(0, tilemap.size.x), Random.Range(0, tilemap.size.y), 0));
        }
        else
        {
            isValidSpawn = true;
        }
    }

    void Update()
    {
        if (!isValidSpawn)
        {
            ValidateSpawn();
            return;
        }

        if (Input.GetKey(left))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(right))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(up))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(down))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
