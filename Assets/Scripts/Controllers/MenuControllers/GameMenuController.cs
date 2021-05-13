using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject saveGame;
    [SerializeField]
    private Image savecontent;
    [SerializeField]
    private Image loadcontent;
    private GameObject newSaveGame;
    private List<GameData> gameDataList;
    private Button savegameselectbutton;
    [SerializeField]
    private GameObject inputText;
    [SerializeField]
    private GameObject loadText;

    public void resumeBttn()
    {
        MenuEventHandler.ResumeButtonClicked?.Invoke();
    }

    public void saveMenuBttn()
    {
        SaveMenuOpenerInput(true);
    }

    public void saveGameBackButton()
    {
        SaveMenuOpenerInput(false);
    }
    public void loadGameBackButton()
    {
        LoadMenuOpenerInput(false);
    }
    public void loadMenuBttn()
    {
        LoadMenuOpenerInput(true);
    }
    public void SaveMenuOpenerInput(bool isOpened)
    {
        SaveManager.Instance.LoadGameData();
        saveMenuOpener(isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(!isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(1).gameObject.SetActive(isOpened);
    }
    public void LoadMenuOpenerInput(bool isOpened)
    {

        SaveManager.Instance.LoadGameData();
        loadMenuOpener(isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(!isOpened);
        GameObject.Find("CanvasMenu").transform.GetChild(2).gameObject.SetActive(isOpened);
    }


    public void saveMenuOpener(bool isOpened)
    {
        gameDataList = SaveManager.Instance.gameDataList;

        if (isOpened)
        {
            createSaveGamepre();
        }
        else
        {
            if (gameDataList.Count > 0)
            {
                for (int i = 0; i < gameDataList.Count; i++)
                {
                    var loaddata = GameObject.Find("saveGameContent").transform.GetChild(i).gameObject;
                    if (loaddata != null)
                    {
                        Destroy(loaddata);
                    }
                }
            }
        }
    }

    public void loadMenuOpener(bool isOpened)
    {
        gameDataList = SaveManager.Instance.gameDataList;
        if (isOpened)
        {
            if (gameDataList.Count > 0)
            {
                TMP_Text text = loadText.GetComponent<TMP_Text>();
                text.text = gameDataList[0].gameName;
            }
            createLoadGamepre();
        }
        else
        {
            if (gameDataList.Count > 0)
            {
                for (int i = 0; i < gameDataList.Count; i++)
                {
                    var loaddata = GameObject.Find("loadGameContent").transform.GetChild(i).gameObject;
                    if (loaddata != null)
                    {
                        Destroy(loaddata);
                    }

                }
            }
        }
    }

    void TaskOnClick(int index)
    {

        TMP_InputField text = inputText.GetComponent<TMP_InputField>();
        text.text = gameDataList[index].gameName;
        //string text = inputText.GetComponent<TMP_InputField>().text;
    }
    void TaskOnClick1(int index)
    {
        TMP_Text text = loadText.GetComponent<TMP_Text>();
        text.text = gameDataList[index].gameName;
    }

    public void saveGameSaveButton()
    {
        string text = inputText.GetComponent<TMP_InputField>().text;
        if (text != "")
        {
            for (int i = 0; i < gameDataList.Count; i++)
            {
                Destroy(GameObject.Find("saveGameContent").transform.GetChild(i).gameObject);
            }
            SaveManager.Instance.Save(text);
            createSaveGamepre();
        }
    }


    private void createSaveGamepre()
    {
        for (int i = 0; i < gameDataList.Count; i++)
        {
            newSaveGame = Instantiate(saveGame, savecontent.transform);
            TMP_Text text1 = newSaveGame.transform.GetChild(0).GetComponent<TMP_Text>();
            text1.text = gameDataList[i].gameName;
            TMP_Text text2 = newSaveGame.transform.GetChild(1).GetComponent<TMP_Text>();
            text2.text = gameDataList[i].gameTime;
            savegameselectbutton = newSaveGame.GetComponentInChildren<Button>();
            int temp = i;
            savegameselectbutton.onClick.AddListener(delegate { TaskOnClick(temp); });
        }
    }
    private void createLoadGamepre()
    {
        for (int i = 0; i < gameDataList.Count; i++)
        {
            newSaveGame = Instantiate(saveGame, loadcontent.transform);
            TMP_Text text1 = newSaveGame.transform.GetChild(0).GetComponent<TMP_Text>();
            text1.text = gameDataList[i].gameName;
            TMP_Text text2 = newSaveGame.transform.GetChild(1).GetComponent<TMP_Text>();
            text2.text = gameDataList[i].gameTime;
            savegameselectbutton = newSaveGame.GetComponentInChildren<Button>();
            int temp = i;
            savegameselectbutton.onClick.AddListener(delegate { TaskOnClick1(temp); });
        }
    }

    public void LoadGameBttn()
    {
        //PlayerManager.Instance.DeselectUnits();
        TMP_Text text = loadText.GetComponent<TMP_Text>();
        if (text.text != "")
        {

            SaveManager.Instance.Load(text.text);
        }
    }
}
