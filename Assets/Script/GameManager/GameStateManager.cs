using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
    normal,
    pause,
    GameOver,
    dialogue
}
public class GameStateManager : SingleTon<GameStateManager>
{   
    GameState m_curState;
    public GameState curstate
    {
        get { return m_curState; }
        set { m_curState = value; }
    }

    public static event Action onGameOver;

    public void ChangeState(GameState state)
    {   if (m_curState == state)
            return;
        this.m_curState = state;
        switch (state)
        {
            case GameState.normal:
                OnNormalState();
                break;
            case GameState.pause: 
                OnPauseState();
                break;
            case GameState.dialogue:
                OnDialogueState();
                break;
            case GameState.GameOver:
                OnGameOverState();
                break;
        }
    }
    void OnNormalState()
    {
        Time.timeScale = 1;
    }
    void OnPauseState()
    {
        Time.timeScale = 0;
    }
    void OnDialogueState()
    {
        Debug.Log("DialogueState");
    }
    void OnGameOverState()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        onGameOver?.Invoke();
        yield return new WaitForSeconds(5f);
        ChangeState(GameState.normal);
        SceneControler.Instance.LoadMainMenu();
    }
}
