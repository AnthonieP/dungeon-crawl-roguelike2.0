using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawn : MonoBehaviour
{
    public bool isShop;
    public GameObject[] itemsToSpawn;
    public GameObject spawnedItem;
    

    private void Start()
    {
        SpawnItem();
    }

    public void SpawnItem()
    {
        ItemBase itemSpawned = Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length - 1)], transform.position, Quaternion.identity).GetComponent<ItemBase>();
        spawnedItem = itemSpawned.gameObject;
        if (isShop)
        {
            itemSpawned.moneyUp -= itemSpawned.price;
            itemSpawned.priceText.gameObject.SetActive(true);
            itemSpawned.priceText.text = ((itemSpawned.moneyUp - itemSpawned.price) * -1).ToString();
        }
    }
}
