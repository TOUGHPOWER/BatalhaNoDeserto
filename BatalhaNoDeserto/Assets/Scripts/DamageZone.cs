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
    private bool damageOverTime;

    private void OnTriggerEnter(Collider other)
    {
        Health target = other.gameObject.GetComponent<Health>();

        if (target != null)
            target.ChangeHealth(-damage);

        if (destroyInInpact)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (damageOverTime)
        {
            Health target = other.gameObject.GetComponent<Health>();

            if (target != null)
                target.ChangeHealth(-damage * Time.deltaTime);
        }
    }
}
