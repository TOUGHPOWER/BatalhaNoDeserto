using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiMaster : MonoBehaviour
{
    [SerializeField] GameObject optionsUI;
    [SerializeField] Dropdown colorBlindDropdown;
    public void LoadMainGameScene() 
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ShowOptionsUI()
    {
        optionsUI.SetActive(true);
    }

    public void StopShowingOptionsUI()
    {
        optionsUI.SetActive(false);
        
    }

    

}
