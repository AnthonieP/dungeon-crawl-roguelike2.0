using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;
    public bool targetIsCamera;

    void Start()
    {
        if (targetIsCamera)
        {
            target = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
