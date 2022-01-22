using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] bool smoothMove;
    [SerializeField] PlayerStatsSO stats;

    [HideInInspector]
    private Rigidbody2D rb;
    PlayerUIController playerUI;
    PlayerAnimationController animationController;

    private float xInput, yInput;

    public float health;
    [SerializeField] private float dedectorRadius;
    [SerializeField] private LayerMask canDedecte;

    public static Action<PlayerController> onPlayerAttack;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = stats.Health;
        playerUI = GetComponent<PlayerUIController>();
        animationController = transform.GetComponentInChildren<PlayerAnimationController>();
    }
    //player layer 50'de
    private void Update()
    {

        CheckForMovementInput(smoothMove);
        CheckForAttackInput();
        animationController.CheckForWalkAnim(rb);






        Collider2D dedected = Physics2D.OverlapCircle(transform.position, dedectorRadius, canDedecte);
        if (dedected)
        {
            if(dedected.transform.childCount > 0)
            {
                if (dedected.transform.GetChild(0).name == "LayerLine")
                {
                    if(transform.position.y < dedected.transform.GetChild(0).position.y)
                    {
                        if (dedected.GetComponent<SpriteRenderer>().sortingOrder == 45) return;
                        dedected.GetComponent<SpriteRenderer>().sortingOrder = 45;
                    }
                    else
                    {
                        if (dedected.GetComponent<SpriteRenderer>().sortingOrder == 55) return;
                        dedected.GetComponent<SpriteRenderer>().sortingOrder = 55;
                    }
                }
            }
            

        }
    }

    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xInput, yInput) * movementSpeed;
    }
    private void CheckForMovementInput(bool smooth)
    {
        if (smooth)
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
        }
        else
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }
    }
    private void CheckForAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animationController.Attack();
            onPlayerAttack?.Invoke(this);
        }
    }

    public void GetAttacked()
    {
        health -= 10;
        CheckHealthBar();
    }
    public void CheckHealthBar()
    {
        playerUI.HealthBar.fillAmount = health / 100;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, dedectorRadius);
    }
}
