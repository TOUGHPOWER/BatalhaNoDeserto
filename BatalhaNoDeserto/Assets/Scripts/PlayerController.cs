using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocityOffset;
    [SerializeField] private float velocityController;
    private new Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocty = transform.forward * (velocityOffset + Input.GetAxis("Vertical") * velocityController) * 10  + 
            transform.right*Input.GetAxis("Horizontal") *velocityController * 10;
        rigidbody.velocity = velocty;
    }
}
