using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Enemies.System
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract float Health { get; set; }
        public abstract float AttackRange { get; set; }
        public abstract float Power { get; set; }
        public abstract float AttackDuration { get; set; }

        public abstract bool isDie { get; set; }
        public abstract bool isAttacking { get; set; }

        public abstract IEnumerator Attack();
        public abstract void GetAttacked();
        public abstract void Die();

        private Animator animator;

        public  Animator Animator
        {
            get
            { 
                if(animator == null)
                {
                    if(transform.GetComponent<Animator>() != null)
                    {
                        animator = transform.GetComponent<Animator>();
                    }
                    else
                    {
                        animator = transform.GetComponentInChildren<Animator>();
                    }
                    if(animator != null)
                        Debug.Log("Animator founded");
                }
                return animator;
            }
            set {}
        }


        private PlayerController _player;
        public PlayerController Player
        {
            get
            {
                if(_player == null)
                {
                    _player = FindObjectOfType<PlayerController>();
                }
                return _player;
            }
            set
            {

            }
        }


        public float DistancePlayer(PlayerController player)
        {
            float playerPosX = player.transform.position.x;
            float playerPosY = player.transform.position.y;

            float enemyPosX = transform.position.x;
            float enemyPosY = transform.position.y;

            float x = Mathf.Abs(playerPosX - enemyPosX);
            float y = Mathf.Abs(playerPosY - enemyPosY);

            float result = Mathf.Sqrt((x * x) + (y * y));

            return result;
        }
        
    }
}

