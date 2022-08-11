using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Tooltip("Put the grid on which you want these tiles to placed on FROM THE SCENE")]
    public GameObject tilesGrid;

    [Header("Rooms included in the generation")]
    public GameObject Spawn;
    public GameObject[] corridors;
    public GameObject[] rooms;
    public GameObject EndRoom;
    public int roomsToGenerate;


    private Transform lastRoom;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpawn();
        for (int i = 0; i < roomsToGenerate - 1; i++)
        {
            GenerateRooms();
        }
        GenerateFinalRoom();
    }

    private void GenerateSpawn()
    {
        // spawn is a little lower than you think
        GameObject _lastroom = Instantiate(Spawn, tilesGrid.transform.position + new Vector3(-11f,-5f,0f), Quaternion.identity, tilesGrid.transform);
        lastRoom = _lastroom.transform.Find("Exit");
        Debug.Log(lastRoom.position);
    }

    private void GenerateRooms()
    {
        // for the time being, we will generate either a room, or a corridor
        int Roomtype = Random.Range(0, 3);

        if (Roomtype == 0 || Roomtype == 1) // corridors
        {
            GameObject _lastroom = Instantiate(corridors[Roomtype], tilesGrid.transform.position + lastRoom.position, Quaternion.identity, tilesGrid.transform);
            lastRoom = _lastroom.transform.Find("Exit");
            Debug.Log(lastRoom.position);
        }
        else if (Roomtype == 2) // rooms
        {
            GameObject _lastroom = Instantiate(rooms[0], tilesGrid.transform.position + lastRoom.position, Quaternion.identity, tilesGrid.transform);
            lastRoom = _lastroom.transform.Find("Exit");
            Debug.Log(lastRoom.position);
        }
    }

    private void GenerateFinalRoom()
    {
        GameObject _lastroom = Instantiate(EndRoom, tilesGrid.transform.position + lastRoom.position, Quaternion.identity, tilesGrid.transform);
        lastRoom = _lastroom.transform.Find("Exit");
        Debug.Log(lastRoom.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown("g"))
        {
            GenerateRooms();
        }
    }
}
