using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.System
{
    public abstract class AgressiveEnemy : Enemy
    {

        public abstract float AngerRange { get; set; }
        public abstract void Anger();


        /*public override float Health { get; set; }
        public override float AngerRange { get; set; }
        public override float Power { get; set; }

        public override void Attack()
        {

        }

        public override void Die()
        {

        }

        public override void PlayerDedection()
        {

        }*/
    }
}

