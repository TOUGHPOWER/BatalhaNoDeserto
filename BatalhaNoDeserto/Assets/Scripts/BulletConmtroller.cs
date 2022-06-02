using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConmtroller : MonoBehaviour
{
    private const int VEL_MODIFIER = 2;
    private UiMaster ui;
    private MoveFoward move;
    void Start()
    {
        move = GetComponent<MoveFoward>();
        ui = FindObjectOfType<UiMaster>();
        //move.Velocity += ui.Dificulty * VEL_MODIFIER;
    }
}
