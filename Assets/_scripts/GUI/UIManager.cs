using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region inGame

    [SerializeField]
    private List<Image> _lifesImages;

    [SerializeField]
    private Text _countDownText;

    [SerializeField]
    private GameObject _countDownContainer;

    [SerializeField]
    private Text _attemptsText;

    //Contenedor de la UI en el juego      
    [SerializeField]
    private GameObject _hud;
    #endregion

    #region inPause

    //Menus
    [SerializeField]
    private GameObject pauseMenu;

    #endregion

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        HidePauseMenu();
    }

    public void ExitGame()
    {
        //TODO
    }

    public void ShowCountDown(int start)
    {
        _countDownContainer.SetActive(true);

        StartCoroutine(PlayCountDown(start));

    }

    private void HideCountDown()
    {
        _countDownContainer.SetActive(false);
    }

    private IEnumerator PlayCountDown(int start)
    {

        for (int i = start; i > 0; i--)
        {

            _countDownText.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        HideCountDown();

        //TODO METHOD FOR RESUME GAME
    }

    public void LoseLife()
    {
        _lifesImages[_lifesImages.Count - 1].gameObject.SetActive(false);
        _lifesImages.Remove(_lifesImages[_lifesImages.Count - 1]);
    }


    public void ResetGame()
    {
        //TODO
        SceneManager.LoadScene(0);
    }

    public void RefreshAttempts(int number)
    {
        _attemptsText.text = number.ToString();
    }
}
