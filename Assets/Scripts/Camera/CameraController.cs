using UnityEngine;

namespace IndustrRazvlProj
{
    /// <summary>
    /// Controls Rect and ViewSize of main Camera
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _arenaArea;
        [SerializeField] private Vector3 _cameraOffset = new Vector3(0, 0, -10);
        [SerializeField] private Vector2 _defaultRectSize = new Vector2(1, 1);
        [SerializeField] private Vector2 _rectCenter = new Vector2(0.5f, 0.5f);
        private float _targetRatio;
        private float _previousRatio;

        private void Start()
        {
            _targetRatio = _arenaArea.localScale.x / _arenaArea.localScale.y;
            SetCameraPos();
            SetCamera();
        }

        private void FixedUpdate()
        {
            float currentRario = (float)Screen.width / (float)Screen.height;
            if (_previousRatio != currentRario)
            {
                SetCamera();
            }
        }

        private void SetCameraPos()
        {
            Camera.main.transform.position = _arenaArea.position + _cameraOffset;
        }

        public void SetCamera()
        {
            _previousRatio = (float)Screen.width / (float)Screen.height;

            if (_previousRatio > _targetRatio) // If Screen wider then arena area
            {
                // Sets view size
                float camViewSize = _arenaArea.localScale.y / 2;
                Camera.main.orthographicSize = camViewSize;
                // Sets Rect
                float pixelScale = Screen.height / _arenaArea.localScale.y;
                float targetWidth = pixelScale * _arenaArea.localScale.x;
                float relativeWidth = targetWidth / Screen.width;
                Vector2 rectSize = new Vector2(relativeWidth, _defaultRectSize.y);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
            }
            else // If screen higher then arena area
            {
                // Sets view size
                float relativeViewSize = _arenaArea.localScale.y / (_arenaArea.localScale.x / (_arenaArea.localScale.x / 2));
                Camera.main.orthographicSize = relativeViewSize;
                // Sets Rect
                float pixelScale = Screen.width / (_arenaArea.localScale.x);
                float targetHeight = pixelScale * (_arenaArea.localScale.y);
                float relativeHeight = targetHeight / Screen.height;
                Vector2 rectSize = new Vector2(_defaultRectSize.x, relativeHeight);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
            }
        }
    }
}