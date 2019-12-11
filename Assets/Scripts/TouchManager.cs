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
    private ToolData ToolData;

    private void Start()
    {
        cam = Camera.main;
        vegetable_basket = GameObject.FindGameObjectWithTag("vege_bag");
        hand = GameObject.FindGameObjectWithTag("hand");
        player = GameObject.FindGameObjectWithTag("Player");
        ToolData = GetComponent<ToolData>();
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
                    next_refrigerator = Resources.Load("prefabs/Tools/Refrigerator_open");
                    refrigerator=(GameObject)Instantiate(next_refrigerator, pos, rotation);
                    refrigerator.tag = "Refrigerator";
                }
                if (isHit && hit.collider.gameObject.name == "refrigerator_door_a_open" && hit.distance < 2.5)
                {
                    refrigerator = GameObject.FindGameObjectWithTag("Refrigerator");
                    pos = refrigerator.transform.position;
                    rotation = refrigerator.transform.rotation;
                    Destroy(refrigerator);
                    next_refrigerator = Resources.Load("prefabs/Tools/Refrigerator");
                    refrigerator = (GameObject)Instantiate(next_refrigerator, pos, rotation);
                    refrigerator.tag = "Refrigerator";
                }
                if (isHit && hit.collider.gameObject.name == "catfish_frozen" && !ishandfull && hit.distance < 2)
                {
                    item = (GameObject)Instantiate(Resources.Load("prefabs/Meats/Catfish_raw"), hand.transform);
                    item.tag = "meat";
                    item.name = "Catfish_raw";
                    item.transform.parent=hand.transform;
                }
                if (isHit && hit.collider.gameObject.name == "rackoflamb_frozen" && !ishandfull && hit.distance < 2)
                {
                    item = (GameObject)Instantiate(Resources.Load("prefabs/Meats/Rackoflamb_raw"), hand.transform);
                    item.tag = "meat";
                    item.name = "Rackoflamb_raw";
                    item.transform.parent=hand.transform;
                    //Debug.Log(item.transform.parent);
                }
                if (isHit && hit.collider.gameObject.transform.parent!=null && !ishandfull && hit.distance < 3.5)
                {
                    if(hit.collider.gameObject.transform.parent.tag == "tool")
                    {
                        //Debug.Log("yes");
                        GameObject tool = hit.collider.gameObject.transform.parent.gameObject;
                        if (tool.GetComponent<ToolManager>().Toolstate == ToolManager.StateType.idle)
                        {
                            ToolManager.ToolType tooltype = tool.GetComponent<ToolManager>().Toolkind;
                            Destroy(tool);
                            tool = (GameObject)Instantiate(ToolData.SearchCookingToolByToolType(tooltype), hand.transform);
                            tool.transform.SetParent(hand.transform);
                        }
                        if(tool.GetComponent<ToolManager>().Toolstate ==ToolManager.StateType.empty)
                        {
                            tool.transform.SetPositionAndRotation(hand.transform.position, hand.transform.rotation);
                            tool.transform.SetParent(hand.transform);
                        }
                    }
                }
            }
        }
    }  
}
