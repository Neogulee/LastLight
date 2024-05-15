using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private GameClear gameClear;
    private GameOverUi gameOver;  

    void Start()
    {
        gameClear = FindObjectOfType<GameClear>();
        gameOver = FindObjectOfType<GameOverUi>();
    }

    public void GameClear()
    {
        gameClear.GameClearStart();
    }
    public void GameOver()
    {
        gameOver.GameOverStart();
    }


}
