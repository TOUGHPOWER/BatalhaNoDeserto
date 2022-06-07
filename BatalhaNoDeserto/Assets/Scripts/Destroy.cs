using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] GameObject parent;
    
    public void DestroyParent()
    {
        parent.SetActive(false);
    }
}
