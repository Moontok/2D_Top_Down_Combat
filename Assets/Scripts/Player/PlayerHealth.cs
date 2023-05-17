using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrust = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth = 0;
    private bool canTakeDamage = true;

    private Knockback knockback = null;
    private Flash flash = null;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.collider.GetComponent<EnemyAI>();

        if (enemy && canTakeDamage)
        {
            TakeDamage(1);
            knockback.GetKnockedBack(other.transform, knockBackThrust);
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(DamageRecoveryRoutine());
        }        
    }

    public void TakeDamage(int damage)
    {
        canTakeDamage = false;
        currentHealth -= damage;

        // if (currentHealth <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
