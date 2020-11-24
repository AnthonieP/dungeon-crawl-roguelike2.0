using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    [Header("Prices")]
    public int resetPrice;
    public int minResetPriceUp;
    public int maxResetPriceUp;
    [Header("Shop")]
    public ItemSpawn[] shopItems;
    [Header("Canvas")]
    public Text priceText;

    private void Start()
    {
        priceText.text = resetPrice.ToString();

    }

    public void ResetShop()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        if(player.money - resetPrice >= 0){
            player.money -= resetPrice;
            resetPrice += Random.Range(minResetPriceUp, maxResetPriceUp);
            for (int i = 0; i < shopItems.Length; i++)
            {
                Destroy(shopItems[i].spawnedItem);
                shopItems[i].SpawnItem();
            }
            priceText.text = resetPrice.ToString();


        }
    }
}
