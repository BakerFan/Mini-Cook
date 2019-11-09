using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableBasket : MonoBehaviour
{
    private GameObject bag;
    private GameObject[] bag_items=new GameObject [9];
    private int count=0;

    // Start is called before the first frame 
    void Start()
    {
        bag= GameObject.FindGameObjectWithTag("bag");
        bag.SetActive(false);
        for(; count<bag.transform.childCount-1;count++)
        {
            bag_items[count] = bag.transform.GetChild(count+1).gameObject;
            //Debug.Log(bag_items[count]);
        }
    }

    void OnTouched()
    {
        bag.SetActive(true);
    }
}
