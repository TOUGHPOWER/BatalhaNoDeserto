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
    [SerializeField] Slider             velPorjPlayerSlider;
    [SerializeField] Slider             velFRPlayerSlider;
    [SerializeField] Slider             velFREnemySlider;
    [SerializeField] Slider             velHPEnemySlider;
    [SerializeField] Slider             volumeSlider;
    [Header("Variaveis auxiliares")]
    [SerializeField] Colorblind         colorblind;
    [SerializeField] bool               InGame;
    [SerializeField] GameObject         player;
    [SerializeField] PlayerController   playerController;

    [Header("Options")]
    [SerializeField] private float volume;
    [field: SerializeField] public int  VelPlayer { get; private set; }
    [field: SerializeField] public int  VelEnemy { get; private set; }
    [field: SerializeField] public int  VelProjectEnemy { get; private set; }
    [field: SerializeField] public int  VelProjectPlayer { get; private set; }
    [field: SerializeField] public float  FireRatePlayer { get; private set; }
    [field: SerializeField] public float  FireRateEnemy { get; private set; }
    [field: SerializeField] public int  HealthEnemy { get; private set; }
    [field: SerializeField] public bool FixedMov { get; private set; }
    [Header("Music")]
    [SerializeField] AudioSource sorce;
    [SerializeField] AudioClip normalMusic;
    [SerializeField] AudioClip wonMusic;
    [SerializeField] AudioClip lostMusic;


    //funcoes base
    private void Start()
    {
        LoadPrefs();
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sorce.clip = normalMusic;
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
        sorce.clip = wonMusic;
        sorce.Play();
        firstButtonWon.Select();
        Time.timeScale = 0;
        wonMenu.SetActive(true);
        StopShooting();
    }
    public void OpenLoseMenu()
    {
        sorce.clip = lostMusic;
        sorce.Play();
        firstButtonLost.Select();
        Time.timeScale = 0;
        lostMenu.SetActive(true);
    }
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        sorce.volume = 0.5f;
        pauseMenu.SetActive(true);
        StopShooting();
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        sorce.volume = 1f;
        pauseMenu.SetActive(false);
        StopShowingOptionsUI();
        StartShooting();
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

    private void StopShooting()
    {
        if(player!= null)
        {
            foreach (Spawner gun in playerController.guns)
            {
                gun.CanShoot = false;
            }
                
        }
        
    }
    private void StartShooting()
    {
        if (player != null)
        {
            foreach (Spawner gun in playerController.guns)
                gun.CanShoot = true;
        }
        
    }

    //loaders de scenes

    public void LoadLevel0()
    {
        SavePrefs();
        SceneManager.LoadScene("Level 0");
    }
    public void LoadLevel1() 
    {
        SavePrefs();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2() 
    {
        SavePrefs();
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SavePrefs();
        SceneManager.LoadScene("Level 3");
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
    public void Reaload()
    {
        SavePrefs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    //saves prefs
    public void UpdateValues()
    {
        colorblind.Type     = colorBlindDropdown.value;
        VelPlayer           = (int)velPlayerSlider.value;
        VelEnemy            = (int)velEnemylider.value;
        VelProjectEnemy     = (int)velPorjEnemySlider.value;
        VelProjectPlayer    = (int)velPorjPlayerSlider.value;
        FireRatePlayer      = (int)velFRPlayerSlider.value;
        FireRateEnemy       = (int)velFREnemySlider.value;
        HealthEnemy         = (int)velHPEnemySlider.value;
        volume              = volumeSlider.value;
        AudioListener.volume = volume;

    }

    public void ChangePlayerSpeed(float speed)          => VelPlayer = (int)speed;
    public void ChangeEnemySpeed(float speed)           => VelEnemy = (int)speed;
    public void ChangeEnemyBulletSpeed(float speed)     => VelProjectEnemy = (int)speed;
    public void ChangePlayerBulletSpeed(float speed)    => VelProjectPlayer = (int)speed;
    public void ChangePlayerFireRate(float speed)       => FireRatePlayer = speed;
    public void ChangeEnemyFireRate(float speed)        => FireRateEnemy = speed;
    public void ChangeEnemyHealth(float speed)          => HealthEnemy = (int)speed;

    public void SavePrefs() 
    {
        PlayerPrefs.SetInt("ColorBlind", colorblind.Type);
        PlayerPrefs.SetInt("VelPlayer", VelPlayer);
        PlayerPrefs.SetInt("VelEnemy", VelEnemy);
        PlayerPrefs.SetInt("VelProjectEnemy", VelProjectEnemy);
        PlayerPrefs.SetInt("VelProjectPlayer", VelProjectPlayer);
        PlayerPrefs.SetFloat("FireRatePlayer", FireRatePlayer);
        PlayerPrefs.SetFloat("FireRateEnemy", FireRateEnemy);
        PlayerPrefs.SetInt("HealthEnemy", HealthEnemy);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void LoadPrefs() 
    {
        colorBlindDropdown.value    = PlayerPrefs.GetInt("ColorBlind", 0);
        velPlayerSlider.value       = PlayerPrefs.GetInt("VelPlayer", 0);
        velEnemylider.value         = PlayerPrefs.GetInt("VelEnemy", 0);
        velPorjEnemySlider.value    = PlayerPrefs.GetInt("VelProjectEnemy", 0);
        velPorjPlayerSlider.value   = PlayerPrefs.GetInt("VelProjectPlayer", 0);
        velFRPlayerSlider.value     = PlayerPrefs.GetFloat("FireRatePlayer", 0);
        velFREnemySlider.value      = PlayerPrefs.GetFloat("FireRateEnemy", 0);
        velHPEnemySlider.value      = PlayerPrefs.GetInt("HealthEnemy", 0);
        volumeSlider.value          = PlayerPrefs.GetInt("Volume", 1);
        UpdateValues();
    }
}
