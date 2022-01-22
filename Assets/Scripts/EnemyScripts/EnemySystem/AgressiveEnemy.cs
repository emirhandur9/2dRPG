using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.System
{
    public abstract class AgressiveEnemy : Enemy
    {

        public abstract float AngerRange { get; set; }
        public abstract void Anger();


        public void PlayerDedection(PlayerController player)
        {
            float distance = DistancePlayer(player);

            if (distance < AngerRange)
            {
                if (distance < AttackRange)
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

        
    }
}

