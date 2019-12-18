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
    public GameObject boil_water;
    public GameObject steam;
    // Start is called before the first frame update
    void Start()
    {
    }

    public string Cook()
    {
        string ingredients = "";
        List<string> list = new List<string>();
        if(Toolstate==StateType.filled)
        {
            int i = 0;
            for (i = 0; i < transform.GetChild(1).childCount; i++)
                list.Add(transform.GetChild(1).GetChild(i).name);
            list.Sort();
            
            for(i=0;i<list.Count-1;i++)
            {
                ingredients += list[i];
                ingredients += ",";
            }
            ingredients += list[i];
            Toolstate = StateType.cooking;
        }
        return ingredients;
    }

    public void StopCooking()
    {
        if(Toolstate==StateType.cooking)
        {
            for(int i=0;i<transform.GetChild(1).childCount;i++)
            {
                Destroy(transform.GetChild(1).GetChild(i).gameObject);
            }
        }
        isWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.name.IndexOf("idle") != -1)
            Toolstate = StateType.idle;
        else if (transform.name.IndexOf("cooking") != -1)
        {
            if (itemplace.transform.childCount == 0&&!isWater)
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

        if(Toolstate== StateType.cooking)
        {
            if (boil_water != null)
                boil_water.SetActive(true);
            if (steam != null)
                steam.SetActive(true);
        }
        else
        {
            if (boil_water != null)
                boil_water.SetActive(false);
            if (steam != null)
                steam.SetActive(false);
        }
    }
}
