using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform ground = default;
    [SerializeField] private GameTile tilePrefab = default;

    private Vector2Int size;
    private GameTile[] tiles;

    private Queue<GameTile> searchFrontier = new Queue<GameTile>();

    public void Initialize(Vector2Int size)
    {
        this.size = size;
        ground.localScale = new Vector3(size.x, size.y, 1f);
        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        tiles = new GameTile[size.x * size.y];

        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameTile tile = tiles[i] = tilePrefab.Spawn();
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if (x > 0)
                {
                    GameTile.MakeEastWestNeighbors(tile, tiles[i - 1]);
                }
                if (y > 0)
                {
                    GameTile.MakeNorthSouthNeighbors(tile, tiles[i - size.x]);
                }
            }
        }
        FindPaths();
    }

    private void FindPaths()
    {
        foreach (GameTile tile in tiles)
        {
            tile.ClearPath();
        }
        tiles[0].BecomeDestination();
        searchFrontier.Enqueue(tiles[0]);
        while (searchFrontier.Count > 0)
        {
            GameTile tileForGrow = searchFrontier.Dequeue();
            if (tileForGrow != null)
            {
                searchFrontier.Enqueue(tileForGrow.GrowPathNorth());
                searchFrontier.Enqueue(tileForGrow.GrowPathSouth());
                searchFrontier.Enqueue(tileForGrow.GrowPathEast());
                searchFrontier.Enqueue(tileForGrow.GrowPathWest());
            }
        }

        foreach (GameTile tile in tiles)
        {
            tile.ShowPath();
        }
    }
}