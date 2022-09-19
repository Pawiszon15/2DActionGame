using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponDisplayer : MonoBehaviour
{
    [SerializeField] Image[] tools;
    private Transform[] toolsImagePos;

    [SerializeField] Image marker;



    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform imagePos in toolsImagePos)
        {
            int x = 0;
            toolsImagePos[x].transform.position = tools[x].transform.position;
            x++;
        }
    }

    public void moveMarker(int currentTool)
    {
        marker.transform.position = tools[currentTool].transform.position;
    }

    public void makeToolAvaiable(int toolAgainAvaiable)
    {
        tools[toolAgainAvaiable].color = Color.white;
    }

    public void makeToolUnavaiable(int usedTool)
    {
        tools[usedTool].color = Color.red;
    }

}
