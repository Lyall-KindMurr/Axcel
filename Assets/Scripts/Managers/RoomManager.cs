using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Tooltip("Put the grid on which you want these tiles to placed on FROM THE SCENE")]
    public GameObject tilesGrid;

    [Header("Rooms included in the generation")]
    public GameObject[] corridors;
    public GameObject[] rooms;

    private Transform lastRoom;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpawn();
        GenerateRooms();
    }

    private void GenerateSpawn()
    {
        // spawn is a little lower than you think
        GameObject _lastroom = Instantiate(corridors[0], tilesGrid.transform.position + new Vector3(-11f,-5f,0f), Quaternion.identity, tilesGrid.transform);
        lastRoom = _lastroom.transform.Find("Exit");
        Debug.Log(lastRoom.position);
    }

    private void GenerateRooms()
    {
        // for the time being, we will generate either a room, or a corridor
        int Roomtype = Random.Range(0, 2);

        if (Roomtype == 0) // corridors
        {
            GameObject _lastroom = Instantiate(corridors[0], tilesGrid.transform.position + lastRoom.position, Quaternion.identity, tilesGrid.transform);
            lastRoom = _lastroom.transform.Find("Exit");
            Debug.Log(lastRoom.position);
        }
        else if (Roomtype == 1) // rooms
        {
            GameObject _lastroom = Instantiate(rooms[0], tilesGrid.transform.position + lastRoom.position, Quaternion.identity, tilesGrid.transform);
            lastRoom = _lastroom.transform.Find("Exit");
            Debug.Log(lastRoom.position);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("g"))
        {
            GenerateRooms();
        }
    }
}
