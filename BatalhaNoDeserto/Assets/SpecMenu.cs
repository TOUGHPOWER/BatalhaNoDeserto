using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject button; 

    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(button.activeInHierarchy && Input.GetButtonDown("Fire1"))
        {
            button.GetComponent<Button>().Select();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
