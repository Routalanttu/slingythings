using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

namespace SlingySlugs
{
    public class CameraController : MonoBehaviour
    {
		//CAMERA 
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

        //FOLLOW TARGET 
		public float _dampingSpeed = 0.05F; 
        public Transform _target;
		private bool _following; 

		private float _startingZoomTimer; 
		private bool _startingZoomDone; 
		private bool _startedZoomOut; 

		//ZOOMING
		Vector2?[] _oldTouchPositions = {
			null,
			null
		};
		Vector2 _oldTouchVector;
		float _oldTouchDistance;
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

		//Late update for smooth camera movement
		void LateUpdate(){

			if (!_startingZoomDone) {
				StartingZoom (); 
			}


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

		//Camera follows active character 
		private void FollowTarget(){
			_gcTransform.position = Vector3.Lerp(_gcTransform.position, _target.position, _dampingSpeed) + new Vector3(0, 0.0f, -10);

		}

		//Camera zooms in to active character 
		private void ZoomToTarget(){
			_zoomOuting = false; 
			_startedZoomOut = false; 

			if (!_startedZoomIn) {
				_elapsed = 0; 
				_startedZoomIn = true; 
				_zoomPre = _gcCam.orthographicSize;
			}

			_elapsed += Time.deltaTime*2;

			_gcCam.orthographicSize = Mathf.Lerp(_zoomPre, _zoomTargetIn, _elapsed);
		}

		public void StartZoomOut(){
			_zoomOuting = true; 
			_zooming = false;
		}

		//When character is slinged, camera zooms out
		public void ZoomOut(){
			_zooming = false; 
			//_following = true; 

			if (!_startedZoomOut) {
				_elapsed = 0; 
				_startedZoomOut = true; 
				_zoomOutPre = _gcCam.orthographicSize;
			}
			_elapsed += Time.deltaTime/2;
			_gcCam.orthographicSize = Mathf.Lerp(_zoomOutPre, _zoomTargetOut, _elapsed);
		}

		//User controlled camera 
		private void TouchCam(){
			if (Input.touchCount == 0) {
				_playerIsControllingCamera = false; 
				_oldTouchPositions[0] = null;
				_oldTouchPositions[1] = null;
				_touchTimer = 0; 
			}
			else if (Input.touchCount == 1) {
				_playerIsControllingCamera = true; 

				//if player touches something else than a character for more than x time, player gains control of camera
				_touchTimer += Time.deltaTime; 

				if (!GameManager.Instance.CharacterTouched && _touchTimer>0.10f) {
					_following = false; 
				}


				if (_oldTouchPositions[0] == null || _oldTouchPositions[1] != null) {
					_oldTouchPositions[0] = Input.GetTouch(0).position;
					_oldTouchPositions[1] = null;
				}
				else {
					Vector2 newTouchPosition = Input.GetTouch(0).position;

					transform.position += _gcTransform.TransformDirection((Vector3)((_oldTouchPositions[0] - newTouchPosition) * _gcCam.orthographicSize / _gcCam.pixelHeight * 2f));

					_oldTouchPositions[0] = newTouchPosition;
				}
			}
			else {
				_playerIsControllingCamera = true; 
				//player gains control of zoom
				if (!GameManager.Instance.CharacterTouched) {
					_following = false; 
					_zooming = false; 
					_zoomOuting = false; 
					_startedZoomIn = false;
					_startedZoomOut = false;
				}
					
				if (_oldTouchPositions[1] == null) {
					_oldTouchPositions[0] = Input.GetTouch(0).position;
					_oldTouchPositions[1] = Input.GetTouch(1).position;
					_oldTouchVector = (Vector2)(_oldTouchPositions[0] - _oldTouchPositions[1]);
					_oldTouchDistance = _oldTouchVector.magnitude;
				}
				else {
					Vector2 screen = new Vector2(_gcCam.pixelWidth, _gcCam.pixelHeight);

					Vector2[] newTouchPositions = {
						Input.GetTouch(0).position,
						Input.GetTouch(1).position
					};
					Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
					float newTouchDistance = newTouchVector.magnitude;

					_gcTransform.position += _gcTransform.TransformDirection((Vector3)((_oldTouchPositions[0] + _oldTouchPositions[1] - screen) * _gcCam.orthographicSize / screen.y));
					_gcCam.orthographicSize *= _oldTouchDistance / newTouchDistance;
					_gcTransform.position -= _gcTransform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * _gcCam.orthographicSize / screen.y);

					_oldTouchPositions[0] = newTouchPositions[0];
					_oldTouchPositions[1] = newTouchPositions[1];
					_oldTouchVector = newTouchVector;
					_oldTouchDistance = newTouchDistance;


				}
			}

			//set max and min zoom 
			_gcCam.orthographicSize = Mathf.Max(_gcCam.orthographicSize, _defaultCamSizeY / 4);
			_gcCam.orthographicSize = Mathf.Min(_gcCam.orthographicSize, _defaultCamSizeY);

		}

		//Set new target for camera follow
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

		}

		//Camera zooms in to level at the start of the game
		private void StartingZoom(){

			if (_startingZoomTimer < 2.5f) {
				_gcCam.orthographicSize -= Time.deltaTime * 10; 
				_gcCam.transform.Translate (Vector3.down * Time.deltaTime * 7f);
				_startingZoomTimer += Time.deltaTime;  
			} else if(!_startingZoomDone) {
				_zoomPre = _gcCam.orthographicSize; 
				_startingZoomDone = true; 
			}


		}

		//Restrict camera movement
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

