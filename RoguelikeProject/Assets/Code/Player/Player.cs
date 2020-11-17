using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerInfo")]
    public int money;
    [Header("Stats")]
    public float maxHealth;
    public float curHealth;
    public float speed;
    public float attackPower;
    public float attackSpeed;
    public float attackTime;
    public float fireRate;
    [Header("ItemStuff")]
    public float interactRange;
    [Header("Camera")]
    public Camera camera;
    public CameraC cameraCode;
    public Vector3 currentCamPos;
    [Header("Orb")]
    public GameObject projectileOrb;
    public GameObject projectileOrbBase;
    public GameObject projectile;
    public float orbRotateSpeed;
    [Header("Canvas")]
    public GameObject healthBar;
    public GameObject tooltip;
    [Header("Debug")]
    public float roomDis;
    RaycastHit hit;
    float fireRateTime = 0;

    private void Start()
    {
        currentCamPos = cameraCode.transform.position;
    }

    private void Update()
    {
        RotateOrb();

        fireRateTime -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireRateTime < 0)
        {
            FireProjectile();
            fireRateTime = fireRate;
        }
        if (Input.GetButtonDown("Interact"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
            bool foundInteract = false;
            for (int i = 0; i < colliders.Length && !foundInteract; i++)
            {
                if(colliders[i].transform.tag == "interactable")
                {
                    foundInteract = true;

                    colliders[i].GetComponent<Interact>().PlayEvent();
                }
            }
        }



        InteractableIsClose();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(input * speed * Time.deltaTime);
    }

    void RotateOrb()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = new Vector3(hit.point.x, projectileOrbBase.transform.position.y, hit.point.z) - projectileOrbBase.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(projectileOrbBase.transform.forward, targetDirection, orbRotateSpeed * Time.deltaTime, 0.0f);
            projectileOrbBase.transform.rotation = Quaternion.LookRotation(newDirection);
            projectileOrb.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void FireProjectile()
    {
        Projectile curProjectile = Instantiate(projectile, projectileOrb.transform.position, projectileOrb.transform.rotation).GetComponent<Projectile>();
        curProjectile.damage = attackPower;
        curProjectile.speed = attackSpeed;
        curProjectile.time = attackTime;
    }

    void InteractableIsClose()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        bool foundInteract = false;
        for (int i = 0; i < colliders.Length && !foundInteract; i++)
        {
            if (colliders[i].transform.tag == "interactable")
            {
                foundInteract = true;
                tooltip.SetActive(true);
            }
            else if (!foundInteract)
            {
                tooltip.SetActive(false);
            }
        }
    }

    public void GetHit(float damage)
    {
        curHealth -= damage;

        if(curHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {

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
