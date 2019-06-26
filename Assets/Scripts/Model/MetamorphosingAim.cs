using Shooter.Helper;
using Shooter.Interface;
using System;
using UnityEngine;

namespace Shooter.Model
{
    public class MetamorphosingAim : MonoBehaviour, ISetDamage, ISelectObj
    {
        public event Action OnPointChange;

        public float Hp = 100;

        private bool _isDead;

        private MeshRenderer _meshRenderer;
        private Color _initialColor;
        private float _initialHp;

        private void Awake()
        {
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _initialColor = _meshRenderer.material.color;
            _initialHp = Hp;
        }

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

            _meshRenderer.material.color = Color.Lerp(Color.gray, _initialColor, Hp/_initialHp);

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