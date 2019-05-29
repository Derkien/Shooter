using UnityEngine;

namespace Shooter
{
    public class UiInterface
    {
        private FlashLightPowerUi _flashLightPowerUi;

        public FlashLightPowerUi FlashLightPowerUi
        {
            get
            {
                if (!_flashLightPowerUi)
                {
                    _flashLightPowerUi = Object.FindObjectOfType<FlashLightPowerUi>();
                }

                return _flashLightPowerUi;
            }
        }

        private FlashLightStateImageUi _flashLightStateImageUi;

        public FlashLightStateImageUi FlashLightStateImageUi
        {
            get
            {
                if (!_flashLightStateImageUi)
                {
                    _flashLightStateImageUi = Object.FindObjectOfType<FlashLightStateImageUi>();
                }

                return _flashLightStateImageUi;
            }
        }

        private WeaponUiText _weaponUiText;

        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                {
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                }

                return _weaponUiText;
            }
        }

        private ObjectAtLineOfSightNameUi _objectAtLineOfSightNameUi;

        public ObjectAtLineOfSightNameUi ObjectAtLineOfSightNameUi
        {
            get
            {
                if (!_objectAtLineOfSightNameUi)
                {
                    _objectAtLineOfSightNameUi = Object.FindObjectOfType<ObjectAtLineOfSightNameUi>();
                }

                return _objectAtLineOfSightNameUi;
            }
        }
    }
}