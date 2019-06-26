using Shooter.Helper;
using Shooter.Interface;
using System;
using UnityEngine;

namespace Shooter.Model
{
    public class Aim : MonoBehaviour, ISetDamage, ISelectObj
    {
        public event Action OnPointChange;

        public float Hp = 100;
        private bool _isDead;

        public void SetDamage(InfoCollision info)
        {
            if (_isDead)
            {
                return;
            }

            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                var tempRigidbody = GetComponent<Rigidbody>();
                if (!tempRigidbody)
                {
                    tempRigidbody = gameObject.AddComponent<Rigidbody>();
                }
                tempRigidbody.velocity = info.Dir;
                Destroy(gameObject, 10);

                OnPointChange?.Invoke();
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return gameObject.name;
        }
    }
}