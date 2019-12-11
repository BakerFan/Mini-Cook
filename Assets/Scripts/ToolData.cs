using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolData : MonoBehaviour
{
    public Dictionary<ToolManager.ToolType, Object> Tools_cooking = new Dictionary<ToolManager.ToolType, Object>();
    public Dictionary<ToolManager.ToolType, Object> Tools_idle = new Dictionary<ToolManager.ToolType, Object>();

    private void Start()
    {
        Tools_idle.Add(ToolManager.ToolType.fry_pan, Resources.Load("prefabs/Tools/Fry_pan_idle"));
        Tools_idle.Add(ToolManager.ToolType.boil_pot, Resources.Load("prefabs/Tools/Boil_pot_idle"));
        Tools_cooking.Add(ToolManager.ToolType.fry_pan, Resources.Load("prefabs/Tools/Fry_pan_cooking"));
        Tools_cooking.Add(ToolManager.ToolType.boil_pot, Resources.Load("prefabs/Tools/Boil_pot_cooking"));
    }

    public Object SearchIdleToolByToolType(ToolManager.ToolType type)
    {
        return Tools_idle[type];
    }
    public Object SearchCookingToolByToolType(ToolManager.ToolType type)
    {
        return Tools_cooking[type];
    }
}
