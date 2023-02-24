using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Movement
{
    public abstract class MovementBase : MonoBehaviour
    {
        public abstract event Action OnMovementFinished;

        public abstract IEnumerator Moving();
    }
}
