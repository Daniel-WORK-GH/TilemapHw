using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int>
{
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    public bool IsAllowed(Vector3Int node)
    {
        TileBase neighborTile = tilemap.GetTile(node);
        return allowedTiles.Contains(neighborTile);
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        int xOffset = 0;
        if (node.y % 2 != 0)
            xOffset = 1;
            
        List<Vector3Int> List = new List<Vector3Int>
        {
            // On the same row.
            new Vector3Int(node.x - 1, node.y),
            new Vector3Int(node.x + 1, node.y),

            // 2 in row above
            new Vector3Int(node.x + xOffset - 1, node.y + 1),
            new Vector3Int(node.x + xOffset, node.y + 1),

            // 2 in row below
            new Vector3Int(node.x + xOffset - 1, node.y - 1),
            new Vector3Int(node.x + xOffset, node.y - 1)
        };


        foreach (var pos in List)
        {             
            TileBase neighborTile = tilemap.GetTile(pos);
            Debug.Log("pos = " + pos + ", allowed = " + allowedTiles.Contains(neighborTile));
            Debug.Log("len = " + allowedTiles.Length);

            if (allowedTiles.Contains(neighborTile))
                yield return pos;
        }
    }
}
