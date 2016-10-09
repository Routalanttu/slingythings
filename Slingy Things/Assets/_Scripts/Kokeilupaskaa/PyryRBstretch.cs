using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class PyryRBstretch: MonoBehaviour {

		public float _maxStretch = 2.0f; 

		private bool clickedOn;

		Vector2 _slugPosition;
		Vector2 _mousePos;
		Transform _gcTransform;

		[SerializeField] private AudioClip squish;

		private CharacterAnimator _charAnim;
		[SerializeField] private ArrowAnimator _arrowAnim;

		//private PyryRBsling _slinger;

		private void Awake(){
			_gcTransform = GetComponent<Transform> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();
			//_slinger = GetComponent<PyryRBsling> ();
		}

		// Update is called once per frame
		void Update () {
			if (clickedOn) {
				Dragging ();
				_charAnim.SetToStretch ();
			}
		}

		void OnMouseDown(){
			clickedOn = true;
			SoundController.Instance.PlaySoundByIndex (0, _gcTransform.position); 
		}

		void OnMouseUp(){
			clickedOn = false;
			//SoundController.Instance.PlaySoundByIndex (0, _gcTransform.position); 
			SoundController.Instance.PlaySoundByIndex (1, _gcTransform.position); 
			_arrowAnim.HideAll ();
		}

		void Dragging(){

			_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
			_slugPosition = _gcTransform.position; //have to use the vector2 form for below

			Vector2 vectorToTarget = _slugPosition - _mousePos; 

			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			float angleInRad = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			_charAnim.RotateTail (q);


			if (vectorToTarget.magnitude > 0.8f) {
				if (vectorToTarget.magnitude < _maxStretch) {
					_charAnim.ScaleTail (vectorToTarget.magnitude);
				} else {
					_charAnim.ScaleTail (_maxStretch);
				}

				//_arrowAnim.SetArrowVisibility (vectorToTarget.magnitude);
			} else {
				_charAnim.ScaleTail (0.8f);
				//_arrowAnim.HideAll ();
			}

			if (vectorToTarget.x < 0f) {
				_charAnim.StretchFlip (true);
			} else if (vectorToTarget.x > 0f) {
				_charAnim.StretchFlip (false);
			}

		}




	}

}