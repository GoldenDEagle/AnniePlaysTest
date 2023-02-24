using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public interface IWeapon
    {
        public void StopFiring();
        public void StartFiring(Vector3 direction);
    }
}
