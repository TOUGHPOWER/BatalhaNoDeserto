using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wilberforce;

public class UiMaster : MonoBehaviour
{
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject wonMenu;
    [SerializeField] Button     firstButtonWon;
    [SerializeField] GameObject lostMenu;
    [SerializeField] Button     firstButtonLost;
    [SerializeField] Dropdown   colorBlindDropdown;
    [SerializeField] Colorblind colorblind;
    [SerializeField] bool       InGame;
    [SerializeField] GameObject player;


    private void Start()
    {
        LoadPrefs();
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (InGame && Input.GetButtonDown("Pause"))
            OpenPauseMenu();

        if (!player.activeInHierarchy && !lostMenu.activeInHierarchy)
            OpenLoseMenu();
    }
    public void OpenWinigMenu()
    {
        firstButtonWon.Select();
        Time.timeScale = 0;
        wonMenu.SetActive(true);
    }
    public void OpenLoseMenu()
    {
        firstButtonLost.Select();
        Time.timeScale = 0;
        lostMenu.SetActive(true);
    }
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        StopShowingOptionsUI();
    }
    public void LoadMainGameScene() 
    {
        SavePrefs();
        SceneManager.LoadScene("Main Scene");
    }
    public void LoadMainMenu()
    {
        SavePrefs();
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        SavePrefs();
        Application.Quit();
    }
    public void ShowOptionsUI()
    {
        optionsUI.SetActive(true);
    }
    public void StopShowingOptionsUI()
    {
        optionsUI.SetActive(false);
        SavePrefs();
    }
    public void UpdateColorBlind()
    {
        colorblind.Type = colorBlindDropdown.value;
        SavePrefs();
    }

    public void SavePrefs() 
    {
        PlayerPrefs.SetInt("ColorBlind", colorblind.Type);
    }

    public void LoadPrefs() 
    {
        colorBlindDropdown.value = PlayerPrefs.GetInt("ColorBlind", 0);
        UpdateColorBlind();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            OpenWinigMenu();
    }
}
