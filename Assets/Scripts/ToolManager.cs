using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public enum StateType {idle,cooking,filled,empty};
    public bool isWater=false;
    public enum ToolType { fry_pan, boil_pot};
    public StateType Toolstate = 0;
    public ToolType Toolkind = 0;
    public GameObject itemplace;
    public GameObject water;
    public GameObject steam;
    // Start is called before the first frame update
    void Start()
    {
        itemplace = transform.GetChild(1).gameObject;
    }

    public void Cook()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.name.IndexOf("idle") != -1)
            Toolstate = StateType.idle;
        else if (transform.name.IndexOf("cooking") != -1)
        {
            if (itemplace.transform.childCount == 0)
                Toolstate = StateType.empty;
            else if (Toolstate != StateType.cooking)
                Toolstate = StateType.filled;
            else
                Toolstate = StateType.cooking;
        }

        if(water!=null)
        {
            if (isWater)
            {
                water.SetActive(true);
            }
            else
            {
                water.SetActive(false);
            }
        }
    }
}
