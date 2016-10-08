using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Stretcher: MonoBehaviour {

		// Puuttuvia toiminnallisuuksia (poista kun done):
		// _stretchVectorin lähetys Slingerille
		// Minimin alituksesta heiton cancelointi ja muutenkin täysin eri toiminto
		// Stretchin sallinta vain kun on maassa
		// Stretchin sallinta vain kun on aktiivi
		// Vuoron heitto eteenpäin sitten kun sallittu heitto tehty

		private Transform _gcTransform;
		private Vector2 _slugPosition;
		private Vector2 _mousePos;
		private bool _clickedOn;

		// These values might/should be determined by the character type.
		// Can be done straight to the prefab though?
		[SerializeField] private float _maxStretch = 2.0f;
		[SerializeField] private float _minStretch = 0.8f;
		private Vector2 _stretchVector;
		private Slinger _slinger;

		private CharacterAnimator _charAnim;
		private ArrowAnimator _arrowAnim;

		// Onko Slug paras nimi tälle scriptille?
		private Slug _slug;

		private void Awake(){
			_gcTransform = GetComponent<Transform> ();
			_slinger = GetComponent<Slinger> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_charAnim.SetToIdle ();
			_arrowAnim = FindObjectOfType<ArrowAnimator> ();
			_slug = GetComponent<Slug> ();

			/*	Ehkä tällainen pitäis tehdä, ellei prefabissa vain määritä
			 * _maxStretch = //Hahmotyypin määrittämä maksimi
			 * _minStretch = //Hahmotyypin määrittämä minimi
			 * 
			 */
		}
			
		void Update () {
			if (_clickedOn) {
				Stretch ();
			}
		}

		void OnMouseDown(){
			if (_slug.IsActive) {
				_clickedOn = true;
				_charAnim.SetToStretch ();
				SoundController.Instance.PlaySoundByIndex (0, _gcTransform.position);
			}
		}

		void OnMouseUp(){
			if (_clickedOn) {
				_clickedOn = false;
				_arrowAnim.HideAll ();


				//Play slingshot sound:
				//SoundController.Instance.PlaySoundByIndex (SOMETHING, _gcTransform.position); 
				//Play animal shout sound, determined by character type (!!!):
				//_slinger.Sling();
				SoundController.Instance.PlaySoundByIndex (1, _gcTransform.position);

			}

		}

		void Stretch(){

			_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
			// Get Vector2 value from the character's position:
			_slugPosition = _gcTransform.position;

			_stretchVector = _slugPosition - _mousePos; 

			float stretchAngle = Mathf.Atan2(_stretchVector.y, _stretchVector.x) * Mathf.Rad2Deg;
			Quaternion tailRotation = Quaternion.AngleAxis(stretchAngle, Vector3.forward);
			_charAnim.RotateTail (tailRotation);

			if (_stretchVector.magnitude > _minStretch) {
				if (_stretchVector.magnitude < _maxStretch) {
					_charAnim.ScaleTail (_stretchVector.magnitude);
				} else {
					_charAnim.ScaleTail (_maxStretch);
				}
				_arrowAnim.SetArrowVisibility (_stretchVector.magnitude);
			} else {
				_charAnim.ScaleTail (_minStretch);
				_arrowAnim.HideAll ();
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