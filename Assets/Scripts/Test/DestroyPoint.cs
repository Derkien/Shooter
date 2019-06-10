﻿using System;
using UnityEngine;

namespace Shooter
{
    public class DestroyPoint : MonoBehaviour
    {
        public event Action<GameObject> OnFinishChange;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bot>())
            {
                OnFinishChange?.Invoke(gameObject);
            }
        }
    }
}