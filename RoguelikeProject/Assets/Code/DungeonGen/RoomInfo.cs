using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/RoomData", order = 1)]
public class RoomInfo : ScriptableObject
{
    [Header("Settings")]
    public float roomDis;
    public float roomSpawnChance;
    public float itemRoomSpawnChance;
    public float shopRoomSpawnChance;
    [Header("RoomObjects")]
    public GameObject[] roomsDoorLeft;
    public GameObject[] roomsDoorRight;
    public GameObject[] roomsDoorUp;
    public GameObject[] roomsDoorDown;
    public GameObject itemRoom;
    public GameObject shopRoom;
    public GameObject bossRoom;

}
