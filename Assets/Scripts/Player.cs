using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour
{
    private GameObject hand;
    public bool isHandFull;

    private void Start()
    {
        hand = this.transform.GetChild(1).gameObject;
        //Instantiate(Resources.Load("Corn"), hand.transform);
    }

    private void Update()
    {
        if (hand.transform.childCount == 0)
            isHandFull = false;
        else
        { 
            isHandFull = true;
            //Debug.Log(isHandFull);
            hand.transform.GetChild(0).position = hand.transform.position;
        }
    }
}
