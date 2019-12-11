using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager:MonoBehaviour
{
    private GameObject hand;
    private GameObject item;
    private Object[,] items = new Object[3, 3];
    public string[] items_name = new string[9];

    private void Start()
    {
        hand = GameObject.FindGameObjectWithTag("hand");
        int index = 0;
        for(int i=0;i<3;i++)
        {
            for (int j = 0; j < 3; j++)
            {
                items[i,j] = Resources.Load("prefabs/Vegetables/"+items_name[index]);
                index++;
            }
        }
    }
    public void pickUpItem00()
    {
        Debug.Log("in!!!");
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[0, 0], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[0];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }   
    }
    public void pickUpItem01()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[0, 1], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[1];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem02()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[0, 2], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[2];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem10()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[1, 0], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[3];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem11()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[1, 1], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[4];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem12()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[1, 2], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[5];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem20()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[2, 0], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[6];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem21()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[2, 1], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[7];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
    public void pickUpItem22()
    {
        if (hand.transform.childCount == 0)
        {
            item = (GameObject)Instantiate(items[2,2], hand.transform);
            item.tag = "vegetable";
            item.name = items_name[8];
            item.transform.SetParent(hand.transform);
            GameObject.FindGameObjectWithTag("bag").SetActive(false);
        }
    }
}
