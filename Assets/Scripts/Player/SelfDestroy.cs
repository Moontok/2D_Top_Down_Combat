using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    private ParticleSystem ps = null;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }
    }

    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
}
