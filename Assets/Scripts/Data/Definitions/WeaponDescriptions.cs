using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Data.Definitions
{
    [CreateAssetMenu(menuName = "Defs/WeaponDescriptions", fileName = "Weapons")]
    public class WeaponDescriptions : ScriptableObject
    {
        [SerializeField] protected WeaponDef[] _collection;

        public WeaponDef Get(WeaponId id)
        {
            foreach (var weaponDef in _collection)
            {
                if (weaponDef.Id == id)
                    return weaponDef;
            }

            return default;
        }
    }
}