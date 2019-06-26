using Shooter.Controller;
using Shooter.Interface;
using UnityEngine;

namespace Shooter
{
    public class InputController : BaseController, IOnUpdate
    {

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _savePlayer = KeyCode.C;
        private KeyCode _loadPlayer = KeyCode.V;
        private int _currentWeapon = 0;

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnUpdate()
        {
            if (!IsActive)
            {
                return;
            }

            if (Input.GetKeyDown(_activeFlashLight))
            {
                Main.Instance.FlashLightController.Switch();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_currentWeapon + 1 < Main.Instance.ObjectManager.Weapons.Length)
                {
                    _currentWeapon++;
                }
                else
                {
                    _currentWeapon = 0;
                }

                SelectWeapon(_currentWeapon);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {

                if (_currentWeapon - 1 > 0)
                {
                    _currentWeapon--;
                }
                else
                {
                    _currentWeapon = Main.Instance.ObjectManager.Weapons.Length - 1;
                }

                SelectWeapon(_currentWeapon);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }

            if (Input.GetKeyDown(_cancel))
            {
                Main.Instance.WeaponController.Off();
                Main.Instance.FlashLightController.Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                Main.Instance.WeaponController.ReloadClip();
            }

            if (Input.GetKeyDown(_savePlayer))
            {
                Main.Instance.SaveDataRepository.Save();
            }

            if (Input.GetKeyDown(_loadPlayer))
            {
                Main.Instance.SaveDataRepository.Load();
            }
        }


        /// <summary>
        /// Выбор оружия
        /// </summary>
        /// <param name="i">Номер оружия</param>
        ///<exception cref="System.NullReferenceException"></exception>
        private void SelectWeapon(int i)
        {
            Main.Instance.WeaponController.Off();
            var tempWeapon = Main.Instance.ObjectManager.Weapons[i];
            if (tempWeapon != null)
            {
                Main.Instance.WeaponController.On(tempWeapon);
            }
        }
    }
}