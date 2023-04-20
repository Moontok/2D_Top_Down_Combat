using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX = null;

    private WeaponInfo weaponInfo = null;
    private Vector3 startPosition = Vector3.zero;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        Indestructable indestructable = other.GetComponent<Indestructable>();

        if (!other.isTrigger && (enemyHealth || indestructable))
        {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(startPosition, transform.position) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }        
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
