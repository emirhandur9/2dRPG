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

    [SerializeField] private Transform[] bodyParts;

    public List<Transform> upwards = new List<Transform>();
    public List<Transform> downwards = new List<Transform>();
    public Transform mostCloseUp;
    public Transform mostCloseDown;
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



        Collider2D[] dedected = Physics2D.OverlapCircleAll(transform.position, dedectorRadius, canDedecte);
        upwards.Clear();
        downwards.Clear();
        mostCloseUp = null;
        mostCloseDown = null;
        if (dedected.Length > 1)
        {
            //Yukarýda olanlar ile aþaðýda olanlarý ayýr.

            Debug.Log(dedected.Length + " kadar obje bulundu");
            foreach (var item in dedected)
            {
                if (item.transform.position.y < transform.position.y)
                {
                    downwards.Add(item.transform);
                }
                else
                {
                    upwards.Add(item.transform);
                }
            }
            Debug.Log("yukarý aþaðý sýralandý");
            

            if(upwards.Count > 0)
            {
                for (int i = 0; i < upwards.Count; i++)
                {
                    for (int j = 0; j < upwards.Count; j++)
                    {
                        if (i != j)
                        {
                            if (upwards[i].position.y < upwards[j].position.y)
                            {
                                if(j < i)
                                {
                                    Transform temp = upwards[i];
                                    upwards[i] = upwards[j];
                                    upwards[j] = temp;
                                }
                                
                            }
                        }
                    }
                }
                mostCloseUp = upwards[0];
            }
            
            Debug.Log("yukarýlardan en yakýný bulundu");

            if(downwards.Count > 0)
            {
                for (int i = 0; i < downwards.Count; i++)
                {
                    for (int j = 0; j < downwards.Count; j++)
                    {
                        if (i != j)
                        {
                            if (downwards[i].position.y < downwards[j].position.y)
                            {
                                if(i < j)
                                {
                                    Transform temp = downwards[i];
                                    downwards[i] = downwards[j];
                                    downwards[j] = temp;
                                }
                                
                            }
                        }
                    }
                }
                mostCloseDown = downwards[0];
            }
            

            
            Debug.Log("aþaðýlardan en yakýný bulundu");
            if(mostCloseUp == null)
            {
                foreach (var item in bodyParts)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = mostCloseDown.transform.GetComponent<SpriteRenderer>().sortingOrder - 5;
                }
            }
            else if (mostCloseDown == null)
            {
                foreach (var item in bodyParts)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = mostCloseUp.transform.GetComponent<SpriteRenderer>().sortingOrder + 5;
                }
            }
            else
            {
                foreach (var item in bodyParts)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = (mostCloseUp.transform.GetComponent<SpriteRenderer>().sortingOrder + mostCloseDown.transform.GetComponent<SpriteRenderer>().sortingOrder) / 2;
                }
            }
            
            Debug.Log("iþlem baþarýlý");

        }
        else if (dedected.Length == 1)
        {
            if (dedected[0].transform.childCount > 0)
            {
                if (dedected[0].transform.GetChild(0).name == "LayerLine")
                {
                    if (transform.position.y < dedected[0].transform.GetChild(0).position.y)
                    {
                        foreach (var item in bodyParts)
                        {
                            item.GetComponent<SpriteRenderer>().sortingOrder = dedected[0].transform.GetComponent<SpriteRenderer>().sortingOrder + 5;
                        }
                        
                    }
                    else
                    {
                        foreach (var item in bodyParts)
                        {
                            item.GetComponent<SpriteRenderer>().sortingOrder = dedected[0].transform.GetComponent<SpriteRenderer>().sortingOrder - 5;
                        }
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
