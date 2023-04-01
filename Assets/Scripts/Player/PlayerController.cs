using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    public static PlayerController Instance;

    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCD = .25f;
    [SerializeField] private TrailRenderer myTrailRenderer = null;

    private float moveSpeed = 1f;
    private PlayerControls playerControls = null;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;
    private bool isDashing = false;

    private bool facingLeft = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        moveSpeed = baseSpeed;
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (isDashing) return;

        isDashing = true;
        myTrailRenderer.emitting = true;
        moveSpeed *= dashSpeed;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = baseSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
