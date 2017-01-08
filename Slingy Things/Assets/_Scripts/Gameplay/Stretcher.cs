using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Stretcher: MonoBehaviour {

		private Transform _gcTransform;
		private Vector2 _slugPosition;
		private Vector2 _mousePos;
		private bool _clickedOn;

		private float _maxPowerStretch = 20.0f;
		private float _minPowerStretch = 0.8f;
		private float _maxVisualStretch = 2.0f;
		private float _minVisualStretch = 0.8f;
		private Vector2 _stretchVector;

		private CharacterAnimator _charAnim;
		private ArrowAnimator _arrowAnim;

		private CharacterInfo _slug;

		private float _stretchTimer; 

		private GameObject _circle;
		private SpriteRenderer _circleRenderer;

		private Color _tempCircleColor = Color.white;

		private void Awake(){
			_gcTransform = GetComponent<Transform> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();
			_arrowAnim = FindObjectOfType<ArrowAnimator> ();
			_slug = GetComponent<CharacterInfo> ();
			_circle = GameObject.FindGameObjectWithTag ("Circle");
			_circleRenderer = _circle.GetComponent<SpriteRenderer> ();
			_circleRenderer.enabled = false;
		}
			
		void Update () {
			if (_clickedOn) {
				Stretch ();
				if (_stretchTimer < 1f) {
					_stretchTimer += Time.deltaTime;
				}
				_tempCircleColor.g = _stretchTimer;
				_tempCircleColor.b = _stretchTimer;
				_tempCircleColor.a = _stretchTimer;
				_circleRenderer.color = _tempCircleColor;
				_circle.transform.localScale = new Vector3((1f - _stretchTimer),(1f - _stretchTimer),1f);
			}
		}

		void OnMouseDown(){
			if (_slug.IsActive) {
				_clickedOn = true;
				_charAnim.SetToStretch ();
				SoundController.Instance.PlaySoundByIndex (1);
                GameManager.Instance.CharacterTouched = true; 
				GameManager.Instance.SetCameraTarget (_gcTransform); 
				_stretchTimer = 0; 
				_circle.transform.position = _gcTransform.position;
				_circleRenderer.enabled = true;
			}
		}

		void OnMouseUp(){
			if (_clickedOn) {
                GameManager.Instance.CharacterTouched = false; 
				_clickedOn = false;
				_arrowAnim.HideAll ();

				// Sling if stretch is over minimum, otherwise cancel:
				if (_stretchVector.magnitude >= _minPowerStretch && _stretchTimer >= 0.4f) {
					// Determine which animal is in question and call the appropriate Sling:
					if (_slug.GetSpecies() == 0) {
						GetComponent<SlugSlinger> ().Sling (_stretchVector);
					} else if (_slug.GetSpecies() == 1) {
						GetComponent<SnailSlinger> ().Sling (_stretchVector);
					} else if (_slug.GetSpecies() == 2) {
						GetComponent<OctoSlinger> ().Sling (_stretchVector);
					} else if (_slug.GetSpecies() == 3) {
						GetComponent<SiikaSlinger> ().Sling (_stretchVector);
					}
				} else {
					_charAnim.SetToIdle ();
					SoundController.Instance.PlaySoundByIndex (1);
				}

				_circle.transform.localScale = new Vector3 (1f, 1f, 1f);
				_circleRenderer.color = Color.white;
				_circleRenderer.enabled = false;
			}

		}

		void Stretch(){
			_charAnim.SetToStretch ();

			_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
			// Get Vector2 value from the character's position:
			_slugPosition = _gcTransform.position;

			_stretchVector = _slugPosition - _mousePos; 
			_stretchVector = Vector2.ClampMagnitude (_stretchVector, _maxPowerStretch);

			float stretchAngle = Mathf.Atan2(_stretchVector.y, _stretchVector.x) * Mathf.Rad2Deg;
			Quaternion tailRotation = Quaternion.AngleAxis(stretchAngle, Vector3.forward);
			_charAnim.RotateTail (tailRotation);
			_arrowAnim.Rotate (tailRotation, _slugPosition);

			float scaledStretch = _minVisualStretch + _stretchVector.magnitude * ((_maxVisualStretch - _minVisualStretch) / _maxPowerStretch);

			// Stretch tail by player input, but disallow excessive shrinkage:
			if (_stretchVector.magnitude > _minPowerStretch) {
				_charAnim.ScaleTail (scaledStretch);
				_arrowAnim.SetArrowVisibility (scaledStretch, _minVisualStretch, _maxVisualStretch);
			} else {
				_charAnim.ScaleTail (_minVisualStretch);
				_arrowAnim.HideAll ();
				if (_stretchTimer >= 0.3f) {
					_stretchTimer = Mathf.Lerp (_stretchTimer, 0.3f, 0.1f);
				}
			}

			// Flip the character sprites to match the pointing direction:
			if (_stretchVector.x < 0f) {
				_charAnim.StretchFlip (true);
			} else if (_stretchVector.x > 0f) {
				_charAnim.StretchFlip (false);
			}
		}
	}
}