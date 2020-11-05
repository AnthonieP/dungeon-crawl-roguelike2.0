using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    [Header("Settings")]
    public float roomDistance;
    public GameObject parent;
    public DungeonManager dungeonManager;
    [Header("Rooms")]
    public GameObject roomPicture;
    public GameObject itemRoomPicture;
    public GameObject shopRoomPicture;
    public GameObject bossRoomPicture;
    

    public void CreateNewMapRoom(GameObject roomToCreate, Vector3 coords)
    {
        Vector3 createPos = new Vector3(transform.position.x + (coords.x * roomDistance), transform.position.y + (coords.z *roomDistance), transform.position.z);
        GameObject mapObject = Instantiate(roomToCreate, createPos, Quaternion.identity, parent.transform);
        
    }
}
