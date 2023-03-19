using UnityEngine;

namespace Hero
{
    public class FootStepAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnTriggerEnter(Collider other)
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }
}
