using UnityEngine;

namespace Shooter
{
    public struct InfoCollision
    {
        private readonly Vector3 _dir;
        private readonly float _damage;

        public InfoCollision(float damage, Vector3 dir = default(Vector3))
        {
            _damage = damage;
            _dir = dir;
        }

        public Vector3 Dir => _dir;

        public float Damage => _damage;
    }
}