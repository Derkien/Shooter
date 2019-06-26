using Shooter.Interface;
using UnityEngine;

namespace Shooter.Model
{
    public class HealthPack : BaseObjectScene, ISelectObj
    {
        public string GetMessage()
        {
            return gameObject.name;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
