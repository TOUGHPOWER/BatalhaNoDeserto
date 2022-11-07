using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField] GameObject infoPopUp;
    [SerializeField] TextMeshProUGUI popUpText;
    [SerializeField] string infoText;

    [SerializeField] Button firstButtonTutorialMenu;
    // Start is called before the first frame update
    void Start()
    {
        infoPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            infoPopUp.SetActive(true);
            Time.timeScale = 0;
            firstButtonTutorialMenu.Select();
            popUpText.text = infoText;
        }

    }

    public void CloseTutorialMenu() 
    {
        infoPopUp.SetActive(false);
        Time.timeScale = 1;

    }


}
