using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] int treeHealth;
    [SerializeField] public GameObject leafParticle;
    [SerializeField] public Transform particlePoint;
    public bool isPlayerTrigger;
    private void OnEnable()
    {
        PlayerController.onPlayerAttack += PlayerAttack;
    }
    private void OnDisable()
    {
        PlayerController.onPlayerAttack -= PlayerAttack;
    }
    private void Start()
    {
        isPlayerTrigger = false;
    }

    public void PlayerAttack(PlayerController player)
    {
        if (isPlayerTrigger)
        {
            if (treeHealth > 0)
            {
                treeHealth--;
                GameObject leaf = Instantiate(leafParticle);
                leaf.transform.position = particlePoint.position;
                Destroy(leaf, 1.5f);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAxe"))
        {
            isPlayerTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAxe"))
        {
            isPlayerTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAxe"))
        {
            isPlayerTrigger = false;
        }
    }
}
