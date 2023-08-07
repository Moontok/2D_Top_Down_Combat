using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] dropItems = null;
    [SerializeField] private int noDropChance = 1;

    public void DropItems()
    {
        int randomNum = Random.Range(0, dropItems.Length + noDropChance);

        if (randomNum < dropItems.Length)
        {
            Instantiate(dropItems[randomNum], transform.position, Quaternion.identity);
        }
    }
}
