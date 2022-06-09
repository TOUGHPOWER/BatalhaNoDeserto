using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UiMaster ui;
    [SerializeField] private float velocityController;
    private new Rigidbody rigidbody;
    public Spawner[] guns;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    public void UpdateFirerate()
    {
        foreach (Spawner gun in guns)
        {
            gun.TimerMax = gun.NormalTimerMax - ui.FireRatePlayer;
            print(gun.NormalTimerMax + "-" + ui.FireRatePlayer + "=" + gun.TimerMax);
            if (gun.TimerMax <= 0)
                gun.TimerMax = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocty = transform.forward * (velocityController + ui.VelPlayer 
            + Input.GetAxis("Vertical") * velocityController) * 10  + 
            transform.right*Input.GetAxis("Horizontal") *velocityController * 10;
        velocty.y = rigidbody.velocity.y;
        rigidbody.velocity = velocty;
    }
}
