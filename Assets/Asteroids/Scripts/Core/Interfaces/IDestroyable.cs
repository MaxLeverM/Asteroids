using System;
using UnityEngine;

namespace Asteroids.Scripts.Core.Interfaces
{
    public interface IDestroyable
    {
        public Action<GameObject> DestroyCalled { get; set; }
        public void CallDestroy();
    }
}