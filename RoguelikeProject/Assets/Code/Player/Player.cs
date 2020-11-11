using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerInfo")]
    public float playerSpeed;
    [Header("Stats")]
    public float speed;
    public float attackPower;
    public float attackSpeed;
    [Header("Camera")]
    public CameraC cameraCode;
    public Vector3 currentCamPos;
    [Header("Debug")]
    public float roomDis;

    private void Start()
    {
        currentCamPos = cameraCode.transform.position;
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(input * playerSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Door")
        {
            cameraCode.target = collision.transform.GetComponent<DungeonDoor>().roomBehindDoor.position + currentCamPos;
            transform.position += collision.transform.GetComponent<DungeonDoor>().doorDirection * roomDis;
        }
    }
}
