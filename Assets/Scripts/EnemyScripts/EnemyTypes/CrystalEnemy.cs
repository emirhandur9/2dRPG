using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies.System;

namespace Enemies.Types
{
    public class CrystalEnemy : AgressiveEnemy
    {
        public override float AngerRange { get; set; }
        public override float Health { get; set; }
        public override float AttackRange { get; set; }
        public override float Power { get; set; }
        public override float AttackDuration { get; set; }
        public override bool isDie { get; set; }
        public override bool isAttacking { get; set; }
     

        private void Start()
        {
            AttackDuration = 1;
            AngerRange = 15;
            AttackRange = 10;
            
        }

        private void Update()
        {
            PlayerDedection(Player);
        }
        public override void Anger()
        {
            //Debug.Log("Anger");
        }

        public override IEnumerator Attack()
        {
            while (isAttacking)
            {
                yield return new WaitForSeconds(AttackDuration);
                Animator.SetTrigger("Attack");
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

