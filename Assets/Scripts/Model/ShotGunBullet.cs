namespace Shooter
{
    public sealed class ShotGunBullet : Ammunition
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            if (collision.gameObject.GetComponent<ShotGunBullet>() == null)
            {
                Destroy(gameObject);
            }
        }
    }
}