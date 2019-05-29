using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    public class ObjectAtLineOfSightNameUi : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public string FillText
        {
            set {  
                if (value == string.Empty)
                {
                    _text.text = value;
                }
                else
                {
                    _text.text = $"You see: " + char.ToUpper(value[0]) + value.Substring(1);
                }
            }
        }
    }
}
