using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const int HEALTH_MODIFIER = 1;
    private const float VELOCITY_MODIFIER = 1/2;
    private const float SHOOTING_RATE_MODIFIER = 1/3;
    
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
        //hp.AddMaxHp(ui.Dificulty * HEALTH_MODIFIER);
        move.Velocity += ui.VelEnemy * VELOCITY_MODIFIER;
        foreach (Spawner gun in guns)
        {
            //gun.TimerMax -= ui.Dificulty * SHOOTING_RATE_MODIFIER;
        }
    }
}
