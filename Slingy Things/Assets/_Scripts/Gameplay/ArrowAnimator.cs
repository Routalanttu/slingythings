using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class ArrowAnimator : MonoBehaviour {

		private Transform _gcTransform;

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
			_gcTransform = GetComponent<Transform> ();
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

		/// <summary>
		/// Sets the arrow visibility to match stretch amount smoothly.
		/// </summary>
		/// <param name="magnitude">Stretch vector magnitude.</param>
		/// <param name="min">Minimum stretch.</param>
		/// <param name="max">Maximum stretch.</param>
		public void SetArrowVisibility (float magnitude, float min, float max) {
			for (int i = 0; i < _arrowSprites.Length; i++) {
				// Check if stretch is enough to allow arrow's visibility:
				if (magnitude > (min+(i*(max-min)/5f))) {
					// Tune arrow's visibility up according to stretch amount:
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
			_gcTransform.position = slugPosition;
			_gcTransform.rotation = rotation;
		}

		private void Expand (int i, float magnitude) {
			// Scale the arrow to match stretch volume:
			_arrows [i].localScale = new Vector3 (
				magnitude*(i+1)*0.15f,
				magnitude*(i+1)*0.15f,
				1f
			);

			// Increase distance from stretch center:
			_arrows [i].localPosition = new Vector3 (
				0.5f + magnitude*(i+1)*1.3f,
				_arrows[i].localPosition.y,
				_arrows[i].localPosition.z
			);
		}
	}
}
