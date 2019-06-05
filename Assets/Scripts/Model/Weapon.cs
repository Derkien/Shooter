using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public abstract class Weapon : BaseObjectScene
    {
        [SerializeField]
        private int _maxCountAmmunition = 40;
        [SerializeField]
        private int _countClip = 5;

        public Ammunition Ammunition;
        public Clip Clip;

        protected AmmunitionType[] _ammunitionType = { AmmunitionType.Bullet };

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _rechargeTime = 0.2f;
        private Queue<Clip> _clips = new Queue<Clip>();

        protected bool _isReady = true;

        private void Start()
        {
            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = _maxCountAmmunition });
            }

            ReloadClip();
        }

        public abstract void Fire();

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        public void ReloadClip()
        {
            if (CountClip <= 0)
            {
                return;
            }
            Clip = _clips.Dequeue();
        }

        public int CountClip => _clips.Count;
    }
}