using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UniversalComponents
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [Tooltip("Target layers")]
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _onEnterAction;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;

            _onEnterAction?.Invoke(other.gameObject);
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}