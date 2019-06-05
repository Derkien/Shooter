using UnityEngine;

namespace Shooter
{
    public sealed class ShotGun : Weapon
    {
        [Range(10, 20)]
        public int _spreadFactor = 14;
        [Range(5,15)]
        public int _shotsNumber = 5;

        public override void Fire()
        {
            if (!_isReady)
            {
                return;
            }
            if (Clip.CountAmmunition <= 0)
            {
                return;
            }
            if (!Ammunition)
            {
                return;
            }

            for (int i = 0; i < _shotsNumber; i++)
            {
                MakeRandomShot();
            }

            Clip.CountAmmunition--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechargeTime);
        }

        private void MakeRandomShot()
        {
            var randomNumberX = Random.Range(-_spreadFactor, _spreadFactor);
            var randomNumberY = Random.Range(-_spreadFactor, _spreadFactor);
            var randomNumberZ = Random.Range(-_spreadFactor, _spreadFactor);
            
            var bullet = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            bullet.transform.Rotate(randomNumberX, randomNumberY, 0);
            bullet.AddForce(bullet.transform.forward * _force);
        }
    }
}