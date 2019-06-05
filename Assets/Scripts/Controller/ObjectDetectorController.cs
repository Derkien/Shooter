using UnityEngine;

namespace Shooter
{
    public class ObjectDetectorController : BaseController, IOnUpdate, IInitialization
    {
        private readonly Camera _camera;
        private readonly Vector3 _vector;
        private const float _maxDistance = 20f;
        private ObjectAtLineOfSightNameUi _objectAtLineOfSightNameUi;

        public ObjectDetectorController(Camera camera)
        {
            _camera = camera;
            _vector = new Vector3(0.5f, 0.5f, 0f);
        }

        public void OnStart()
        {
            _objectAtLineOfSightNameUi = UiInterface.ObjectAtLineOfSightNameUi;
        }

        public void OnUpdate()
        {
            Ray ray = _camera.ViewportPointToRay(_vector);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            {
                Transform objectHit = hit.transform;

                var _selectedObj = objectHit.GetComponent<ISelectObj>();
                if (_selectedObj != null)
                {
                    _objectAtLineOfSightNameUi.FillText = _selectedObj.GetMessage();
                }
                else
                {
                    _objectAtLineOfSightNameUi.FillText = objectHit.name;
                }
            }
            else
            {
                _objectAtLineOfSightNameUi.FillText = string.Empty;
            }
        }
    }
}
