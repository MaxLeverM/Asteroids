using System;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class LaserDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Starship starship;
        private Transform holder;
        private LaserGun laserGun;

        private void Start()
        {
            holder = starship.transform;
            laserGun = starship.AdditionalGun;
            laserGun.OnShotStatusChanged += ActivateLaser;
        }

        private void ActivateLaser(bool isActive)
        {
            lineRenderer.enabled = isActive;
            UpdateLinePositions();
        }

        private void FixedUpdate()
        {
            if(lineRenderer.enabled)
                UpdateLinePositions();
        }

        private void UpdateLinePositions()
        {
            lineRenderer.SetPosition(0, holder.position);
            lineRenderer.SetPosition(1, holder.position + (holder.up * laserGun.Distance));
        }
    }
}