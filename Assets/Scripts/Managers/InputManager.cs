using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CameraController cameraController;
    public SaveManager saveManager;
    public PlayerManager playerManager;
    public SaveGameMenuController saveGameMenuController;

    private bool isGameMenuOpened;
    private bool isSaveMenuOpened;
    private bool isLoadMenuOpened;
    private float timeScaleNow=1;
    private float timeScaleNow2=1;

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
                playerManager.LeftClickDown();

            }
            if (Input.GetMouseButtonUp(0))
            {
                playerManager.LeftClickUp();
                
            }
            if(Input.GetMouseButtonDown(1))
            {
                playerManager.RightClickDown();
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
                    
                    saveManager.QuickSave();
                }
                if (Input.GetKeyDown(KeyCode.F6))
                {                    
                    saveManager.QuickLoad();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGameMenuOpened)
            {
                gameMenuOpenerInput(true);
            }else if(isGameMenuOpened && isSaveMenuOpened)
            {
                SaveMenuOpenerInput(false);
            }else if(isGameMenuOpened && isLoadMenuOpened)
            {
                LoadMenuOpenerInput(false);
            }else if(isGameMenuOpened && !isSaveMenuOpened)
            {
                gameMenuOpenerInput(false);
            }
        }
    }

    public void gameMenuOpenerInput(bool isOpened)
    {
        isGameMenuOpened=isOpened;
        playerManager.gameMenuOpener(isOpened);
        cameraController.gameMenuIsOpened(isOpened);
        if(isOpened){ 
            timeScaleNow2=Time.timeScale;           
            Time.timeScale = 0;
            
        }else
        {
            Time.timeScale=timeScaleNow2;            
        }
    }

    public void SaveMenuOpenerInput(bool isOpened)
    {
        isSaveMenuOpened=isOpened;
        saveManager.LoadGameData();
        saveGameMenuController.saveMenuOpener(isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(!isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(1).gameObject.SetActive(isOpened);
    }
    public void LoadMenuOpenerInput(bool isOpened)
    {
        isLoadMenuOpened=isOpened;
        saveManager.LoadGameData();
        saveGameMenuController.loadMenuOpener(isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(!isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(2).gameObject.SetActive(isOpened);
    }
}
