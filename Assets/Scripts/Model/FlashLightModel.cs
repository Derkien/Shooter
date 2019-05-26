using UnityEngine;

namespace Shooter
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        public const float BatteryChargeMax = 10f;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;
        public float BatteryChargeCurrent { get; private set; }

        [SerializeField] private float _speed = 10;
        [SerializeField] private float _rechargeRate = 1.5f;

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vecOffset = transform.position - _goFollow.position;
            BatteryChargeCurrent = BatteryChargeMax;
        }

        public void Switch(bool value)
        {
            _light.enabled = value;
            if (!value)
            {
                return;
            }

            transform.position = _goFollow.position + _vecOffset;
            transform.rotation = _goFollow.rotation;
        }

        public void Rotation()
        {
            if (!_light)
            {
                return;
            }

            transform.position = _goFollow.position + _vecOffset;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;

                return true;
            }
            return false;
        }

        public void RechargeBattery()
        {
            if (BatteryChargeCurrent > BatteryChargeMax)
            {
                return;
            }

            BatteryChargeCurrent += _rechargeRate * Time.deltaTime;

            if (BatteryChargeCurrent >= BatteryChargeMax)
            {
                BatteryChargeCurrent = BatteryChargeMax;
            }
        }
    }
}