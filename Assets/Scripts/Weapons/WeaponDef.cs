using Assets.Scripts.Projectiles;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class WeaponDef
    {
        [SerializeField] private string _name;
        [SerializeField] private WeaponId _id;
        [SerializeField] private int _damage;
        [SerializeField] private float _shotInterval;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Projectile _projectilePrefab;

        public string Name => _name;
        public WeaponId Id => _id;
        public int Damage => _damage;
        public float ShotInterval => _shotInterval;
        public float ProjectileSpeed => _projectileSpeed;
        public Projectile ProjectilePrefab => _projectilePrefab;
    }

    public enum WeaponId
    {
        MachineGun = 0,
        MissileLauncher = 1
    }
}
