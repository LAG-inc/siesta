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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.SI.ShowPauseMenu();
            PhaseManager.SI.Pause(true);
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