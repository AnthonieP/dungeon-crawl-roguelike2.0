using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 rotateSpeed;
    public int price;
    [Header("StatUps")]
    public float speedUp;
    public float attackPowerUp;
    public float attackSpeedUp;
    public float rangeUp;
    public float maxHealthUp;
    public float curHealthUp;
    public int moneyUp;

    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    public void PickUp()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        if((player.money += moneyUp) >= 0)
        {
            player.speed += speedUp;
            player.attackPower += attackPowerUp;
            player.attackSpeed += attackSpeedUp;
            player.attackTime += rangeUp;
            player.maxHealth += maxHealthUp;
            player.curHealth += curHealthUp;
            player.money += moneyUp;

            Destroy(gameObject);
        }
    }
}
