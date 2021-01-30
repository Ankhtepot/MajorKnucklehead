using System;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class AmmoManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private List<Projectile> projectiles;

        public Projectile CurrentProjectile => currentProjectile;
        private Projectile currentProjectile;
#pragma warning restore 649

        private void Awake()
        {
            currentProjectile = projectiles[0];
        }
    }
}
