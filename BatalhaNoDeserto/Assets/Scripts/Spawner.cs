using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] public float NormalTimerMax { get; set; }
    [field: SerializeField] public float TimerMax { get; set; }
    [SerializeField] GameObject         spawnObject;
    [SerializeField] bool               automatic;
    [SerializeField] bool               onTriger;
    [SerializeField] bool               button;
    private float                       timer;
    public bool                         CanShoot { get; set; } = true;
    [SerializeField] AudioSource        sound;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;

        if (automatic && timer == 0)
            Shoot();
        else if (Input.GetButton("Fire1") && timer == 0 && button)
            Shoot();
        
    }

    private void Shoot() 
    {
        if (CanShoot)
        {
            if (sound != null)
                sound.Play();
            Instantiate(spawnObject, transform.position, transform.rotation);
            timer = TimerMax;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onTriger && other.gameObject.tag == "Player")
            Shoot();
    }
}
