using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager SI;
    public GameState currentGameState;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }


    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }
}