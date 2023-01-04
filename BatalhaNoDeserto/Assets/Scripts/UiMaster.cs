using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wilberforce;
using UnityEngine.Audio;

public class UiMaster : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject         optionsUI;
    [SerializeField] GameObject         pauseMenu;
    [SerializeField] GameObject         wonMenu;
    [SerializeField] GameObject         lostMenu;
    [SerializeField] GameObject         mainMenu;
    [SerializeField] GameObject         selectorMenu;
    [SerializeField] TutorialPopUp      tutorialPopUp;
    [SerializeField] GameObject         starterPopup;
    [Header("Buttons")]
    [SerializeField] Button             firstButtonWon;
    [SerializeField] Button             firstButtonLost;
    [SerializeField] Button             firstButtonMainMenu;
    [SerializeField] Button             firstButtonPauseMenu;
    [SerializeField] Button             firstButtonOptionsMenu;
    [SerializeField] Button             firstButtonLevelSelector;
    [SerializeField] Button             starterButton;
    [Header("Sliders & Dropdowns & Toggles")]
    [SerializeField] Dropdown           colorBlindDropdown;
    [SerializeField] Slider             velPlayerSlider;
    [SerializeField] Slider             velEnemylider;
    [SerializeField] Slider             velPorjEnemySlider;
    [SerializeField] Slider             velPorjPlayerSlider;
    [SerializeField] Slider             velFRPlayerSlider;
    [SerializeField] Slider             velFREnemySlider;
    [SerializeField] Slider             velHPEnemySlider;
    [SerializeField] Slider             musicVolumeSlider;
    [SerializeField] Slider             effectVolumeSlider;
    [SerializeField] Toggle             fixedMovToggle;
    [Header("Variaveis auxiliares")]
    [SerializeField] Colorblind         colorblind;
    [SerializeField] bool               InGame;
    [SerializeField] bool               InTutorial;
    [SerializeField] GameObject         player;
    [SerializeField] PlayerController   playerController;

    [Header("Options")]
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectVolume;
    [field: SerializeField] public int  VelPlayer { get; private set; }
    [field: SerializeField] public int  VelEnemy { get; private set; }
    [field: SerializeField] public int  VelProjectEnemy { get; private set; }
    [field: SerializeField] public int  VelProjectPlayer { get; private set; }
    [field: SerializeField] public float  FireRatePlayer { get; private set; }
    [field: SerializeField] public float  FireRateEnemy { get; private set; }
    [field: SerializeField] public int  HealthEnemy { get; private set; }
    [field: SerializeField] public bool FixedMov { get; private set; }
    [Header("Music")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip normalMusic;
    [SerializeField] AudioClip wonMusic;
    [SerializeField] AudioClip lostMusic;
    [SerializeField] AudioMixer audioMixer;


    //funcoes base
    private void Start()
    {
        LoadPrefs();
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(source != null)
            source.clip = normalMusic;
        optionsUI.SetActive(false);
        pauseMenu.SetActive(false);
        wonMenu.SetActive(false);
        lostMenu.SetActive(false);

        if (InGame && InTutorial)
        {
            CloseStarterPopup();
        }
        else if (InGame)
        {
            OpenStarterPopup();
        }

    }
    private void Update()
    {
        if (InGame && Input.GetButtonDown("Pause") || InTutorial && Input.GetButtonDown("Pause"))
            OpenPauseMenu();

        if (InGame && !player.activeInHierarchy && !lostMenu.activeInHierarchy)
            OpenLoseMenu();

        if(Input.GetMouseButtonDown(0)) 
        {
            print("Hello");
            if (optionsUI.activeInHierarchy)
            {
                firstButtonOptionsMenu.Select();
                
            }
            else if (pauseMenu.activeInHierarchy)
            {
                firstButtonPauseMenu.Select();
            }
            else if (wonMenu.activeInHierarchy)
            {
                firstButtonWon.Select();
                
            }
            else if (lostMenu.activeInHierarchy)
            {
                firstButtonLost.Select();
                
            }
            else if (mainMenu.activeInHierarchy)
            {
                firstButtonMainMenu.Select();
                
            }
            else if (selectorMenu.activeInHierarchy)
            {
                firstButtonLevelSelector.Select();

            }
            else if (starterPopup.activeInHierarchy) 
            {
                starterButton.Select();
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        


    }

    //abrir e fechar menus
    public void OpenWinigMenu()
    {
        source.clip = wonMusic;
        source.Play();
        firstButtonWon.Select();
        Time.timeScale = 0;
        wonMenu.SetActive(true);
        StopShooting();
    }
    public void OpenLoseMenu()
    {
        source.clip = lostMusic;
        source.Play();
        firstButtonLost.Select();
        Time.timeScale = 0;
        lostMenu.SetActive(true);
    }
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        source.volume = 0.5f;
        pauseMenu.SetActive(true);
        firstButtonPauseMenu.Select();
        StopShooting();
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        source.volume = 1f;
        pauseMenu.SetActive(false);
        StopShowingOptionsUI();
        tutorialPopUp.CloseTutorialMenu();
        StartShooting();
    }
    public void ShowOptionsUI()
    {
        optionsUI.SetActive(true);
        firstButtonOptionsMenu.Select();
    }
    public void StopShowingOptionsUI()
    {
        optionsUI.SetActive(false);
        SavePrefs();
    }

    public void OpenStarterPopup() 
    {
        Time.timeScale = 0;
        starterPopup.SetActive(true);
        starterButton.Select();
        
    }
    public void CloseStarterPopup()
    {
        starterPopup.SetActive(false);
        Time.timeScale = 1;
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
    public void LoadLevelSelec()
    {
        SavePrefs();
        SceneManager.LoadScene("Level Selector");
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
        FireRatePlayer      = velFRPlayerSlider.value;
        FireRateEnemy       = velFREnemySlider.value;
        HealthEnemy         = (int)velHPEnemySlider.value;
        musicVolume         = musicVolumeSlider.value;
        effectVolume        = effectVolumeSlider.value;
        FixedMov            = fixedMovToggle.isOn;
        audioMixer.SetFloat("musicVol", musicVolume);
        audioMixer.SetFloat("effectVol", effectVolume);
        if(playerController != null)
            playerController.UpdateFirerate();
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
        print("Save");
        PlayerPrefs.SetInt("ColorBlind", colorblind.Type);
        PlayerPrefs.SetInt("VelPlayer", VelPlayer);
        PlayerPrefs.SetInt("VelEnemy", VelEnemy);
        PlayerPrefs.SetInt("VelProjectEnemy", VelProjectEnemy);
        PlayerPrefs.SetInt("VelProjectPlayer", VelProjectPlayer);
        PlayerPrefs.SetFloat("FireRatePlayer", FireRatePlayer);
        PlayerPrefs.SetFloat("FireRateEnemy", FireRateEnemy);
        PlayerPrefs.SetInt("HealthEnemy", HealthEnemy);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
        PlayerPrefs.SetInt("FixedMov", Convert.ToInt32(FixedMov));
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
        musicVolumeSlider.value     = PlayerPrefs.GetFloat("MusicVolume", 0);
        effectVolumeSlider.value    = PlayerPrefs.GetFloat("EffectVolume", 0);
        fixedMovToggle.isOn         = Convert.ToBoolean(PlayerPrefs.GetInt("FixedMov", 0));
        UpdateValues();
    }
}
