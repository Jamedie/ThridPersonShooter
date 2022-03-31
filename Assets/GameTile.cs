using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    private static Quaternion
    northRotation = Quaternion.Euler(90f, 0f, 0f),
    eastRotation = Quaternion.Euler(90f, 90f, 0f),
    southRotation = Quaternion.Euler(90f, 180f, 0f),
    westRotation = Quaternion.Euler(90f, 270f, 0f);

    [SerializeField] private Transform arrow = default;

    private GameTile north, east, south, west, nextOnPath;
    private int distance;

    public bool HasPath => distance != int.MaxValue;

    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        Debug.Assert(
            west.east == null && east.west == null, "Redefined neighbors!"
        );
        west.east = east;
        east.west = west;
    }

    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        Debug.Assert(
            south.north == null && north.south == null, "Redefined neighbors!"
        );
        south.north = north;
        north.south = south;
    }

    public GameTile GrowPathNorth() => GrowPathTo(north);

    public GameTile GrowPathEast() => GrowPathTo(east);

    public GameTile GrowPathSouth() => GrowPathTo(south);

    public GameTile GrowPathWest() => GrowPathTo(west);

    public void ClearPath()
    {
        distance = int.MaxValue;
        nextOnPath = null;
    }

    public void ShowPath()
    {
        if (distance == 0)
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);
        arrow.localRotation =
            nextOnPath == north ? northRotation :
            nextOnPath == east ? eastRotation :
            nextOnPath == south ? southRotation :
            westRotation;
    }

    public void BecomeDestination()
    {
        distance = 0;
        nextOnPath = null;
    }

    private GameTile GrowPathTo(GameTile neighbor)
    {
        Debug.Assert(HasPath, "No path!");
        if (neighbor == null || neighbor.HasPath)
        {
            return null;
        }
        neighbor.distance = distance + 1;
        neighbor.nextOnPath = this;
        return neighbor;
    }
}