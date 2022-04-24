using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float HealthPoints { get; private set; }
    [SerializeField] private float maxHP = 0;
    [SerializeField] private Text hpText;

    public void ChangeHealth(float value)
    {
        HealthPoints += value;
        if (HealthPoints > maxHP)
            HealthPoints = maxHP;
        else if (HealthPoints <= 0)
            Kill();

        if (hpText != null)
            hpText.text = HealthPoints.ToString();
    }

    public void SetHealth(float value)
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
        //play animation
        HealthPoints = 0;
        if (hpText != null)
            hpText.text = HealthPoints.ToString();
        DestroySelf();
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
