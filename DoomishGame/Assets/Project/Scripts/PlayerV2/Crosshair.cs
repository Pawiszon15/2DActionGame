using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Image offensiveImage;
    [SerializeField] Image utilityImage;
    [SerializeField] ToolCooldown[] toolCooldown;
    private ItemSwaper itemSwaper;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }

    public void ShowAvaiability(int currentTool)
    {
        if (toolCooldown[currentTool].leftMouseUse > 0 && toolCooldown[currentTool].enabled)
        {
            offensiveImage.color = Color.red;
        }

        if (toolCooldown[currentTool].rightMouseUse > 0 && toolCooldown[currentTool].enabled)
        {
            utilityImage.color = Color.green;
        }


        if (toolCooldown[currentTool].leftMouseUse <= 0)
        {
            offensiveImage.color = Color.white;
        }


        if (toolCooldown[currentTool].rightMouseUse <= 0)
        {
            utilityImage.color = Color.white;
        }
    }
}
