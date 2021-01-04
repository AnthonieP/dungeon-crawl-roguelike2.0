using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Header("Pathfinding")]
    public AStar pathfinding;
    public GameObject target;
    public List<Vector3> waypoints = new List<Vector3>();
    int waypointcount = 3;
    public float distanceForNextWaypoint;
    public float pathUpdateTime;
    float pathUpdateTimeTime;
    [Header("Movement")]
    public float moveSpeed;
    public float rotateSpeed;
    [Header("Stats")]
    public float health;
    public float damage;
    public float knockbackMul;
    [Header("Misc")]
    public EnemySpawn spawnPoint;

    private void Start()
    {
        
    }
    void Update()
    {
        pathUpdateTimeTime -= Time.deltaTime;
        if(pathUpdateTimeTime < 0)
        {
            NewPath();
            pathUpdateTimeTime = pathUpdateTime;
        }

        if (waypointcount < waypoints.Count)
        {
            ManageWaypoints();
            if (waypointcount < waypoints.Count)
            {
                Move();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < .1f)
        {
            Die();
        }
    }

    public void TakeKnockback(float knockback, Vector3 knockbackDirection)
    {
        transform.GetComponent<Rigidbody>().AddForce(knockbackDirection * (knockback * knockbackMul));
    }

    void Die()
    {
        spawnPoint.EnemyDied();



        Destroy(gameObject);
    }

    void NewPath()
    {
        waypoints = new List<Vector3>();
        pathfinding.Pathfind(GetStartTile(), GetEndTile(), transform);
        waypointcount = 3;
    }

    void ManageWaypoints()
    {
        if(Vector3.Distance(new Vector3(transform.position.x , transform.position.y, transform.position.z), new Vector3 (waypoints[waypointcount].x, transform.position.y, waypoints[waypointcount].z)) < distanceForNextWaypoint)
        {
            waypointcount += 1;
        }
    }

    GridTile GetStartTile()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.5f, 10, .5f));
        GridTile startTile = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].transform.tag == "GridTile")
            {
                startTile = colliders[i].GetComponent<GridTile>();
            }
        }

        return startTile;
    }

    GridTile GetEndTile()
    {
        Collider[] colliders = Physics.OverlapBox(target.transform.position, new Vector3(.5f, 10, .5f));
        GridTile startTile = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.tag == "GridTile")
            {
                startTile = colliders[i].GetComponent<GridTile>();
            }
        }

        return startTile;
    }

    void Move()
    {
        RotateTowards(waypoints[waypointcount]);
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    void RotateTowards(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, rotateSpeed * Time.deltaTime, 0.0f));
    }
}
