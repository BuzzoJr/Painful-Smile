using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedShipController : MonoBehaviour
{
    public List<GameObject> crewMembers;
    void Start()
    {
        for (int i = 0; i < crewMembers.Count - 2; i++)
        {
            crewMembers[Random.Range(0, crewMembers.Count)].SetActive(true);
        }
        Invoke("Destroy", 5f);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
