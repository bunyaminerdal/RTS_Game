using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resumeBttn()
    {
        inputManager.gameMenuOpenerInput(false);
    }

    public void saveMenuBttn()
    {
        inputManager.SaveMenuOpenerInput(true);
    }
    
    public void saveGameBackButton()
    {
        inputManager.SaveMenuOpenerInput(false);
    }
    public void loadGameBackButton()
    {
        inputManager.LoadMenuOpenerInput(false);
    }
    public void loadMenuBttn()
    {
        inputManager.LoadMenuOpenerInput(true);
    }

}
