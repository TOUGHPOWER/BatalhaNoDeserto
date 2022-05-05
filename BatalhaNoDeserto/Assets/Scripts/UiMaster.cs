using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wilberforce;

public class UiMaster : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject         optionsUI;
    [SerializeField] GameObject         pauseMenu;
    [SerializeField] GameObject         wonMenu;
    [SerializeField] GameObject         lostMenu;
    [Header("Elementos UI")]
    [SerializeField] Button             firstButtonWon;
    [SerializeField] Button             firstButtonLost;
    [SerializeField] Dropdown           colorBlindDropdown;
    [SerializeField] Dropdown           dificultyDropdown;
    [Header("Variaveis auxiliares")]
    [SerializeField] Colorblind         colorblind;
    [SerializeField] bool               InGame;
    [SerializeField] GameObject         player;
    [field: SerializeField] public int  Dificulty { get; private set; }

    //funcoes base
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

        if (InGame && !player.activeInHierarchy && !lostMenu.activeInHierarchy)
            OpenLoseMenu();
    }

    //abrir e fechar menus
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
    public void ShowOptionsUI()
    {
        optionsUI.SetActive(true);
    }
    public void StopShowingOptionsUI()
    {
        optionsUI.SetActive(false);
        SavePrefs();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (InGame && other.gameObject.tag == "Player")
            OpenWinigMenu();
    }

    //loaders de scenes
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
    
    //saves prefs
    public void UpdateValues()
    {
        colorblind.Type = colorBlindDropdown.value;
        Dificulty = dificultyDropdown.value;
        SavePrefs();
    }

    public void SavePrefs() 
    {
        PlayerPrefs.SetInt("ColorBlind", colorblind.Type);
        PlayerPrefs.SetInt("Dificulty", Dificulty);
    }

    public void LoadPrefs() 
    {
        colorBlindDropdown.value = PlayerPrefs.GetInt("ColorBlind", 0);
        print(PlayerPrefs.GetInt("Dificulty", 0));
        dificultyDropdown.value = PlayerPrefs.GetInt("Dificulty", 0);
        UpdateValues();
    }
}
