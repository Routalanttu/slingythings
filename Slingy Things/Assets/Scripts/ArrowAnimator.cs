using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class ArrowAnimator : MonoBehaviour {

		// Juttuja joita ei vielä ole:
		// Sijainnin vaihto
		// Pitäiskö tän olla joku Instance/singleton/whatever?
		// Nuolien välisen välimatkan kasvu venytyksen mukaan

		private Transform _myTransform;
		[SerializeField] private Transform _arrowOne;
		[SerializeField] private Transform _arrowTwo;
		[SerializeField] private Transform _arrowThree;
		[SerializeField] private Transform _arrowFour;
		[SerializeField] private Transform _arrowFive;
		private Transform[] _arrows;
		private SpriteRenderer _arrowOneSprite;
		private SpriteRenderer _arrowTwoSprite;
		private SpriteRenderer _arrowThreeSprite;
		private SpriteRenderer _arrowFourSprite;
		private SpriteRenderer _arrowFiveSprite;

		private void Awake() {
			_myTransform = GetComponent<Transform> ();
			_arrowOneSprite = _arrowOne.GetComponent<SpriteRenderer> ();
			_arrowTwoSprite = _arrowTwo.GetComponent<SpriteRenderer> ();
			_arrowThreeSprite = _arrowThree.GetComponent<SpriteRenderer> ();
			_arrowFourSprite = _arrowFour.GetComponent<SpriteRenderer> ();
			_arrowFiveSprite = _arrowFive.GetComponent<SpriteRenderer> ();
			_arrows = new Transform[5];
			_arrows [0] = _arrowOne;
			_arrows [1] = _arrowTwo;
			_arrows [2] = _arrowThree;
			_arrows [3] = _arrowFour;
			_arrows [4] = _arrowFive;
		}

		/*
		public void SetArrowVisibility (float magnitude) {
			if (magnitude > 1f) {
				_arrowOneSprite.enabled = true;
				Color tmp = _arrowOneSprite.color;
				tmp.a = (magnitude - 1f)*5f;
				_arrowOneSprite.color = tmp;
			} else {
				_arrowOneSprite.enabled = false;
			}
			if (magnitude > 1.2f) {
				_arrowTwoSprite.enabled = true;
				Color tmp = _arrowTwoSprite.color;
				tmp.a = (magnitude - 1.2f)*5f;
				_arrowTwoSprite.color = tmp;
			} else {
				_arrowTwoSprite.enabled = false;
			}
			if (magnitude > 1.4f) {
				_arrowThreeSprite.enabled = true;
				Color tmp = _arrowThreeSprite.color;
				tmp.a = (magnitude - 1.4f)*5f;
				_arrowThreeSprite.color = tmp;
			} else {
				_arrowThreeSprite.enabled = false;
			}
			if (magnitude > 1.6f) {
				_arrowFourSprite.enabled = true;
				Color tmp = _arrowFourSprite.color;
				tmp.a = (magnitude - 1.6f)*5f;
				_arrowFourSprite.color = tmp;
			} else {
				_arrowFourSprite.enabled = false;
			}
			if (magnitude > 1.8f) {
				_arrowFiveSprite.enabled = true;
				Color tmp = _arrowFiveSprite.color;
				tmp.a = (magnitude - 1.8f)*5f;
				_arrowFiveSprite.color = tmp;
			} else {
				_arrowFiveSprite.enabled = false;
			}
		}
		*/

		public void SetArrowVisibility (float magnitude, float min, float max) {
			if (magnitude > min) {
				_arrowOneSprite.enabled = true;
				Color tmp = _arrowOneSprite.color;
				tmp.a = (magnitude - min)*5f;
				_arrowOneSprite.color = tmp;
			} else {
				_arrowOneSprite.enabled = false;
			}
			if (magnitude > (min+((max-min)/5f))) {
				_arrowTwoSprite.enabled = true;
				Color tmp = _arrowTwoSprite.color;
				tmp.a = (magnitude - (min+((max-min)/5f)))*5f;
				_arrowTwoSprite.color = tmp;
			} else {
				_arrowTwoSprite.enabled = false;
			}
			if (magnitude > (min+(2f*(max-min)/5f))) {
				_arrowThreeSprite.enabled = true;
				Color tmp = _arrowThreeSprite.color;
				tmp.a = (magnitude - (min+(2f*(max-min)/5f)))*5f;
				_arrowThreeSprite.color = tmp;
			} else {
				_arrowThreeSprite.enabled = false;
			}
			if (magnitude > (min+(3f*(max-min)/5f))) {
				_arrowFourSprite.enabled = true;
				Color tmp = _arrowFourSprite.color;
				tmp.a = (magnitude - (min+(3f*(max-min)/5f)))*5f;
				_arrowFourSprite.color = tmp;
			} else {
				_arrowFourSprite.enabled = false;
			}
			if (magnitude > (min+(4f*(max-min)/5f))) {
				_arrowFiveSprite.enabled = true;
				Color tmp = _arrowFiveSprite.color;
				tmp.a = (magnitude - (min+(4f*(max-min)/5f)))*5f;
				_arrowFiveSprite.color = tmp;
			} else {
				_arrowFiveSprite.enabled = false;
			}

			Expand (magnitude);
		}

		public void HideAll () {
			_arrowOneSprite.enabled = false;
			_arrowTwoSprite.enabled = false;
			_arrowThreeSprite.enabled = false;
			_arrowFourSprite.enabled = false;
			_arrowFiveSprite.enabled = false;
		}

		public void Rotate (Quaternion rotation, Vector3 slugPosition) {
			_myTransform.position = slugPosition;
			_myTransform.rotation = rotation;
		}

		private void Expand (float magnitude) {

			for (int i = 0; i < _arrows.Length; i++) {
				_arrows [i].localScale = new Vector3 (
					magnitude*(i+1)*0.6f,
					magnitude*(i+1)*0.6f,
					1f
				);
			}

			for (int i = 0; i < _arrows.Length; i++) {
				_arrows [i].localPosition = new Vector3 (
					0.5f + magnitude*(i+1)*0.6f,
					_arrows[i].localPosition.y,
					_arrows[i].localPosition.z
				);
			}

		}
	}
}
