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
    public Camera camera;
    public CameraC cameraCode;
    public Vector3 currentCamPos;
    [Header("Orb")]
    public GameObject projectileOrb;
    public GameObject projectileOrbBase;
    public float orbRotateSpeed;
    [Header("Debug")]
    public float roomDis;
    RaycastHit hit;

    private void Start()
    {
        currentCamPos = cameraCode.transform.position;
    }

    private void Update()
    {
        RotateOrb();
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

    void RotateOrb()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = new Vector3(hit.point.x, projectileOrbBase.transform.position.y, hit.point.z) - projectileOrbBase.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(projectileOrbBase.transform.forward, targetDirection, orbRotateSpeed * Time.deltaTime, 0.0f);
            projectileOrbBase.transform.rotation = Quaternion.LookRotation(newDirection);
        }
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
