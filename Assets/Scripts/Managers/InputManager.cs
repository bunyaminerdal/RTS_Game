using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public static event Action LeftButtonClickDownAction;
    public static event Action LeftButtonClickUpAction;
    public static event Action RightButtonClickDownAction;
    public static event Action QuickSaveAction;
    public static event Action QuickLoadAction;
    public static event Action DeSelectUnitAction;

    private bool isGameMenuOpened;
    private float timeScaleNow=1;
    private float timeScaleNow1 = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame                                                                                                           
    void Update()
    {
        if(!isGameMenuOpened)
        {
            //Detect if mouse is down
            if(Input.GetMouseButtonDown(0))
            {
                LeftButtonClickDownAction?.Invoke();                

            }
            if (Input.GetMouseButtonUp(0))
            {

                LeftButtonClickUpAction?.Invoke();
            }
            if(Input.GetMouseButtonDown(1))
            {
                RightButtonClickDownAction?.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(Time.timeScale!=0)
                {          
                    timeScaleNow=Time.timeScale;          
                    Time.timeScale = 0;
                    
                }else
                {
                    Time.timeScale=timeScaleNow;                    
                }
            }
            if(!isGameMenuOpened)
            {
                if (Input.GetKeyDown(KeyCode.F5))
                {

                    QuickSaveAction?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    DeSelectUnitAction?.Invoke();
                    QuickLoadAction?.Invoke();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGameMenuOpened)
            {
                GameMenuOpenerInput(true);
            }
        }
    }
    public void IsGameMenuOpened(bool isOpened)
    {
        isGameMenuOpened = isOpened;
        if (isOpened)
        {
            timeScaleNow1 = Time.timeScale;
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = timeScaleNow1;
        }
    }
    void GameMenuOpenerInput(bool isOpened)
    {
        
        isGameMenuOpened =isOpened;
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(isOpened);
        if(isOpened){
            timeScaleNow1 = Time.timeScale;           
            Time.timeScale = 0;
            
        }else
        {
            Time.timeScale= timeScaleNow1;            
        }
    }

    

}
