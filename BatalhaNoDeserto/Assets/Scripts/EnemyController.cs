using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private MoveFoward          move;
    private Health              hp;
    private UiMaster            ui;
    [SerializeField] Spawner[]  guns;

    private void Start()
    {
        move = GetComponent<MoveFoward>();
        hp = GetComponent<Health>();
        ui = FindObjectOfType<UiMaster>();
        
        SetDificulty();
    }


    private void SetDificulty()
    {
        ui = FindObjectOfType<UiMaster>();
        hp.AddMaxHp(ui.HealthEnemy);
        move.Velocity += ui.VelEnemy;
        foreach (Spawner gun in guns)
        {
            gun.TimerMax = gun.NormalTimerMax - ui.FireRateEnemy;
            if (gun.TimerMax <= 0)
                gun.TimerMax = 0.1f;
        }
    }
}
