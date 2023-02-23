using Assets.Scripts.Data.Definitions;
using Assets.Scripts.Projectiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponId _weaponId;

        private int _damage;
        private float _shotInterval;
        private float _projectileSpeed;
        private Coroutine _coroutine;
        private bool _isFiring;
        private Vector3 _firingDirection;

        public bool IsFiring => _isFiring;

        private void Start()
        {
            LoadWeaponParameters(_weaponId);
        }

        public void StartFiring(Vector3 direction)
        {
            _firingDirection = direction;

            if (_coroutine != null)
                return;

            _isFiring = true;
            _coroutine = StartCoroutine(FiringRoutine());
        }

        public void StopFiring()
        {
            if (_coroutine == null)
                return;

            _isFiring = false;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator FiringRoutine()
        {
            while (_isFiring)
            {
                Fire();
                yield return new WaitForSeconds(_shotInterval);
            }
        }

        private void Fire()
        {
            Projectile projectile = ProjectilePool.Instance.Pool.Get();
            projectile.transform.position = transform.position;
            projectile.Launch(_firingDirection, _projectileSpeed, _damage);
        }

        private void LoadWeaponParameters(WeaponId weaponId)
        {
            WeaponDef weaponDef = DefsFacade.I.Weapons.Get(weaponId);
            _damage = weaponDef.Damage;
            _shotInterval = weaponDef.ShotInterval;
            _projectileSpeed = weaponDef.ProjectileSpeed;
        }

        public void SwitchWeapon()
        {
            _weaponId += 1;
            LoadWeaponParameters(_weaponId);
        }
    }
}