using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

namespace SlingySlugs
{
    public class CameraController : MonoBehaviour
    {

		//CAMERA HAS TO BE SET AT ORIGO!! OTHERWISE BORDER CHECK FAILS

        private float _aspectRatio;

        private float _defaultCamSizeX;
        private float _newCamSizeX;

        private float _defaultCamSizeY;
        private float _newCamSizeY;

		private float _borderSettingX;
		private float _borderSettingY;

        private Camera _gcCam;
        private Transform _gcTransform;

        private float _posX;
        private float _posY;
        private float _posZ;

		private float _defaultPosX;
		private float _defaultPosY;
		private float _defaultPosZ;

		private bool _moveback; 

        //for following a target 
		public float _dampingSpeed = 0.02F; 
        public Transform _target;
		private bool _following; 

		private float _startingZoomTimer; 
		private bool _startingZoomDone; 
		private bool _startedZoomOut; 

		//zoom stuff
		Vector2?[] oldTouchPositions = {
			null,
			null
		};
		Vector2 oldTouchVector;
		float oldTouchDistance;

		private bool _zooming; 
		private bool _zoomOuting; 
		private bool _startedZoomIn;
		private float _zoomPre; 
		private float _zoomOutPre;
		private float _zoomTargetIn = 20f; 
		private float _zoomTargetOut = 25f;
		private float _elapsed = 0f;
		private bool _playerIsControllingCamera; 
		private float _touchTimer;

        // Use this for initialization
        void Start()
        {

            _gcCam = GetComponent<Camera>();
            _gcTransform = GetComponent<Transform>();

            _aspectRatio = (float)_gcCam.pixelWidth / (float)_gcCam.pixelHeight;
            _defaultCamSizeX = _gcCam.orthographicSize * _aspectRatio;
            _defaultCamSizeY = _gcCam.orthographicSize; //orthographic size is the camera Y size 

			_gcTransform.position = new Vector3 (0, 0, -10); 

        }

        // Update is called once per frame
        void Update()
        {
			

        }

		void LateUpdate(){

			StartingZoom (); 

			if (!GameManager.Instance.CharacterTouched)
			{
				TouchCam (); 

			}

			if (_target != null && _startingZoomDone && !_playerIsControllingCamera)
			{

				if (_following) {
					FollowTarget (); 
				}

				if (_zooming && !_zoomOuting) {
					ZoomToTarget (); 
				} else if(_zoomOuting && !_zooming) {
					ZoomOut (); 
				}

			}

			CheckBorders();

		}

		private void FollowTarget(){
			_gcTransform.position = Vector3.Lerp(_gcTransform.position, _target.position, _dampingSpeed) + new Vector3(0, 0.0f, -10);

		}

		private void ZoomToTarget(){
			_zoomOuting = false; 
			_startedZoomOut = false; 

			if (!_startedZoomIn) {
				_elapsed = 0; 
				_startedZoomIn = true; 
				_zoomPre = _gcCam.orthographicSize;
			}

			_elapsed += Time.deltaTime/2;

			_gcCam.orthographicSize = Mathf.Lerp(_zoomPre, _zoomTargetIn, _elapsed);
		}

		public void StartZoomOut(){
			_zoomOuting = true; 
			_zooming = false;
		}

		//when slinged this method is called
		public void ZoomOut(){
			_zooming = false; 
			_following = true; 

			if (!_startedZoomOut) {
				_elapsed = 0; 
				_startedZoomOut = true; 
				_zoomOutPre = _gcCam.orthographicSize;
			}
			_elapsed += Time.deltaTime/2;
			_gcCam.orthographicSize = Mathf.Lerp(_zoomOutPre, _zoomTargetOut, _elapsed);
		}


		private void TouchCam(){
			if (Input.touchCount == 0) {
				_playerIsControllingCamera = false; 
				oldTouchPositions[0] = null;
				oldTouchPositions[1] = null;
				_touchTimer = 0; 
			}
			else if (Input.touchCount == 1) {
				_playerIsControllingCamera = true; 


				//if player touches something else than a character for more than a second, he gains control of camera
				_touchTimer += Time.deltaTime; 

				if (!GameManager.Instance.CharacterTouched && _touchTimer>0.2f) {
					_following = false; 
				}


				if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
					oldTouchPositions[0] = Input.GetTouch(0).position;
					oldTouchPositions[1] = null;
				}
				else {
					Vector2 newTouchPosition = Input.GetTouch(0).position;

					transform.position += _gcTransform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * _gcCam.orthographicSize / _gcCam.pixelHeight * 2f));

					oldTouchPositions[0] = newTouchPosition;
				}
			}
			else {
				_playerIsControllingCamera = true; 
				if (oldTouchPositions[1] == null) {
					oldTouchPositions[0] = Input.GetTouch(0).position;
					oldTouchPositions[1] = Input.GetTouch(1).position;
					oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
					oldTouchDistance = oldTouchVector.magnitude;
				}
				else {
					Vector2 screen = new Vector2(_gcCam.pixelWidth, _gcCam.pixelHeight);

					Vector2[] newTouchPositions = {
						Input.GetTouch(0).position,
						Input.GetTouch(1).position
					};
					Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
					float newTouchDistance = newTouchVector.magnitude;

					_gcTransform.position += _gcTransform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * _gcCam.orthographicSize / screen.y));
					_gcCam.orthographicSize *= oldTouchDistance / newTouchDistance;
					_gcTransform.position -= _gcTransform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * _gcCam.orthographicSize / screen.y);

					oldTouchPositions[0] = newTouchPositions[0];
					oldTouchPositions[1] = newTouchPositions[1];
					oldTouchVector = newTouchVector;
					oldTouchDistance = newTouchDistance;

					//player gains control of zoom
					if (!GameManager.Instance.CharacterTouched) {
						_zooming = false; 
						_zoomOuting = false; 
						_startedZoomIn = false;
						_startedZoomOut = false;
					}
				}
			}

			//set max and min zoom 
			_gcCam.orthographicSize = Mathf.Max(_gcCam.orthographicSize, _defaultCamSizeY / 3);
			_gcCam.orthographicSize = Mathf.Min(_gcCam.orthographicSize, _defaultCamSizeY);

		}

        public void SetCameraTarget(Transform target)
        {

			_playerIsControllingCamera = false; 

            if (target != null)
            {
                _target = target;
            }

			_following = true; 
			_zooming = true; 
			_zoomOuting = false; 
			_startedZoomIn = false;
			_startedZoomOut = false; 

			Debug.Log ("SET CAMERA TARGET"); 

		}

		private void StartingZoom(){

			if (_startingZoomTimer < 3) {
				_gcCam.orthographicSize -= Time.deltaTime * 8; 
				_gcCam.transform.Translate (Vector3.down * Time.deltaTime * 6f);
				_startingZoomTimer += Time.deltaTime;  
			} else if(!_startingZoomDone) {
				_zoomPre = _gcCam.orthographicSize; 
				_startingZoomDone = true; 
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
				_moveback = true; 
            }

            if (_posY > _borderSettingY)
            {
                _posY = _borderSettingY;
				_moveback = true; 
            }

            if (_posX < -_borderSettingX)
            {
                _posX = -_borderSettingX;
				_moveback = true; 
            }

            if (_posX > _borderSettingX)
            {
                _posX = _borderSettingX;
				_moveback = true; 
            }

            //if position goes over border, move back to border edges
			if (_moveback) {
				_gcTransform.position = new Vector3(_posX, _posY, _posZ);
			}
            
        }

    }


}

