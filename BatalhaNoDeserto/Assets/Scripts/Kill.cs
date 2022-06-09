using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    [SerializeField] GameObject agent;
    [SerializeField] GameObject dropItem;
    
    public void DestroyParent()
    {
        agent.SetActive(false);
        if (dropItem != null && Random.value > 0.5f)
            Instantiate(dropItem, agent.transform.position, agent.transform.rotation);
    }
}
