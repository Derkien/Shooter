using UnityEngine;

namespace Shooter
{
    public class ObjectManager : IInitialization
    {
        private Weapon[] _weapons = new Weapon[5];

        public Weapon[] Weapons => _weapons;

        public FlashLightModel FlashLight { get; private set; }

        public void OnStart()
        {
            _weapons = Main.Instance.Player.GetComponentsInChildren<Weapon>();

            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }

            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(false);
        }

        // Добавить функционал
    }
}