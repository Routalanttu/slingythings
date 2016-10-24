using UnityEngine;
using System.Collections;

namespace SlingySlugs
{
    public class CameraController : MonoBehaviour
    {

        public float _zoomSpeed = 0.02f;
        public float _swipeSpeed = 0.1F;

        private float _aspectRatio;

        private float _defaultCamSizeX;
        private float _newCamSizeX;

        private float _defaultCamSizeY;
        private float _newCamSizeY;

        private float _borderSettingX;
        private float _borderSettingY;

        private Camera _gcCam;
        private Transform _gcTransform;

        private Vector2 _oldTouchPosition;
        private Vector2 _newTouchPosition;

        private float _posX;
        private float _posY;
        private float _posZ;

        //for following a target 
        public float dampingSpeed;
        public Transform _target;
        Transform _transform;

      


        // Use this for initialization
        void Start()
        {

            _gcCam = GetComponent<Camera>();
            _gcTransform = GetComponent<Transform>();

            _aspectRatio = (float)_gcCam.pixelWidth / (float)_gcCam.pixelHeight;
            _defaultCamSizeX = _gcCam.orthographicSize * _aspectRatio;
            _defaultCamSizeY = _gcCam.orthographicSize; //orthographic size is the camera Y size   

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.touchCount == 0)
            {
                _oldTouchPosition = Vector2.zero;
            }

            if (!GameManager.Instance.CharacterTouched)
            {

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    MoveCamera();
                }

                if (Input.touchCount == 2)
                {
                    MoveCamera();
                    PinchZoom();
                }
            }

            if (_target != null && GameManager.Instance.CharacterTouched)
            {
                _transform.position = Vector3.Lerp(_transform.position, _target.position, dampingSpeed) + new Vector3(0, 0.0f, -10);
            }

            CheckBorders();

        }

        public void SetCameraTarget(Transform target)
        {

            if (target != null)
            {
                _target = target;
            }

        }

        private void CheckBorders()
        {
            //Get current position
            _posX = _gcTransform.position.x;
            _posY = _gcTransform.position.y;
            _posZ = _gcTransform.position.z;

            _newCamSizeX = _gcCam.orthographicSize * _aspectRatio;
            _newCamSizeY = _gcCam.orthographicSize;

            //Get change of border 
            float borderChangeX = (_defaultCamSizeX - _newCamSizeX);
            float borderChangeY = (_defaultCamSizeY - _newCamSizeY);

            _borderSettingX = borderChangeX;
            _borderSettingY = borderChangeY;


            if (_posY < -_borderSettingY)
            {
                _posY = -_borderSettingY;
            }

            if (_posY > _borderSettingY)
            {
                _posY = _borderSettingY;
            }

            if (_posX < -_borderSettingX)
            {
                _posX = -_borderSettingX;
            }

            if (_posX > _borderSettingX)
            {
                _posX = _borderSettingX;
            }

            //if position goes over border, move back to border edges
            _gcTransform.position = new Vector3(_posX, _posY, _posZ);
        }

        void MoveCamera()
        {

            if (_oldTouchPosition == Vector2.zero)
            {
                _oldTouchPosition = Input.GetTouch(0).position;
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;

                transform.position += transform.TransformDirection((Vector2)((_oldTouchPosition - newTouchPosition) * _gcCam.orthographicSize / _gcCam.pixelHeight * 2f));

                _oldTouchPosition = newTouchPosition;
            }

        }

        void PinchZoom()
        {

            //get touch information 
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            //get the positions of the 2 touches in the previous frame 
            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            //get the difference in magnitude (between previous frame and this frame) of the vector between the touch points
            //if difference is negative, the user is zooming out (pinching closer), and if positive, user is zooming in (pinching away)
            float prevTouchDeltaMagnitude = (secondTouchPrevPos - firstTouchPrevPos).magnitude;
            float touchDeltaMagnitude = (secondTouch.position - firstTouch.position).magnitude;
            float magnitudeDifference = prevTouchDeltaMagnitude - touchDeltaMagnitude;

            //change camera size 
            _gcCam.orthographicSize += magnitudeDifference * _zoomSpeed;

            //set max and min zoom 
            _gcCam.orthographicSize = Mathf.Max(_gcCam.orthographicSize, _defaultCamSizeY / 3);
            _gcCam.orthographicSize = Mathf.Min(_gcCam.orthographicSize, _defaultCamSizeY);
        }
    }


}

