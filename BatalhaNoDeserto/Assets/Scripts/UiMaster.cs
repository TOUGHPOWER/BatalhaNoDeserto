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
    [SerializeField] Slider             velPlayerSlider;
    [SerializeField] Slider             velEnemylider;
    [SerializeField] Slider             velPorjEnemySlider;
    [SerializeField] Slider             numEnemySlider;
    [Header("Variaveis auxiliares")]
    [SerializeField] Colorblind         colorblind;
    [SerializeField] bool               InGame;
    [SerializeField] GameObject         player;

    [Header("Options")]
    private int test;
    [field: SerializeField] public int  VelPlayer { get; private set; }
    [field: SerializeField] public int  VelEnemy { get; private set; }
    [field: SerializeField] public int  VelProjectEnemy { get; private set; }
    [field: SerializeField] public int  NumEnemy { get; private set; }
    [field: SerializeField] public bool FixedMov { get; private set; }

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
        VelPlayer = (int)velPlayerSlider.value;
        VelEnemy = (int)velEnemylider.value;
        VelProjectEnemy = (int)velPorjEnemySlider.value;
        NumEnemy = (int)numEnemySlider.value;
    }

    public void ChangePlayerSpeed(float speed) => VelPlayer = (int)speed;
    public void ChangeEnemySpeed(float speed) => VelEnemy = (int)speed;
    public void ChangeEnemyBulletSpeed(float speed) => VelProjectEnemy = (int)speed;
    public void ChangeEnemySpawner(float num) => NumEnemy = (int)num;

    public void SavePrefs() 
    {
        PlayerPrefs.SetInt("ColorBlind", colorblind.Type);
        PlayerPrefs.SetInt("VelPlayer", VelPlayer);
        PlayerPrefs.SetInt("VelEnemy", VelEnemy);
        PlayerPrefs.SetInt("VelProjectEnemy", VelProjectEnemy);
        PlayerPrefs.SetInt("NumEnemy", NumEnemy);
    }

    public void LoadPrefs() 
    {
        colorBlindDropdown.value = PlayerPrefs.GetInt("ColorBlind", 0);
        velPlayerSlider.value = PlayerPrefs.GetInt("VelPlayer", 0);
        velEnemylider.value = PlayerPrefs.GetInt("VelEnemy", 0);
        velPorjEnemySlider.value = PlayerPrefs.GetInt("VelProjectEnemy", 0);
        numEnemySlider.value = PlayerPrefs.GetInt("NumEnemy", 0);
        UpdateValues();
    }
}
