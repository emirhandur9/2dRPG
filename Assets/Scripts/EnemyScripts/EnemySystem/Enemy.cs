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

