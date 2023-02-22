using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Collisions
{
    public class EnterTriggerEffect : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;

            _action?.Invoke(other.gameObject);
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}