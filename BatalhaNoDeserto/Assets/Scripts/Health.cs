using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int HealthPoints { get; private set; }
    [SerializeField] private int maxHP = 0;
    [SerializeField] private TextMeshProUGUI hpText;

    public void ChangeHealth(int value)
    {
        HealthPoints += value;
        if (HealthPoints > maxHP)
            HealthPoints = maxHP;
        else if (HealthPoints <= 0)
            Kill();

        if (hpText != null)
            hpText.text = HealthPoints.ToString();
    }

    public void SetHealth(int value)
    {
        HealthPoints = value;
        if (HealthPoints > maxHP)
            HealthPoints = maxHP;
        else if (HealthPoints < 0)
            HealthPoints = 0;

        if (hpText != null)
            hpText.text = HealthPoints.ToString();
    }

    private void Kill()
    {
        HealthPoints = 0;
        if (hpText != null)
            hpText.text = HealthPoints.ToString();
        gameObject.SetActive(false);
    }

    private void DestroySelf() => Destroy(gameObject);

    private void Start()
    {
        HealthPoints = maxHP;
        print(HealthPoints);
        if (hpText != null)
            hpText.text = HealthPoints.ToString();
    }
}
