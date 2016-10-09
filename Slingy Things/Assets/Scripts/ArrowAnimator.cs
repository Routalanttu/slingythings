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
		private SpriteRenderer[] _arrowSprites;

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
			_arrowSprites = new SpriteRenderer[5];
			_arrowSprites [0] = _arrowOneSprite;
			_arrowSprites [1] = _arrowTwoSprite;
			_arrowSprites [2] = _arrowThreeSprite;
			_arrowSprites [3] = _arrowFourSprite;
			_arrowSprites [4] = _arrowFiveSprite;

		}

		public void SetArrowVisibility (float magnitude, float min, float max) {

			for (int i = 0; i < _arrowSprites.Length; i++) {
				if (magnitude > (min+(i*(max-min)/5f))) {
					_arrowSprites[i].enabled = true;
					Color tmp = _arrowSprites[i].color;
					tmp.a = (magnitude - (min+(i*(max-min)/5f)))*5f;
					_arrowSprites[i].color = tmp;
				} else {
					_arrowSprites[i].enabled = false;
				}

				Expand (i, magnitude);
			}
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

		private void Expand (int i, float magnitude) {

			// Scale the arrow to match stretch volume:
			_arrows [i].localScale = new Vector3 (
				magnitude*(i+1)*0.6f,
				magnitude*(i+1)*0.6f,
				1f
			);

			// Increase distance from center:
			_arrows [i].localPosition = new Vector3 (
				0.5f + magnitude*(i+1)*0.6f,
				_arrows[i].localPosition.y,
				_arrows[i].localPosition.z
			);
		}
	}
}
