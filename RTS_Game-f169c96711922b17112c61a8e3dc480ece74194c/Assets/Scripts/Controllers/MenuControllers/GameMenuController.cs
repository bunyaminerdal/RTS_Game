using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    Closed,
    MainMenu,
    GameMenu,
    LoadMenu,
    SaveMenu,
    OptionsMenu
}
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

    [SerializeField]
    private GameObject GameMenuObj;
    [SerializeField]
    private GameObject LoadMenuObj;
    [SerializeField]
    private GameObject SaveMenuObj;
    private Dictionary<MenuType, GameObject> menuDic;

    private void Awake()
    {
        menuDic = new Dictionary<MenuType, GameObject>();
        menuDic.Add(MenuType.GameMenu, GameMenuObj);
        menuDic.Add(MenuType.SaveMenu, SaveMenuObj);
        menuDic.Add(MenuType.LoadMenu, LoadMenuObj);

    }
    private void OnEnable()
    {
        MenuEventHandler.GameMenuClicked.AddListener(GameMenuOpener);
    }
    private void OnDisable()
    {
        MenuEventHandler.GameMenuClicked.RemoveListener(GameMenuOpener);
    }
    public void resumeBttn()
    {
        MenuEventHandler.ResumeButtonClicked?.Invoke();
    }

    public void saveMenuBttn()
    {
        SaveManager.Instance.LoadGameData();
        GameMenuOpener(MenuType.SaveMenu);
        saveMenuOpener();
    }

    public void saveGameBackButton()
    {
        GameMenuOpener(MenuType.GameMenu);
    }
    public void loadGameBackButton()
    {
        GameMenuOpener(MenuType.GameMenu);
    }
    public void loadMenuBttn()
    {
        SaveManager.Instance.LoadGameData();
        GameMenuOpener(MenuType.LoadMenu);
        loadMenuOpener();
    }


    private void GameMenuOpener(MenuType menuType)
    {
        foreach (MenuType type in menuDic.Keys)
        {
            if (menuDic[type] != null) menuDic[type].SetActive(false);
            if (type == menuType && menuType != MenuType.Closed)
            {
                if (menuDic[type] != null) menuDic[type].SetActive(true);
                MenuEventHandler.CurrentMenuChanged?.Invoke(type);
            }
        }

    }


    public void saveMenuOpener()
    {
        gameDataList = SaveManager.Instance.gameDataList;
        if (gameDataList.Count > 0 && GameObject.Find("saveGameContent").transform.childCount > 0)
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
        createSaveGamepre();

    }

    public void loadMenuOpener()
    {
        gameDataList = SaveManager.Instance.gameDataList;
        if (gameDataList.Count > 0 && GameObject.Find("loadGameContent").transform.childCount > 0)
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
        if (gameDataList.Count > 0)
        {
            TMP_Text text = loadText.GetComponent<TMP_Text>();
            text.text = gameDataList[0].gameName;
        }
        createLoadGamepre();

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
