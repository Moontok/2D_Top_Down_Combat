using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType
    {
        GoldCoin,
        HealthGlobe,
        StaminaGlobe
    }

    [SerializeField] private PickupType pickupType = PickupType.GoldCoin;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float accelarationRate = .2f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private AnimationCurve animCurve = null;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float maxDistanceAway = .2f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir = Vector3.zero;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickupDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelarationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-maxDistanceAway, maxDistanceAway);
        float randomY = transform.position.y + Random.Range(-maxDistanceAway, maxDistanceAway);
        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (pickupType)
        {
            case PickupType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickupType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickupType.StaminaGlobe:
                Stamina.Instance.RefreshStamina(1);
                break;
            default:
                break;
        }
    }
}
