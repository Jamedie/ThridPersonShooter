using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] Walls;
    public GameObject[] Doors;
    public GameObject[] Floors;

    public int MinRoomSize = 2;
    public int MaxRoomSize = 15;
    public int MaxDoor = 3;

    public Transform FloorParent;
    public Transform WallParent;
    public Transform EntranceParent;

    public Vector2 _roomSize;

    [ContextMenu("CreateRoom")]
    public void CreateRoom()
    {
        StartCoroutine("CreateRoomCo");
    }

    private IEnumerator CreateRoomCo()
    {
        //Create Floors
        _roomSize = new Vector2(Random.Range(MinRoomSize, MaxRoomSize), Random.Range(MinRoomSize, MaxRoomSize));

        float floorSizeX = Floors[0].GetComponent<Renderer>().bounds.size.x;
        float floorSizeZ = Floors[0].GetComponent<Renderer>().bounds.size.z;

        for (int x = 0; x < _roomSize.x; x++)
        {
            for (int y = 0; y < _roomSize.y; y++)
            {
                Instantiate(Floors[Random.Range(0, Floors.Length)], position: new Vector3(x * floorSizeX, 0, y * floorSizeZ), rotation: Quaternion.identity, FloorParent).transform.name = "Floor [" + x + "," + y + "]";
            }
            yield return new WaitForSeconds(0.01f);
        }

        //Create Walls

        for (int x = 0; x < _roomSize.x; x++)
        {
            for (int y = 0; y < _roomSize.y; y++)
            {
                //check all side of the cell
                if (x - 1 < 0)
                {
                    Instantiate(Walls[Random.Range(0, Walls.Length)], position: new Vector3(x * floorSizeX - floorSizeX, 0, y * floorSizeZ), rotation: Quaternion.Euler(0, 90, 0), WallParent).transform.name = "Right Wall";
                }
                if (x + 2 > _roomSize.x)
                {
                    Instantiate(Walls[Random.Range(0, Walls.Length)], position: new Vector3(x * floorSizeX, 0, y * floorSizeZ + floorSizeZ), rotation: Quaternion.Euler(0, -90, 0), WallParent).transform.name = "Right Wall";
                }
                if (y - 1 < 0)
                {
                    Instantiate(Walls[Random.Range(0, Walls.Length)], position: new Vector3(x * floorSizeX, 0, y * floorSizeZ), rotation: Quaternion.Euler(0, 0, 0), WallParent).transform.name = "Top Wall";
                }
                if (y + 2 > _roomSize.y)
                {
                    Instantiate(Walls[Random.Range(0, Walls.Length)], position: new Vector3(x * floorSizeX - floorSizeX, 0, y * floorSizeZ + floorSizeZ), rotation: Quaternion.Euler(0, 180, 0), WallParent).transform.name = "Down Wall";
                }
            }
            yield return new WaitForSeconds(0.01f);

            //Create Door
        }
    }

    // Update is called once per frame
    private void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i])
            {
                Walls[i].SetActive(false);
                Doors[i].SetActive(true);
            }
            else
            {
                Walls[i].SetActive(true);
                Doors[i].SetActive(false);
            }
        }
    }
}