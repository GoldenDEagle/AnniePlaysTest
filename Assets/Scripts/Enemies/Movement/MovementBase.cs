using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.Movement
{
    public abstract class MovementBase : MonoBehaviour
    {
        public abstract event Action OnMovementFinished;

        public abstract IEnumerator Moving();


        // get random point on navmesh within radius
        protected Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere * radius;
            randomDirection.y = 0f;
            randomDirection += transform.position;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
    }
}
