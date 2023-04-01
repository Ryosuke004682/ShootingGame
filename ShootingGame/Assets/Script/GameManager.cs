using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject title        = null;
    [SerializeField] private GameObject clearText    = null;
    [SerializeField] private GameObject gameOverText = null;


    public enum GameState
    {
        TITLE   ,
        GAMEMAIN,
        CLEAR   ,
        GAMEOVER,
    };

    private GameState state = GameState.TITLE;

    delegate void gameProc();
    Dictionary<GameState, gameProc> gameProcList;

    private void Start()
    {
        gameProcList = new Dictionary<GameState, gameProc>
        {
            {GameState.TITLE   ,    Title},
            {GameState.GAMEMAIN, GameMain},
            {GameState.CLEAR   ,    Clear},
            {GameState.GAMEOVER, GameOver},
        };

        state = GameState.TITLE;
    }

    private void Update()
    {
        gameProcList[state]();
    }

    private void Title()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = GameState.GAMEMAIN;
            StageControl.Instance.StageStart();

            title.SetActive(false);
        }
    }


    private void GameMain()
    {
        if (!StageControl.Instance.isPlay)
        {
           if(StageControl.Instance.playStop
                == StageControl.PlayStopCodeDef.PlayerDead)
           {
                gameOverText.SetActive(true);
                state = GameState.GAMEOVER;
           }
           else
            {
                clearText.SetActive(true);
                state = GameState.CLEAR;
            }
        }

    }
    
    /*クリア画面から、マウスをクリックしたらタイトルに戻す*/
    private void Clear()
    {
        if(Input.GetMouseButtonDown(0))
        {
            state   = GameState.TITLE;
            clearText.SetActive(false);
            title    .SetActive(true);

            StageControl.Instance.ResetStage();
        }
    }

    private void GameOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = GameState.GAMEOVER;

            gameOverText.SetActive(false);
            title.       SetActive(true);

            StageControl.Instance.ResetStage();

        }
    }

}
