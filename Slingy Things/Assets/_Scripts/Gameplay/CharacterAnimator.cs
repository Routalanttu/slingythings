using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class CharacterAnimator : MonoBehaviour {

		[SerializeField] private Animator _headAnimator;
		[SerializeField] private SpriteRenderer _head;
		[SerializeField] private SpriteRenderer _idleTail;
		[SerializeField] private SpriteRenderer _stretchTail;
		[SerializeField] private SpriteRenderer _counterPiece;
		[SerializeField] private Sprite _healthyIdle;
		[SerializeField] private Sprite _damagedIdle;
		[SerializeField] private Sprite _fuckedIdle;
		[SerializeField] private Sprite _stretchedIdle;
		[SerializeField] private Transform _counterPieceTransform;
		[SerializeField] private Transform _stretchTailTransform;
		[SerializeField] private Transform _flightTransform;
		[SerializeField] private SpriteRenderer _flight;
		private CharacterInfo _char;

		private void Awake () {
			_char = GetComponent<CharacterInfo> ();
		}

		public void SetToIdle () {
			_head.enabled = true;
			_idleTail.enabled = true;
			_stretchTail.enabled = false;
			_counterPiece.enabled = false;
			_flight.enabled = false;

			SetFaceToStretched (false);
		}

		public void SetToStretch () {
			_head.enabled = true;
			_idleTail.enabled = false;
			_stretchTail.enabled = true;
			_counterPiece.enabled = true;
			_flight.enabled = false;

			SetFaceToStretched (true);
		}

		public void SetToFlight () {
			_head.enabled = false;
			_idleTail.enabled = false;
			_stretchTail.enabled = false;
			_counterPiece.enabled = false;
			_flight.enabled = true;

			SetFaceToStretched (false);
		}
			
		public void RotateTail (Quaternion rotation) {
			_counterPieceTransform.rotation = rotation;
			// If StretchTail wouldn't be parented to CounterPiece:
			//_stretchTailTransorm.rotation = q;
		}

		public void RotateFlight (Vector2 curVelo) {
			float flightAngle = Mathf.Atan2 (curVelo.y, curVelo.x) * Mathf.Rad2Deg;
			_flightTransform.localRotation = Quaternion.AngleAxis (flightAngle, Vector3.forward);

			if (curVelo.x < 0f) {
				FlightFlip (true);
			} else if (curVelo.x > 0f) {
				FlightFlip (false);
			}
		}

		public void ScaleTail (float stretch) {
			_stretchTailTransform.localScale = new Vector3 (stretch / 1.25f, 1f, 1f);
		}

		public void StretchFlip (bool state) {
			_head.flipX = state;
			_idleTail.flipX = state;
			_stretchTail.flipY = state;
		}

		private void FlightFlip (bool state) {
			_flight.flipY = state;
		}

		public void SetFaceToStretched (bool stretched) {
			_headAnimator.SetBool ("isStretched", stretched);
		}

		public void SetHealthFace (int health) {
			_headAnimator.SetInteger ("slugHealth", health);
		}
			
		// Temp
		private void Update() {
			SetHealthFace (_char.Health);
		}
	}
}
