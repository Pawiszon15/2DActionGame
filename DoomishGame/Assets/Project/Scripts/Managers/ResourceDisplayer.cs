using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDisplayer : MonoBehaviour
{

    [SerializeField] string resourceName;
    [SerializeField] TextMeshProUGUI textMesh;

    private void Start()
    {
/*        textMesh = GetComponent<TextMeshProUGUI>();
*/    }

    public void ChangeResourceAmount(int resource)
    {
        textMesh.text = resourceName + " - " + resource.ToString();
    }

    public void ChangeResourceName(string name, Color color)
    {
        textMesh.text = name;
        textMesh.color = color;
    }
}
