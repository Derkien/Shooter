using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    public class FlashLightStateImageUi : MonoBehaviour
    {
        [SerializeField] private Sprite _stageOffImage;
        [SerializeField] private Sprite _stageOnImage;

        private Image _currentStateImage;

        private void Awake()
        {
            _currentStateImage = GetComponent<Image>();
        }

        public void SetActive(bool value)
        {
            _currentStateImage.sprite = value == true ? _stageOnImage : _stageOffImage;
        }
    }
}
