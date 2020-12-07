using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class enemy
    {
        public GameObject enemyObj;
        public float healthMult = 1;
        public float damageMult = 1;
    }
    public enemy[] enemies;
    public bool enemyDied = false;
    public Room currentRoom;

    public void SpawnEnemy()
    {
        int random = Random.Range(0, enemies.Length - 1);
        AI spawn = Instantiate(enemies[random].enemyObj, transform.position, Quaternion.identity).GetComponent<AI>();
        spawn.spawnPoint = this;
        spawn.pathfinding = currentRoom.aStar;
        spawn.damage *= enemies[random].damageMult;
        spawn.health *= enemies[random].healthMult;
    }

    public void EnemyDied()
    {
        enemyDied = true;
        currentRoom.CheckRoomUnlock();
    }
}
