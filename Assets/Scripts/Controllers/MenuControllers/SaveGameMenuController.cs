using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveGameMenuController : MonoBehaviour
{
    public RawImage saveGame;
    public Image savecontent;
    public Image loadcontent;
    private RawImage newSaveGame;
    private List<GameData> gameDataList;
    private Button savegameselectbutton;
    public GameObject inputText;
    public GameObject loadText;
    public SaveManager saveManager;
    public InputManager inputManager;

    public void saveMenuOpener(bool isOpened)
    {
        gameDataList = saveManager.gameDataList;
        if(isOpened)
        {            
            createSaveGamepre();
        }else{
            for (int i = 0; i < gameDataList.Count; i++)
            {                
                Destroy(GameObject.Find("saveGameContent").transform.GetChild(i).gameObject);                
            }
        }
    }

    public void loadMenuOpener(bool isOpened)
    {
        gameDataList = saveManager.gameDataList;
        if(isOpened)
        {   
            if(gameDataList.Count>0)
            {
                TMP_Text text = loadText.GetComponent<TMP_Text>();
                text.text = gameDataList[0].gameName;
            }
            createLoadGamepre();
        }else{
            for (int i = 0; i < gameDataList.Count; i++)
            {                
                Destroy(GameObject.Find("loadGameContent").transform.GetChild(i).gameObject);                
            }
        }
    }

    void TaskOnClick(int index){
        
        TMP_InputField text = inputText.GetComponent<TMP_InputField>();
        text.text = gameDataList[index].gameName;
        //string text = inputText.GetComponent<TMP_InputField>().text;
	}
    void TaskOnClick1(int index){
        TMP_Text text = loadText.GetComponent<TMP_Text>();
        text.text = gameDataList[index].gameName;
	}

    public void saveGameSaveButton()
    {
        string text = inputText.GetComponent<TMP_InputField>().text;
        if (text!="")
        {   
            for (int i = 0; i < gameDataList.Count; i++)
            {                
                Destroy(GameObject.Find("saveGameContent").transform.GetChild(i).gameObject);                
            }
            saveManager.Save(text);
            createSaveGamepre();
        }
    }


    private void createSaveGamepre()
    {        
        for (int i = 0; i < gameDataList.Count; i++)
            {
                newSaveGame = Instantiate(saveGame,savecontent.transform);
                TMP_Text text1 = newSaveGame.transform.GetChild(0).GetComponent<TMP_Text>();                
                text1.text = gameDataList[i].gameName;
                TMP_Text text2 = newSaveGame.transform.GetChild(1).GetComponent<TMP_Text>();                
                text2.text = gameDataList[i].gameTime;
                savegameselectbutton = newSaveGame.GetComponentInChildren<Button>();
                int temp = i;
                savegameselectbutton.onClick.AddListener(delegate{TaskOnClick(temp);});
            }
    }
    private void createLoadGamepre()
    {        
        for (int i = 0; i < gameDataList.Count; i++)
            {
                newSaveGame = Instantiate(saveGame,loadcontent.transform);
                TMP_Text text1 = newSaveGame.transform.GetChild(0).GetComponent<TMP_Text>();                
                text1.text = gameDataList[i].gameName;
                TMP_Text text2 = newSaveGame.transform.GetChild(1).GetComponent<TMP_Text>();                
                text2.text = gameDataList[i].gameTime;
                savegameselectbutton = newSaveGame.GetComponentInChildren<Button>();
                int temp = i;
                savegameselectbutton.onClick.AddListener(delegate{TaskOnClick1(temp);});
            }
    }

    public void loadGameBttn()
    {
        TMP_Text text = loadText.GetComponent<TMP_Text>();
        if(text.text!="")
        {              
            inputManager.LoadMenuOpenerInput(false);
            inputManager.gameMenuOpenerInput(false);
            saveManager.Load(text.text);
        }
    }

}
