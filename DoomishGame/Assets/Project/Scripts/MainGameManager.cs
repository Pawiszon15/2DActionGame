using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;
    public Color TeamColor; // new variable declared

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (MainGameManager.Instance != null)
        {
            SetColor(MainGameManager.Instance.TeamColor);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetColor(TeamColor);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            NewColorSelected(Color.red);
        }  
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            NewColorSelected(Color.green);
        }
    }

    public void NewColorSelected(Color color)
    {
        MainGameManager.Instance.TeamColor = color;
    }

    private void SetColor(Color color)
    {
        Debug.Log(color);
    }
}
