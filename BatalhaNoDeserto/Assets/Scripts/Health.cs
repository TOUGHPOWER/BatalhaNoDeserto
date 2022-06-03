using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int          HealthPoints { get; private set; }
    [SerializeField] private int                maxHP = 0;
    [SerializeField] private Slider             hpSlider;

    public void ChangeHealth(int value)
    {
        HealthPoints += value;
        if (HealthPoints > maxHP)
            HealthPoints = maxHP;
        else if (HealthPoints <= 0)
            Kill();

        if (hpSlider != null)
            hpSlider.value = HealthPoints;
    }

    public void SetHealth(int value)
    {
        HealthPoints = value;
        if (HealthPoints > maxHP)
            HealthPoints = maxHP;
        else if (HealthPoints < 0)
            HealthPoints = 0;

        if (hpSlider != null)
            hpSlider.value = HealthPoints;
    }

    private void Kill()
    {
        HealthPoints = 0;
        if (hpSlider != null)
            hpSlider.value = HealthPoints;
        gameObject.SetActive(false);
    }

    public void AddMaxHp(int value)
    {
        maxHP += value;
        HealthPoints += value;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = HealthPoints;
        }
    }

    private void Start()
    {
        HealthPoints = maxHP;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = HealthPoints;
        }
    }
}
