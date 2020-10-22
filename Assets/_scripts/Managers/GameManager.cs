using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    GameOver,
    Win
}

public class GameManager : MonoBehaviour
{
    public static GameManager SI;
    public GameState currentGameState = GameState.MainMenu;
    
    private void Awake()
    {
        SI = SI == null ? this : SI;
        ChangeGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameState(GameState.MainMenu);
            UIManager.SI.ShowPauseMenu();
        }
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == GameState.InGame)
        {
            //TODO
        }

        if (newGameState == GameState.GameOver)
        {
            UIManager.SI.PlayTimeLineGameOver();
        }

        if (newGameState == GameState.Win)
        {
            UIManager.SI.PlayTimeLineWin();
        }

        currentGameState = newGameState;
    }
}