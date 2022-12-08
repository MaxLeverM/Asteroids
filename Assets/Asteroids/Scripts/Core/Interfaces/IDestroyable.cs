using System;
using UnityEngine;

namespace Asteroids.Scripts.Core.Interfaces
{
    public interface IDestroyable
    {
        public event Action<GameObject> DestroyCalled;
        public void CallDestroy();
    }
}