using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera cam;
    private float timeHit = 0f;         //用于点击的时间间隔,每次点击时间间隔应大于0.2秒  
    private GameObject vegetable_basket;
    private GameObject refrigerator;
    private Object next_refrigerator;
    private GameObject hand;
    private GameObject player;
    private GameObject item;
    private Vector3 pos;
    private Quaternion rotation;

    private void Start()
    {
        cam = Camera.main;
        vegetable_basket = GameObject.FindGameObjectWithTag("vege_bag");
        hand = GameObject.FindGameObjectWithTag("hand");
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(vegetable_basket);
    }

    void Update()
    {
        bool ishandfull = player.GetComponent<PlayerManager>().isHandFull;
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
                if (isHit && hit.collider.gameObject.name == "shw_garbage_can01" && ishandfull&&hit.distance<3.5)
                {
                    Destroy(hand.transform.GetChild(0).transform.gameObject);
                }
                if (isHit && hit.collider.gameObject.name == "refrigerator_door_a" && hit.distance < 2.5)
                {
                    refrigerator = GameObject.FindGameObjectWithTag("Refrigerator");
                    pos = refrigerator.transform.position;
                    rotation = refrigerator.transform.rotation;
                    Destroy(refrigerator);
                    next_refrigerator = Resources.Load("prefabs/Refrigerator_open");
                    refrigerator=(GameObject)Instantiate(next_refrigerator, pos, rotation);
                    refrigerator.tag = "Refrigerator";
                }
                if (isHit && hit.collider.gameObject.name == "refrigerator_door_a_open" && hit.distance < 2.5)
                {
                    refrigerator = GameObject.FindGameObjectWithTag("Refrigerator");
                    pos = refrigerator.transform.position;
                    rotation = refrigerator.transform.rotation;
                    Destroy(refrigerator);
                    next_refrigerator = Resources.Load("prefabs/Refrigerator");
                    refrigerator = (GameObject)Instantiate(next_refrigerator, pos, rotation);
                    refrigerator.tag = "Refrigerator";
                }
                if (isHit && hit.collider.gameObject.name == "catfish_frozen" && !ishandfull && hit.distance < 2)
                {
                    item = (GameObject)Instantiate(Resources.Load("prefabs/Catfish_raw"), hand.transform);
                    item.tag = "meat";
                    item.name = "Catfish_raw";
                    item.transform.parent=hand.transform;
                }
                if (isHit && hit.collider.gameObject.name == "rackoflamb_frozen" && !ishandfull && hit.distance < 2)
                {
                    item = (GameObject)Instantiate(Resources.Load("prefabs/Rackoflamb_raw"), hand.transform);
                    item.tag = "meat";
                    item.name = "Rackoflamb_raw";
                    item.transform.parent=hand.transform;
                    //Debug.Log(item.transform.parent);
                }

                //碰到tag为工具的物体时
                if (isHit && (hit.collider.gameObject.tag == "tool") && !ishandfull && hit.distance < 3.5)
                {
                    GameObject pan = hit.collider.gameObject;
                    pan.transform.parent = hand.transform;
                }
                //放锅
                if (isHit && hit.collider.gameObject.name == "CookDesk" && hit.distance < 3)
                {
                    //Debug.Log("entered0");
                    if (ishandfull && hand.transform.GetChild(0).tag == "tool")
                    {
                        //Debug.Log("entered");
                        GameObject cookdesk = GameObject.Find("CookDesk");
                        GameObject tool = hand.transform.GetChild(0).gameObject;
                        //GameObject pan = GameObject.Find("Player/Hand/Fried_pan");
                        foreach (Transform child in cookdesk.transform)
                        {
                            if (child.transform.childCount == 0)
                            {
                                //Debug.Log("entered1");
                                tool.transform.parent = child.transform;
                                tool.transform.position = child.transform.position;
                            }
                        }
                    }
                }
            }
        }
    }  
}
