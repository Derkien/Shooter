namespace Shooter.Model
{
    public sealed class Gun : Weapon
    {
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

            var particleSystem = GetParticleSystem();
            if (particleSystem)
            {
                particleSystem.Play();
            }

            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechargeTime);
        }
    }
}