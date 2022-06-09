using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool destroyInInpact;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private GameObject drop;

    private void OnTriggerEnter(Collider other)
    {
        Health target = other.gameObject.GetComponent<Health>();

        if (target != null && target.gameObject.tag == targetTag) 
        {
            target.ChangeHealth(-damage);

            if (destroyInInpact)
            {
                if (drop != null)
                    Instantiate(drop, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }  
    }
}
