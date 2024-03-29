using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            PickupSpawner pickupSpawner = GetComponent<PickupSpawner>();
            pickupSpawner?.DropItems();

            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
