using UnityEngine;

namespace Zombie
{
    [RequireComponent(typeof(Animator))]
    public class ZombieRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] rigidbodies;

        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        public void EnableRagdoll()
        {
            _animator.enabled = false;
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }
    }
}
