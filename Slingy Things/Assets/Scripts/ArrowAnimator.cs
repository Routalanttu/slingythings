using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class ArrowAnimator : MonoBehaviour {

		[SerializeField] private SpriteRenderer _arrowOne;
		[SerializeField] private SpriteRenderer _arrowTwo;
		[SerializeField] private SpriteRenderer _arrowThree;
		[SerializeField] private SpriteRenderer _arrowFour;
		[SerializeField] private SpriteRenderer _arrowFive;

		public void SetArrowVisibility (float magnitude) {
			if (magnitude > 1f) {
				_arrowOne.enabled = true;
				Color tmp = _arrowOne.color;
				tmp.a = (magnitude - 1f)*5f;
				_arrowOne.color = tmp;
			} else {
				_arrowOne.enabled = false;
			}
			if (magnitude > 1.2f) {
				_arrowTwo.enabled = true;
				Color tmp = _arrowTwo.color;
				tmp.a = (magnitude - 1.2f)*5f;
				_arrowTwo.color = tmp;
			} else {
				_arrowTwo.enabled = false;
			}
			if (magnitude > 1.4f) {
				_arrowThree.enabled = true;
				Color tmp = _arrowThree.color;
				tmp.a = (magnitude - 1.4f)*5f;
				_arrowThree.color = tmp;
			} else {
				_arrowThree.enabled = false;
			}
			if (magnitude > 1.6f) {
				_arrowFour.enabled = true;
				Color tmp = _arrowFour.color;
				tmp.a = (magnitude - 1.6f)*5f;
				_arrowFour.color = tmp;
			} else {
				_arrowFour.enabled = false;
			}
			if (magnitude > 1.8f) {
				_arrowFive.enabled = true;
				Color tmp = _arrowFive.color;
				tmp.a = (magnitude - 1.8f)*5f;
				_arrowFive.color = tmp;
			} else {
				_arrowFive.enabled = false;
			}
		}

		public void HideAll () {
			_arrowOne.enabled = false;
			_arrowTwo.enabled = false;
			_arrowThree.enabled = false;
			_arrowFour.enabled = false;
			_arrowFive.enabled = false;
		}
	}
}
