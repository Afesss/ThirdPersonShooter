using System;
using UnityEngine;

namespace Hero
{
    public class AssaultLaser : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private RaycastHit _rayHit;
        private void Update()
        {
            lineRenderer.SetPosition(0, transform.position);
            Vector3 position;
            if (Physics.Raycast(transform.position, transform.forward, out _rayHit))
            {
                position = _rayHit.point;
            }
            else
            {
                position = transform.position + transform.forward * 20;
            }
            lineRenderer.SetPosition(1, position);
        }
    }
}
