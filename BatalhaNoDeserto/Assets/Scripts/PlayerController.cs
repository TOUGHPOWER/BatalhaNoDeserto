using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UiMaster ui;
    [SerializeField] private float velocityController;
    [SerializeField] private float [] lanes;
    private int currentLane;
    private float lastAxisValue;
    private new Rigidbody rigidbody;
    public Spawner[] guns;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastAxisValue = Input.GetAxis("Horizontal");
        currentLane = 0;
    }
    
    public void UpdateFirerate()
    {
        foreach (Spawner gun in guns)
        {
            gun.TimerMax = gun.NormalTimerMax - ui.FireRatePlayer;
            //print(gun.NormalTimerMax + "-" + ui.FireRatePlayer + "=" + gun.TimerMax);
            if (gun.TimerMax <= 0)
                gun.TimerMax = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocty = transform.forward * (velocityController + ui.VelPlayer 
            + Input.GetAxis("Vertical") * velocityController) * 10;
        
        if(ui.FixedMov)
        {
            if(Input.GetButtonDown("Left") || Input.GetButtonDown("Right"))
            {
                currentLane += (Input.GetAxis("Horizontal") > 0)? 1: 
                    (Input.GetAxis("Horizontal") < 0)? -1: 0;

                if(currentLane < 0)
                    currentLane = 0;
                else if(currentLane >= lanes.Length)
                    currentLane = lanes.Length-1;
            }
            
            transform.position = new Vector3( lanes[currentLane], transform.position.y, transform.position.z);
        }else
        {
            velocty += transform.right * Input.GetAxis("Horizontal") * 
                velocityController * 10;
        }

        
        velocty.y = rigidbody.velocity.y;
        rigidbody.velocity = velocty;
        lastAxisValue = Input.GetAxis("Horizontal");
    }
}
