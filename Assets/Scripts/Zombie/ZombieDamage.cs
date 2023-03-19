using System;
using Opsive.UltimateCharacterController.Traits;
using Opsive.UltimateCharacterController.Traits.Damage;
using UnityEngine;

namespace Zombie
{
    public class ZombieDamage : MonoBehaviour
    {
        [SerializeField] private Transform damagePoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float damageRadius;
        [SerializeField] private int damageCount;

        private readonly Collider[] _colliders = new Collider[1];
        
        public void Damage()
        {
            var size = Physics.OverlapSphereNonAlloc(damagePoint.position, damageRadius, _colliders, layerMask);

            if(size == 0)
                return;
            
            foreach (Collider collider in _colliders)
            {
                var damageTarget = collider.GetComponentInParent<IDamageTarget>();
                
                if (damageTarget != null)
                {
                    var damageData = new DamageData();
                    damageData.Amount = damageCount;
                    damageTarget.Damage(damageData);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (damagePoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(damagePoint.position, damageRadius);
            }
        }
    }
}
