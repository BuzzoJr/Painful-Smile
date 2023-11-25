using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    void Start()
    {
        Invoke("Destroy", 1f);
    }

    void Destroy()
    {
       Destroy(this.gameObject); 
    }
}
