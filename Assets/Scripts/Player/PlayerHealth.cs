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

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }        
    }

    public void TakeDamage(int damage, Transform hitTransform)
    {
        if (!canTakeDamage) return;

        knockback.GetKnockedBack(hitTransform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
