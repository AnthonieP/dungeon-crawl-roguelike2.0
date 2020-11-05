using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [Header("Settings")]
    public int minRoomAmount;
    public int maxRoomAmount;
    public int itemRoomAmount;
    public int shopRoomAmount;
    public int framesAfterRestartCheck;
    public DungeonMap map;
    [Header("DebugData")]
    public int framesElapsed;
    public bool genComplete;
    public int currentRoomAmount;
    public int currentItemRoomAmount;
    public int currentShopRoomAmount;
    public bool bossRoomSpawned;
    public Room startRoom;
    public List<GameObject> roomList = new List<GameObject>();
    
    void Update()
    {
        if(framesElapsed < framesAfterRestartCheck)
        {
            framesElapsed++;
        }
        else if (!genComplete)
        {
            if(currentRoomAmount < minRoomAmount || currentRoomAmount > maxRoomAmount || currentItemRoomAmount != itemRoomAmount || currentShopRoomAmount != shopRoomAmount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (!bossRoomSpawned)
            {
                Room farthestRoom = roomList[0].GetComponent<Room>();
                for (int i = 0; i < roomList.Count; i++)
                {
                    if(Vector3.Distance(roomList[i].transform.position, startRoom.transform.position) > Vector3.Distance(farthestRoom.transform.position, startRoom.transform.position))
                    {
                        farthestRoom = roomList[i].GetComponent<Room>();
                    }
                }
                farthestRoom.SpawnBossRoom();

            }
            if (!bossRoomSpawned)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            genComplete = true;
        }
    }
}
