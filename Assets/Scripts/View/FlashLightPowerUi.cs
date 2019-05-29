using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    public class FlashLightPowerUi : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public float FillAmount
        {
            set => _image.fillAmount = 1.0f / FlashLightModel.BatteryChargeMax * value;
        }
    }
}
