using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject put_button;
    private GameObject cookspace1_button;
    private GameObject cookspace2_button;
    private GameObject player;
    private GameObject cookdesk;
    private GameObject hand;
    private GameObject gasrange;
    private float player_cookdesk_distance = 0.0f;
    private float player_gasrange_distance = 0.0f;
    private ToolData toolData;
    bool ishandfull;
    private void Start()
    {
        put_button = GameObject.Find("Put_button");
        cookspace1_button = GameObject.Find("Cookspace1_button");
        cookspace2_button = GameObject.Find("Cookspace2_button");
        player = GameObject.Find("Player");
        cookdesk = GameObject.Find("CookDesk");
        hand = GameObject.FindGameObjectWithTag("hand");
        gasrange = GameObject.Find("KR_Gasrange_01_01");
        toolData = GetComponent<ToolData>();
    }
    // Update is called once per frame
    void Update()
    {
        ishandfull = player.GetComponent<PlayerManager>().isHandFull;
        player_cookdesk_distance = Vector3.Distance(player.transform.position, cookdesk.transform.position);
        player_gasrange_distance = Vector3.Distance(player.transform.position, gasrange.transform.position);
        if (ishandfull && player_cookdesk_distance < 3.7)
        {
            if (hand.transform.GetChild(0).tag == "tool")
            {
                put_button.SetActive(true);
            }
        }
        else
            put_button.SetActive(false);
        if (ishandfull && player_gasrange_distance < 3.7)
        {
            if (hand.transform.GetChild(0).tag == "tool")
            {
                cookspace1_button.SetActive(true);
                cookspace2_button.SetActive(true);
            }
        }
        else
        {
            cookspace1_button.SetActive(false);
            cookspace2_button.SetActive(false);
        }
    }

    //put cook tool into cookdesk
    public void PutToolIntoCookdesk()
    {
        if(hand.transform.GetChild(0).tag == "tool")
        {
            //Debug.Log("entered");
            GameObject cookdesk = GameObject.Find("CookDesk");
            GameObject tool = hand.transform.GetChild(0).gameObject;
            ToolManager.ToolType tooltype = tool.GetComponent<ToolManager>().Toolkind;
            if(tool.GetComponent<ToolManager>().Toolstate != ToolManager.StateType.cooking)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (cookdesk.transform.GetChild(i).childCount == 0)
                    {
                        Destroy(tool);
                        tool = (GameObject)Instantiate(toolData.SearchIdleToolByToolType(tooltype), cookdesk.transform.GetChild(i).transform);
                        tool.transform.SetParent(cookdesk.transform.GetChild(i));
                        break;
                    }
                }
            }
        }     
    }

    //put cook tool on the stove
    public void PutToolOnStove(GameObject cooking_space)
    {
        //Debug.Log(cooking_space.transform.childCount);
        if (hand.transform.GetChild(0).tag == "tool"&& cooking_space.transform.childCount<=1)
        {
            GameObject tool = hand.transform.GetChild(0).gameObject;
            tool.transform.SetPositionAndRotation(cooking_space.transform.position, cooking_space.transform.rotation);
            tool.transform.SetParent(cooking_space.transform);
        }
    }
}
