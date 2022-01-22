using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies.System;
using DG.Tweening;

namespace Enemies.Types
{
    public class SunFlower : AgressiveEnemy
    {
        public override float Health { get; set; }
        public override float Power { get; set; }
        public override float AngerRange { get; set; }
        public override float AttackRange { get; set; }
        public override float AttackDuration { get; set; }
        public override bool isDie { get; set; }
        public override bool isAttacking { get; set; }

        public float bulletSpeed;

        PlayerController player;

        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform bulletSpawnPoint;
        private void Start()
        {
            //Datalarý al.
            player = FindObjectOfType<PlayerController>();
            AngerRange = 15;
            AttackRange = 10;
            isDie = false;
            AttackDuration = 1;
        }
        private void Update()
        {
            float distance = DistancePlayer(player);

            if(distance < AngerRange)
            {
                if(distance < AttackRange)
                {  
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        StartCoroutine(Attack());
                    }
                }
                else
                {
                    Anger();
                    isAttacking = false;

                }

            }

        }


        public override void Anger()
        {
            Debug.Log("Anger");
        }

        public override IEnumerator Attack()
        {
            while (isAttacking)
            {
                yield return new WaitForSeconds(AttackDuration);
                GameObject bullet = Instantiate(bulletPrefab, transform);
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.DOMove(player.transform.position, bulletSpeed);
            }
            
        }

        public override void Die()
        {
            
        }

        public override void GetAttacked()
        {
            
        }

        
    }
}

