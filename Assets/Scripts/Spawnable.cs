using UnityEngine;
using System.Collections;

public class Spawnable : PooledObject
{
    public Rigidbody Body { get; private set; }

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider enteredCollider)
    {
        if (enteredCollider.CompareTag("KillZone"))
        {
            ReturnToPool();
        }
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<Renderer>().material = mat;
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).GetComponent<Renderer>().material = mat;
    }
}
