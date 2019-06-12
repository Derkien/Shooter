using UnityEngine;

namespace Shooter
{
    public sealed class ShotGunBullet : Ammunition
    {
        public AmmunitionType Type = AmmunitionType.ShotGunBullet;

        private void OnCollisionEnter(Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, collision.contacts[0], collision.transform,
                    Rigidbody.velocity));
            }

            if (collision.gameObject.GetComponent<ShotGunBullet>() == null)
            {
                Destroy(gameObject);
            }
        }
    }
}