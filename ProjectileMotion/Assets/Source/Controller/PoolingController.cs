using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject sampleItem;
    public int MaxInstantiateCount;
    public int CreatedCount;
    private int counter;
    public GameObject ItemPrefab;
    public void Initialize()
    {
        MaxInstantiateCount = 16;
        counter = 0;
        CreatedCount = 0;
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
    }

    public GameObject GetItem()
    {
        if (counter != MaxInstantiateCount)
        {
            counter++;
            return items[counter - 1];
        }
        else if (CreatedCount <= 7)
        {
            CreatedCount++;
            // means create extra 8 balls
            return Instantiate(ItemPrefab);
        }
        else return null; // create a warning with animation that says out of balls
    }
}
