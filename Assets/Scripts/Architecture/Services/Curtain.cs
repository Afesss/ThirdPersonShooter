using UnityEngine;

namespace Architecture.Services
{
    public class Curtain : MonoBehaviour
    {
        public void ShowCurtain()
        {
            gameObject.SetActive(true);
        }

        public void HideCurtain()
        {
            gameObject.SetActive(false);
        }
    }
}
