using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float          timerMax;
    [SerializeField] GameObject     spawnObject;
    [SerializeField] bool           automatic;
    [SerializeField] bool           onTriger;
    [SerializeField] bool           button;
    private float                   timer;

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
        Instantiate(spawnObject, transform.position, transform.rotation);
        timer = timerMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onTriger && other.gameObject.tag == "Player")
            Shoot();
    }
}
