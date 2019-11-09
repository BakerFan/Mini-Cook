﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera cam;
    private float timeHit = 0f;         //用于点击的时间间隔,每次点击时间间隔应大于0.2秒  
    private GameObject vegetable_basket;
    private GameObject hand;

    private void Start()
    {
        cam = Camera.main;
        vegetable_basket = GameObject.FindGameObjectWithTag("vege_bag");
        hand = GameObject.FindGameObjectWithTag("hand");
        //Debug.Log(vegetable_basket);
    }

    void Update()
    {
        timeHit += Time.deltaTime;
        if (timeHit > 0.2f)
        {
            //Input.simulateMouseWithTouches = true;
            if (Input.GetMouseButton(0))
            {
                timeHit = 0f;
                RaycastHit hit;
                bool isHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f);
                //Debug.Log(isHit);
                //Debug.Log(hit.collider.gameObject.name);
                if (isHit&&hit.collider.gameObject==vegetable_basket)
                {
                    //Debug.Log("in!");
                    vegetable_basket.SendMessage("OnTouched", SendMessageOptions.DontRequireReceiver);
                }
                if (isHit && hit.collider.gameObject.name == "shw_garbage_can01" && hand.transform.childCount != 0&&hit.distance<3.5)
                {
                    Destroy(hand.transform.GetChild(0).transform.gameObject);
                }

            }
        }
    }  
}
