using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveGameData 
{
    public List<GameData> myGameDataList;

    public SaveGameData()
    {
        myGameDataList = new List<GameData>(); 
    }
}

[Serializable]
public class GameData
{
    public string gameName;
    public string gameTime;    

    public GameData(string _gameName,string _gameTime)
    {
        gameName = _gameName;
        gameTime = _gameTime;        
    }
}


