using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private GameObject cut_item_button;
    private GameObject put_button;
    private GameObject cookspace1_button;
    private GameObject cookspace2_button;
    private GameObject cook1_button;
    private GameObject cook2_button;
    private GameObject additem1_button;
    private GameObject additem2_button;
    private GameObject plate1_button;
    private GameObject plate2_button;
    private GameObject plate1_cook_button;
    private GameObject plate2_cook_button;
    private GameObject water_button;
    private GameObject cut_button;
    private GameObject cutboard_button;
    private GameObject player;
    private GameObject cookdesk;
    private GameObject hand;
    private GameObject gasrange;
    private GameObject water_heater;
    private GameObject cutboard;
    private float player_cookdesk_distance = 0.0f;
    private float player_gasrange_distance = 0.0f;
    private ToolData toolData;
    private DBManager DBManager;
    bool ishandfull;
    private void Start()
    {
        cut_item_button = GameObject.Find("Cut_item_button");
        cutboard_button = GameObject.Find("Cutboard_button");
        cut_button = GameObject.Find("Cut_button");
        water_button = GameObject.Find("Water_button");
        put_button = GameObject.Find("Put_button");
        cookspace1_button = GameObject.Find("Cookspace1_button");
        cookspace2_button = GameObject.Find("Cookspace2_button");
        cook1_button = GameObject.Find("Cook1_button");
        cook2_button = GameObject.Find("Cook2_button");
        additem1_button = GameObject.Find("Additem1_button");
        additem2_button = GameObject.Find("Additem2_button");
        plate1_button = GameObject.Find("Plate1_button");
        plate2_button = GameObject.Find("Plate2_button");
        plate1_cook_button = GameObject.Find("Plate1_cook_button");
        plate2_cook_button = GameObject.Find("Plate2_cook_button");
        player = GameObject.Find("Player");
        cookdesk = GameObject.Find("CookDesk");
        hand = GameObject.FindGameObjectWithTag("hand");
        gasrange = GameObject.Find("KR_Gasrange_01_01");
        water_heater = GameObject.Find("Water_heater");
        cutboard = GameObject.Find("Cutting_board");
        toolData = GetComponent<ToolData>();
        DBManager = GameObject.Find("DataManager").GetComponent<DBManager>();
    }
    // Update is called once per frame
    void Update()
    {
        ishandfull = player.GetComponent<PlayerManager>().isHandFull;
        player_cookdesk_distance = Vector3.Distance(player.transform.position, cookdesk.transform.position);
        player_gasrange_distance = Vector3.Distance(player.transform.position, gasrange.transform.position);
        if (ishandfull && player_cookdesk_distance < 3.7)
        {
            if (hand.transform.GetChild(0).tag == "tool"&& hand.transform.GetChild(0).GetComponent<ToolManager>().Toolstate==ToolManager.StateType.empty)
            {
                put_button.SetActive(true);
            }
        }
        else
            put_button.SetActive(false);
        if (ishandfull && player_gasrange_distance < 3.7)
        {
            cook1_button.SetActive(false);
            cook2_button.SetActive(false);
            if (hand.transform.GetChild(0).tag == "tool")
            {
               if(GameObject.Find("Cooking_space_1").transform.childCount <= 1)
                    cookspace1_button.SetActive(true);
               else
                    cookspace1_button.SetActive(false);
               if (GameObject.Find("Cooking_space_2").transform.childCount <= 1)
                    cookspace2_button.SetActive(true);
               else
                    cookspace2_button.SetActive(false);
            }
            if(hand.transform.GetChild(0).tag == "meat"|| hand.transform.GetChild(0).tag == "vegetable")
            {
                if (GameObject.Find("Cooking_space_1").transform.childCount > 1)
                    additem1_button.SetActive(true);
                else
                    additem1_button.SetActive(false);
                if (GameObject.Find("Cooking_space_2").transform.childCount > 1)
                    additem2_button.SetActive(true);
                else
                    additem2_button.SetActive(false);
            }
        }
        else
        {
            cookspace1_button.SetActive(false);
            cookspace2_button.SetActive(false);
            additem1_button.SetActive(false);
            additem2_button.SetActive(false);
            if(GameObject.Find("Cooking_space_1").transform.childCount >1 && player_gasrange_distance < 3.7)
            {
                if (GameObject.Find("Cooking_space_1").transform.GetChild(1).GetComponent<ToolManager>().Toolstate == ToolManager.StateType.filled)
                    cook1_button.SetActive(true);
                else
                    cook1_button.SetActive(false);
                
            }
            else
                cook1_button.SetActive(false);
            if (GameObject.Find("Cooking_space_2").transform.childCount > 1 && player_gasrange_distance < 3.7)
            {
                if (GameObject.Find("Cooking_space_2").transform.GetChild(1).GetComponent<ToolManager>().Toolstate == ToolManager.StateType.filled)
                    cook2_button.SetActive(true);
                else
                    cook2_button.SetActive(false);

            }
            else
                cook2_button.SetActive(false);
        }
        if (ishandfull&&Vector3.Distance(water_heater.transform.position,player.transform.position)<3.7)
        {
            if (hand.transform.GetChild(0).tag == "tool")
            {
                ToolManager toolManager = hand.transform.GetChild(0).GetComponent<ToolManager>();
                if (toolManager.Toolkind == ToolManager.ToolType.boil_pot &&!toolManager.isWater)
                {
                    water_button.SetActive(true);
                }
                else
                    water_button.SetActive(false);
            }
        }
        else
            water_button.SetActive(false);
        if (ishandfull && Vector3.Distance(cutboard.transform.position, player.transform.position) < 3.7)
        {
            if (hand.transform.GetChild(0).tag == "meat" || hand.transform.GetChild(0).tag == "vegetable")
                cutboard_button.SetActive(true);
            else
                cutboard_button.SetActive(false);
        }
        else
            cutboard_button.SetActive(false);
        if(!ishandfull && Vector3.Distance(cutboard.transform.position, player.transform.position) < 3.7)
        {
            plate1_cook_button.SetActive(false);
            plate2_cook_button.SetActive(false);
            if (GameObject.Find("Plate_1").transform.GetChild(1).childCount > 0)
                plate1_button.SetActive(true);
            else
                plate1_button.SetActive(false);
            if (GameObject.Find("Plate_2").transform.GetChild(1).childCount > 0)
                plate2_button.SetActive(true);
            else
                plate2_button.SetActive(false);

        }
        else
        {
            plate1_button.SetActive(false);
            plate2_button.SetActive(false);
            if(ishandfull && Vector3.Distance(cutboard.transform.position, player.transform.position) < 3.7)
            {
                if(hand.transform.GetChild(0).tag=="vegetable")
                {
                    if (GameObject.Find("Plate_1").transform.GetChild(1).childCount > 0)
                        plate1_cook_button.SetActive(true);
                    else
                        plate1_cook_button.SetActive(false);
                    if (GameObject.Find("Plate_2").transform.GetChild(1).childCount > 0)
                        plate2_cook_button.SetActive(true);
                    else
                        plate2_cook_button.SetActive(false);
                }
                else
                {
                    plate1_cook_button.SetActive(false);
                    plate2_cook_button.SetActive(false);
                }
                
            }
            else
            {
                plate1_cook_button.SetActive(false);
                plate2_cook_button.SetActive(false);
            }
        }

        if (!ishandfull && Vector3.Distance(cutboard.transform.position, player.transform.position) < 3.7)
        {
            if (cutboard.transform.GetChild(0).transform.childCount > 0)
            {
                cut_item_button.SetActive(true);
                cut_button.SetActive(true);
            }
            else
            {
                cut_item_button.SetActive(false);
                cut_button.SetActive(false);
            }
        }
        else
        {
            cut_item_button.SetActive(false);
            cut_button.SetActive(false);
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
            if(tool.GetComponent<ToolManager>().Toolstate == ToolManager.StateType.empty)
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
        if (hand.transform.GetChild(0).tag == "tool")
        {
            GameObject tool = hand.transform.GetChild(0).gameObject;
            tool.transform.SetPositionAndRotation(cooking_space.transform.position, cooking_space.transform.rotation);
            tool.transform.SetParent(cooking_space.transform);
        }
    }
    public void PourWater()
    {
        ToolManager toolManager = hand.transform.GetChild(0).GetComponent<ToolManager>();
        toolManager.isWater = true;
    }
    public void AddItem(GameObject cooking_space)
    {
        GameObject tool_itemSpace = cooking_space.transform.GetChild(1).gameObject.GetComponent<ToolManager>().itemplace;
        GameObject Item = hand.transform.GetChild(0).gameObject;
        Item.transform.SetParent(tool_itemSpace.transform);
        Item.transform.SetPositionAndRotation(tool_itemSpace.transform.position,tool_itemSpace.transform.rotation);
        Item.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    public void Cutboard()
    {
        GameObject item = hand.transform.GetChild(0).gameObject;
        item.transform.SetParent(cutboard.transform.GetChild(0));
        item.transform.SetPositionAndRotation(cutboard.transform.GetChild(0).transform.position, cutboard.transform.GetChild(0).transform.rotation);
    }

    public void Cut_item()
    {
        GameObject item = cutboard.transform.GetChild(0).GetChild(0).gameObject;
        item.transform.SetParent(hand.transform);
        item.transform.SetPositionAndRotation(hand.transform.position, hand.transform.rotation);
    }

    public void Cut()
    {
        GameObject ingredient = cutboard.transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject meal = null;
        var item = DBManager.SelectData("cut", new string[] { "meal" }, new string[] { "ingredients", ingredient.name });
        Destroy(ingredient);
        if (item[0]!="")
        {
            meal = (GameObject)Instantiate(Resources.Load("prefabs/Dishes/"+item[0]), cutboard.transform.GetChild(0).transform);
            meal.name = item[0];            
        }
        item.Clear();
    }

    public void Pickup(GameObject item)
    {
        item.transform.GetChild(0).SetParent(hand.transform);
        // item.transform.GetChild(0).SetPositionAndRotation(hand.transform.position, hand.transform.rotation);
    }

    public void PlateCook1()
    {
        GameObject meal_place = GameObject.Find("Plate_1").transform.GetChild(1).gameObject;
        GameObject handheld = hand.transform.GetChild(0).gameObject;
        string ingredients = null;
        if (string.Compare(meal_place.transform.GetChild(0).name, handheld.name) < 0)
        {
            ingredients = meal_place.transform.GetChild(0).name +","+ handheld.name;
        }
        else
            ingredients = handheld.name + "," + meal_place.transform.GetChild(0).name;
        Destroy(meal_place.transform.GetChild(0).gameObject);
        Destroy(handheld);
        var item = DBManager.SelectData("plate", new string[] { "meal" }, new string[] { "ingredients", ingredients })[0];
        if(item!="")
        {
            Instantiate(Resources.Load("prefabs/Dishes/" + item), meal_place.transform);
        }
    }

    public void PlateCook2()
    {
        GameObject meal_place = GameObject.Find("Plate_2").transform.GetChild(1).gameObject;
        GameObject handheld = hand.transform.GetChild(0).gameObject;
        string ingredients = null;
        if (string.Compare(meal_place.transform.GetChild(0).name, handheld.name) < 0)
        {
            ingredients = meal_place.transform.GetChild(0).name + "," + handheld.name;
        }
        else
            ingredients = handheld.name + "," + meal_place.transform.GetChild(0).name;
        Destroy(meal_place.transform.GetChild(0).gameObject);
        Destroy(handheld);
        var item = DBManager.SelectData("plate", new string[] { "meal" }, new string[] { "ingredients", ingredients })[0];
        if (item != "")
        {
            Instantiate(Resources.Load("prefabs/Dishes/" + item), meal_place.transform);
        }
    }

    public void Cook1()
    {
        GameObject cooking_space = GameObject.Find("Cooking_space_1");
        GameObject meal_place = GameObject.Find("Plate_1").transform.GetChild(1).gameObject;
        string ingredients = cooking_space.transform.GetChild(1).GetComponent<ToolManager>().Cook();
        string tablename = "";
        switch (cooking_space.transform.GetChild(1).GetComponent<ToolManager>().Toolkind)
        {
            case ToolManager.ToolType.boil_pot:
                tablename = "boil";
                break;
            case ToolManager.ToolType.fry_pan:
                tablename = "fry";
                break;
            default:
                break;
        }
        Debug.Log(ingredients);
        var item = DBManager.SelectData(tablename, new string[] { "meal" }, new string[] { "ingredients", ingredients })[0];
        StartCoroutine(Cooking(5f, cooking_space.transform.GetChild(1).GetComponent<ToolManager>(),item,meal_place));
        //Debug.Log(item);
    }

    public void Cook2()
    {
        GameObject cooking_space = GameObject.Find("Cooking_space_2");
        GameObject meal_place = GameObject.Find("Plate_2").transform.GetChild(1).gameObject;
        string ingredients = cooking_space.transform.GetChild(1).GetComponent<ToolManager>().Cook();
        string tablename = "";
        switch (cooking_space.transform.GetChild(1).GetComponent<ToolManager>().Toolkind)
        {
            case ToolManager.ToolType.boil_pot:
                tablename = "boil";
                break;
            case ToolManager.ToolType.fry_pan:
                tablename = "fry";
                break;
            default:
                break;
        }
        var item = DBManager.SelectData(tablename, new string[] { "meal" }, new string[] { "ingredients", ingredients })[0];
        StartCoroutine(Cooking(5f, cooking_space.transform.GetChild(1).GetComponent<ToolManager>(), item, meal_place));
        Debug.Log(item);
    }
    //int count = ingredients.Length;
    //List<string> list_b = new List<string>();
    //list_b.Add("Rackoflamb_raw");
    //    list_b.Add("Chilipepper");
    //    list_b.Add("Leak");

    //    Debug.Log(item);
    //     StartCoroutine(Cooking(cooking_time));

    //cooking_space.transform.GetChild(1).GetComponent<ToolManager>().StopCooking();
    //Debug.Log(count);


    IEnumerator Cooking(float seconds, ToolManager toolManager, string meal, GameObject mealplace)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("做好了！");
        toolManager.StopCooking();
        GameObject NewMeal = null;
        if (meal != "")
        {
            NewMeal = (GameObject)Instantiate(Resources.Load("prefabs/Dishes/" + meal), mealplace.transform);
            NewMeal.name = meal;
        }
    }



public static bool CompareLists<T>(List<T> aListA, List<T> aListB)
    {
        if (aListA == null || aListB == null || aListA.Count != aListB.Count)
            return false;
        if (aListA.Count == 0)
            return true;
        Dictionary<T, int> lookUp = new Dictionary<T, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count))
            {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count))
            {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }
}
