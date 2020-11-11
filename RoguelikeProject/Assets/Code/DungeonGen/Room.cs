using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("RoomInfo")]
    public RoomInfo roomInfo;
    public bool isStartRoom;
    public DungeonManager dungeonManager;
    public MapRoom mapRoom;
    [Header("Doors")]
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject upDoor;
    public GameObject downDoor;
    [Header("Debug")]
    public DungeonDoor leftDoorDebug;
    public DungeonDoor rightDoorDebug;
    public DungeonDoor upDoorDebug;
    public DungeonDoor downDoorDebug;


    private void Start()
    {
        if (isStartRoom)
        {
            dungeonManager.startRoom = this;
            mapRoom = dungeonManager.map.gameObject.GetComponent<MapRoom>();
            StartRoomGenerateRooms();
        }
    }

    public void GenerateRooms()
    {
        Vector3 curPos = transform.position;
        if (!Physics.Raycast(transform.position, -transform.right, roomInfo.roomDis) && dungeonManager.currentRoomAmount < dungeonManager.minRoomAmount && leftDoor != null)
        {
            //Left is clear.
            if(Random.Range(0, 100) < roomInfo.roomSpawnChance)
            {
                leftDoor.SetActive(true);
                
                Vector3 leftRoomPos = new Vector3(curPos.x - roomInfo.roomDis, curPos.y, curPos.z);
                if(Random.Range(0, 100) < roomInfo.itemRoomSpawnChance && dungeonManager.currentItemRoomAmount < dungeonManager.itemRoomAmount)
                {
                    //Room is itemroom
                    GameObject leftRoom = Instantiate(roomInfo.itemRoom, leftRoomPos, Quaternion.identity);
                    leftRoom.GetComponent<Room>().rightDoor.SetActive(true);
                    leftRoom.GetComponent<Room>().rightDoorDebug.roomBehindDoor = transform;
                    leftRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.itemRoomPicture, leftRoomPos / roomInfo.roomDis);
                    leftRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentItemRoomAmount++;
                    leftDoorDebug.roomBehindDoor = leftRoom.transform;
                }
                else if(Random.Range(0, 100) < roomInfo.shopRoomSpawnChance && dungeonManager.currentShopRoomAmount < dungeonManager.shopRoomAmount)
                {
                    //Room is Shoproom
                    GameObject leftRoom = Instantiate(roomInfo.shopRoom, leftRoomPos, Quaternion.identity);
                    leftRoom.GetComponent<Room>().rightDoor.SetActive(true);
                    leftRoom.GetComponent<Room>().rightDoorDebug.roomBehindDoor = transform;
                    
                    leftRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.shopRoomPicture, leftRoomPos / roomInfo.roomDis);
                    leftRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentShopRoomAmount++;
                    leftDoorDebug.roomBehindDoor = leftRoom.transform;
                }
                else
                {
                    //Make room on left.
                    GameObject leftRoom = Instantiate(roomInfo.roomsDoorRight[Random.Range(0, roomInfo.roomsDoorRight.Length - 1)], leftRoomPos, Quaternion.identity);
                    leftRoom.GetComponent<Room>().rightDoor.SetActive(true);
                    leftRoom.GetComponent<Room>().rightDoorDebug.roomBehindDoor = transform;

                    leftRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, leftRoomPos / roomInfo.roomDis);
                    leftRoom.GetComponent<Room>().GenerateRooms();
                    leftDoorDebug.roomBehindDoor = leftRoom.transform;
                }
                dungeonManager.currentRoomAmount++;
                
            }
            
        }


        if (!Physics.Raycast(transform.position, transform.right, roomInfo.roomDis) && dungeonManager.currentRoomAmount < dungeonManager.minRoomAmount && rightDoor != null)
        {
            //Right is clear.
            if (Random.Range(0, 100) < roomInfo.roomSpawnChance)
            {
                rightDoor.SetActive(true);
                
                Vector3 rightRoomPos = new Vector3(curPos.x + roomInfo.roomDis, curPos.y, curPos.z);
                if (Random.Range(0, 100) < roomInfo.itemRoomSpawnChance && dungeonManager.currentItemRoomAmount < dungeonManager.itemRoomAmount)
                {
                    //Room is itemroom
                    GameObject rightRoom = Instantiate(roomInfo.itemRoom, rightRoomPos, Quaternion.identity);
                    rightRoom.GetComponent<Room>().leftDoor.SetActive(true);
                    rightRoom.GetComponent<Room>().leftDoorDebug.roomBehindDoor = transform;

                    rightRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.itemRoomPicture, rightRoomPos / roomInfo.roomDis);
                    rightRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentItemRoomAmount++;
                    rightDoorDebug.roomBehindDoor = rightRoom.transform;
                }
                else if (Random.Range(0, 100) < roomInfo.shopRoomSpawnChance && dungeonManager.currentShopRoomAmount < dungeonManager.shopRoomAmount)
                {
                    //Room is Shoproom
                    GameObject rightRoom = Instantiate(roomInfo.shopRoom, rightRoomPos, Quaternion.identity);
                    rightRoom.GetComponent<Room>().leftDoor.SetActive(true);
                    rightRoom.GetComponent<Room>().leftDoorDebug.roomBehindDoor = transform;

                    rightRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.shopRoomPicture, rightRoomPos / roomInfo.roomDis);
                    rightRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentShopRoomAmount++;
                    rightDoorDebug.roomBehindDoor = rightRoom.transform;
                }
                else
                {
                    //Make room on right.
                    GameObject rightRoom = Instantiate(roomInfo.roomsDoorLeft[Random.Range(0, roomInfo.roomsDoorLeft.Length - 1)], rightRoomPos, Quaternion.identity);
                    rightRoom.GetComponent<Room>().leftDoor.SetActive(true);
                    rightRoom.GetComponent<Room>().leftDoorDebug.roomBehindDoor = transform;

                    rightRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, rightRoomPos / roomInfo.roomDis);
                    rightRoom.GetComponent<Room>().GenerateRooms();
                    rightDoorDebug.roomBehindDoor = rightRoom.transform;
                }
                dungeonManager.currentRoomAmount++;
            }
        }


        if (!Physics.Raycast(transform.position, transform.forward, roomInfo.roomDis) && dungeonManager.currentRoomAmount < dungeonManager.minRoomAmount && upDoor != null)
        {
            //Up is clear.
            if (Random.Range(0, 100) < roomInfo.roomSpawnChance)
            {
                upDoor.SetActive(true);
               
                Vector3 upRoomPos = new Vector3(curPos.x, curPos.y, curPos.z + roomInfo.roomDis);
                if (Random.Range(0, 100) < roomInfo.itemRoomSpawnChance && dungeonManager.currentItemRoomAmount < dungeonManager.itemRoomAmount)
                {
                    //Room is itemroom
                    GameObject upRoom = Instantiate(roomInfo.itemRoom, upRoomPos, Quaternion.identity);
                    upRoom.GetComponent<Room>().downDoor.SetActive(true);
                    upRoom.GetComponent<Room>().downDoorDebug.roomBehindDoor = transform;

                    upRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.itemRoomPicture, upRoomPos / roomInfo.roomDis);
                    upRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentItemRoomAmount++;
                    upDoorDebug.roomBehindDoor = upRoom.transform;
                }
                else if (Random.Range(0, 100) < roomInfo.shopRoomSpawnChance && dungeonManager.currentShopRoomAmount < dungeonManager.shopRoomAmount)
                {
                    //Room is Shoproom
                    GameObject upRoom = Instantiate(roomInfo.shopRoom, upRoomPos, Quaternion.identity);
                    upRoom.GetComponent<Room>().downDoor.SetActive(true);
                    upRoom.GetComponent<Room>().downDoorDebug.roomBehindDoor = transform;
                    upRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.shopRoomPicture, upRoomPos / roomInfo.roomDis);
                    upRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentShopRoomAmount++;
                    upDoorDebug.roomBehindDoor = upRoom.transform;
                }
                else
                {
                    //Make room on up.
                    GameObject upRoom = Instantiate(roomInfo.roomsDoorDown[Random.Range(0, roomInfo.roomsDoorDown.Length - 1)], upRoomPos, Quaternion.identity);
                    upRoom.GetComponent<Room>().downDoor.SetActive(true);
                    upRoom.GetComponent<Room>().downDoorDebug.roomBehindDoor = transform;
                    upRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, upRoomPos / roomInfo.roomDis);
                    upRoom.GetComponent<Room>().GenerateRooms();
                    upDoorDebug.roomBehindDoor = upRoom.transform;
                }
                dungeonManager.currentRoomAmount++;
            }
        }


        if (!Physics.Raycast(transform.position, -transform.forward, roomInfo.roomDis) && dungeonManager.currentRoomAmount < dungeonManager.minRoomAmount && downDoor != null)
        {
            //Down is clear.
            if (Random.Range(0, 100) < roomInfo.roomSpawnChance)
            {
                downDoor.SetActive(true);
                
                Vector3 downRoomPos = new Vector3(curPos.x, curPos.y, curPos.z - roomInfo.roomDis);
                if (Random.Range(0, 100) < roomInfo.itemRoomSpawnChance && dungeonManager.currentItemRoomAmount < dungeonManager.itemRoomAmount)
                {
                    //Room is itemroom
                    GameObject downRoom = Instantiate(roomInfo.itemRoom, downRoomPos, Quaternion.identity);
                    downRoom.GetComponent<Room>().upDoor.SetActive(true);
                    downRoom.GetComponent<Room>().upDoorDebug.roomBehindDoor = transform;

                    downRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.itemRoomPicture, downRoomPos / roomInfo.roomDis);
                    downRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentItemRoomAmount++;
                    downDoorDebug.roomBehindDoor = downRoom.transform;

                }
                else if (Random.Range(0, 100) < roomInfo.shopRoomSpawnChance && dungeonManager.currentShopRoomAmount < dungeonManager.shopRoomAmount)
                {
                    //Room is Shoproom
                    GameObject downRoom = Instantiate(roomInfo.shopRoom, downRoomPos, Quaternion.identity);
                    downRoom.GetComponent<Room>().upDoor.SetActive(true);
                    downRoom.GetComponent<Room>().upDoorDebug.roomBehindDoor = transform;
                    downRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.shopRoomPicture, downRoomPos / roomInfo.roomDis);
                    downRoom.GetComponent<Room>().GenerateRooms();
                    dungeonManager.currentShopRoomAmount++;
                    downDoorDebug.roomBehindDoor = downRoom.transform;

                }
                else
                {
                    //Make room on down.
                    GameObject downRoom = Instantiate(roomInfo.roomsDoorUp[Random.Range(0, roomInfo.roomsDoorUp.Length - 1)], downRoomPos, Quaternion.identity);
                    downRoom.GetComponent<Room>().upDoor.SetActive(true);
                    downRoom.GetComponent<Room>().upDoorDebug.roomBehindDoor = transform;
                    downRoom.GetComponent<Room>().dungeonManager = dungeonManager;
                    dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, downRoomPos / roomInfo.roomDis);
                    downRoom.GetComponent<Room>().GenerateRooms();
                    downDoorDebug.roomBehindDoor = downRoom.transform;
                }
                dungeonManager.currentRoomAmount++;
            }
        }
        dungeonManager.roomList.Add(transform.gameObject);
    }

    void StartRoomGenerateRooms()
    {
        Vector3 curPos = transform.position;

        //Make room on left.
        Vector3 leftRoomPos = new Vector3(curPos.x - roomInfo.roomDis, curPos.y, curPos.z);
        GameObject leftRoom = Instantiate(roomInfo.roomsDoorRight[Random.Range(0, roomInfo.roomsDoorRight.Length - 1)], leftRoomPos, Quaternion.identity);
        leftRoom.GetComponent<Room>().rightDoor.SetActive(true);
        leftRoom.GetComponent<Room>().rightDoorDebug.roomBehindDoor = transform;

        leftRoom.GetComponent<Room>().dungeonManager = dungeonManager;
        dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, leftRoomPos / roomInfo.roomDis);
        dungeonManager.currentRoomAmount++;
        leftDoorDebug.roomBehindDoor = leftRoom.transform;

        //Make room on right.
        Vector3 rightRoomPos = new Vector3(curPos.x + roomInfo.roomDis, curPos.y, curPos.z);
        GameObject rightRoom = Instantiate(roomInfo.roomsDoorLeft[Random.Range(0, roomInfo.roomsDoorLeft.Length - 1)], rightRoomPos, Quaternion.identity);
        rightRoom.GetComponent<Room>().leftDoor.SetActive(true);
        rightRoom.GetComponent<Room>().leftDoorDebug.roomBehindDoor = transform;
        
        rightRoom.GetComponent<Room>().dungeonManager = dungeonManager;
        dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, rightRoomPos / roomInfo.roomDis);
        dungeonManager.currentRoomAmount++;
        rightDoorDebug.roomBehindDoor = rightRoom.transform;

        //Make room on up.
        Vector3 upRoomPos = new Vector3(curPos.x, curPos.y, curPos.z + roomInfo.roomDis);
        GameObject upRoom = Instantiate(roomInfo.roomsDoorDown[Random.Range(0, roomInfo.roomsDoorDown.Length - 1)], upRoomPos, Quaternion.identity);
        upRoom.GetComponent<Room>().downDoor.SetActive(true);
        upRoom.GetComponent<Room>().downDoorDebug.roomBehindDoor = transform;

        upRoom.GetComponent<Room>().dungeonManager = dungeonManager;
        dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, upRoomPos / roomInfo.roomDis);
        dungeonManager.currentRoomAmount++;
        upDoorDebug.roomBehindDoor = upRoom.transform;

        //Make room on down.
        Vector3 downRoomPos = new Vector3(curPos.x, curPos.y, curPos.z - roomInfo.roomDis);
        GameObject downRoom = Instantiate(roomInfo.roomsDoorUp[Random.Range(0, roomInfo.roomsDoorUp.Length - 1)], downRoomPos, Quaternion.identity);
        downRoom.GetComponent<Room>().upDoor.SetActive(true);
        downRoom.GetComponent<Room>().upDoorDebug.roomBehindDoor = transform;
        
        downRoom.GetComponent<Room>().dungeonManager = dungeonManager;
        dungeonManager.map.CreateNewMapRoom(dungeonManager.map.roomPicture, downRoomPos / roomInfo.roomDis);
        dungeonManager.currentRoomAmount++;
        downDoorDebug.roomBehindDoor = downRoom.transform;


        leftRoom.GetComponent<Room>().GenerateRooms();
        rightRoom.GetComponent<Room>().GenerateRooms();
        upRoom.GetComponent<Room>().GenerateRooms();
        downRoom.GetComponent<Room>().GenerateRooms();
    }

    public void SpawnBossRoom()
    {
        
        Vector3 curPos = transform.position;
        if (!Physics.Raycast(transform.position, -transform.right, roomInfo.roomDis) && leftDoor != null && !dungeonManager.bossRoomSpawned)
        {
            
            //Left is clear.
            leftDoor.SetActive(true);
           
            Vector3 leftRoomPos = new Vector3(curPos.x - roomInfo.roomDis, curPos.y, curPos.z);
            GameObject leftRoom = Instantiate(roomInfo.bossRoom, leftRoomPos, Quaternion.identity);
            leftRoom.GetComponent<Room>().rightDoor.SetActive(true);
            leftRoom.GetComponent<Room>().rightDoorDebug.roomBehindDoor = transform;

            dungeonManager.map.CreateNewMapRoom(dungeonManager.map.bossRoomPicture, leftRoomPos / roomInfo.roomDis);
            dungeonManager.currentRoomAmount++;
            leftDoorDebug.roomBehindDoor = leftRoom.transform;
            dungeonManager.bossRoomSpawned = true;

        }
        if (!Physics.Raycast(transform.position, transform.right, roomInfo.roomDis) && rightDoor != null && !dungeonManager.bossRoomSpawned)
        {
            
            //Right is clear.
            rightDoor.SetActive(true);
            
            Vector3 rightRoomPos = new Vector3(curPos.x + roomInfo.roomDis, curPos.y, curPos.z);
            GameObject rightRoom = Instantiate(roomInfo.bossRoom, rightRoomPos, Quaternion.identity);
            rightRoom.GetComponent<Room>().leftDoor.SetActive(true);
            rightRoom.GetComponent<Room>().leftDoorDebug.roomBehindDoor = transform;

            dungeonManager.map.CreateNewMapRoom(dungeonManager.map.bossRoomPicture,rightRoomPos / roomInfo.roomDis);
            dungeonManager.currentRoomAmount++;
            rightDoorDebug.roomBehindDoor = rightRoom.transform;
            dungeonManager.bossRoomSpawned = true;

        }
        if (!Physics.Raycast(transform.position, transform.forward, roomInfo.roomDis) && upDoor != null && !dungeonManager.bossRoomSpawned)
        {
            
            //Up is clear.
            upDoor.SetActive(true);
            
            Vector3 upRoomPos = new Vector3(curPos.x, curPos.y, curPos.z + roomInfo.roomDis);
            GameObject upRoom = Instantiate(roomInfo.bossRoom, upRoomPos, Quaternion.identity);
            upRoom.GetComponent<Room>().downDoor.SetActive(true);
            upRoom.GetComponent<Room>().downDoorDebug.roomBehindDoor = transform;

            dungeonManager.map.CreateNewMapRoom(dungeonManager.map.bossRoomPicture, upRoomPos / roomInfo.roomDis);
            dungeonManager.currentRoomAmount++;
            upDoorDebug.roomBehindDoor = upRoom.transform;
            dungeonManager.bossRoomSpawned = true;

        }
        if (!Physics.Raycast(transform.position, -transform.forward, roomInfo.roomDis) && downDoor != null && !dungeonManager.bossRoomSpawned)
        {
            //Down is clear.
            
            downDoor.SetActive(true);
            
            Vector3 downRoomPos = new Vector3(curPos.x, curPos.y, curPos.z - roomInfo.roomDis);
            GameObject downRoom = Instantiate(roomInfo.bossRoom, downRoomPos, Quaternion.identity);
            downRoom.GetComponent<Room>().upDoor.SetActive(true);
            downRoom.GetComponent<Room>().upDoorDebug.roomBehindDoor = transform;

            dungeonManager.map.CreateNewMapRoom(dungeonManager.map.bossRoomPicture, downRoomPos / roomInfo.roomDis);
            dungeonManager.currentRoomAmount++;
            downDoorDebug.roomBehindDoor = downRoom.transform;
            dungeonManager.bossRoomSpawned = true;

        }

        

    }

}
