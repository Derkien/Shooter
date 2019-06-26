using Shooter.Interface;
using Shooter.Model;
using Shooter.View;
using UnityEngine;

namespace Shooter.Controller
{
    public sealed class FlashLightController : BaseController, IOnUpdate, IInitialization
    {
        private FlashLightModel _flashLightModel;
        private FlashLightPowerUi _flashLightPowerUi;
        private FlashLightStateImageUi _flashLightStateImageUi;

        public void OnStart()
        {
            _flashLightModel = GameObject.FindObjectOfType<FlashLightModel>();

            _flashLightPowerUi = UiInterface.FlashLightPowerUi;
            _flashLightStateImageUi = UiInterface.FlashLightStateImageUi;
        }

        public override void On()
        {
            if (IsActive)
            {
                return;
            }
            if (_flashLightModel == null)
            {
                return;
            }

            if (_flashLightPowerUi == null)
            {
                return;
            }

            if (_flashLightStateImageUi == null)
            {
                return;
            }

            if (_flashLightModel.BatteryChargeCurrent <= 0)
            {
                return;
            }

            base.On();
            _flashLightModel.Switch(true);
            _flashLightStateImageUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive)
            {
                return;
            }
            base.Off();
            _flashLightModel.Switch(false);
            _flashLightStateImageUi.SetActive(false);
        }

        public void OnUpdate()
        {
            _flashLightPowerUi.FillAmount = _flashLightModel.BatteryChargeCurrent;

            if (!IsActive)
            {
                _flashLightModel.RechargeBattery();

                return;
            }

            _flashLightModel.Rotation();

            if (!_flashLightModel.EditBatteryCharge())
            {
                Off();
            }
        }
    }
}