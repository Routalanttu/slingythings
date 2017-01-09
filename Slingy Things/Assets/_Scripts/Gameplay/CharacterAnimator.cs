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

		/// <summary>
		/// Sets the character parts needed for idling visible, hiding others.
		/// </summary>
		public void SetToIdle () {
			_head.enabled = true;
			_idleTail.enabled = true;
			_stretchTail.enabled = false;
			_counterPiece.enabled = false;
			_flight.enabled = false;
		}

		/// <summary>
		/// Sets the character parts needed for stretching visible, hiding others.
		/// </summary>
		public void SetToStretch () {
			_head.enabled = true;
			_idleTail.enabled = false;
			_stretchTail.enabled = true;
			_counterPiece.enabled = true;
			_flight.enabled = false;
		}

		/// <summary>
		/// Sets the character parts needed for flying visible, hiding others.
		/// </summary>
		public void SetToFlight () {
			_head.enabled = false;
			_idleTail.enabled = false;
			_stretchTail.enabled = false;
			_counterPiece.enabled = false;
			_flight.enabled = true;
		}
			
		/// <summary>
		/// Rotates the object that the tail is parented to.
		/// </summary>
		/// <param name="rotation">Rotation from the stretching.</param>
		public void RotateTail (Quaternion rotation) {
			_counterPieceTransform.rotation = rotation;
		}

		/// <summary>
		/// Rotates the flying animal to face the direction it's heading.
		/// </summary>
		/// <param name="curVelo">Object velocity.</param>
		public void RotateFlight (Vector2 curVelo) {
			float flightAngle = Mathf.Atan2 (curVelo.y, curVelo.x) * Mathf.Rad2Deg;
			_flightTransform.localRotation = Quaternion.AngleAxis (flightAngle, Vector3.forward);

			if (curVelo.x < 0f) {
				FlightFlip (true);
			} else if (curVelo.x > 0f) {
				FlightFlip (false);
			}
		}

		/// <summary>
		/// Stretches the tail according to input.
		/// </summary>
		/// <param name="stretch">The stretch amount from input.</param>
		public void ScaleTail (float stretch) {
			_stretchTailTransform.localScale = new Vector3 (stretch / 1.25f, 1f, 1f);
		}

		/// <summary>
		/// Flips the facing direction of the stretched animal.
		/// </summary>
		/// <param name="state">The desired boolean value for flipping.</param>
		public void StretchFlip (bool state) {
			_head.flipX = state;
			_idleTail.flipX = state;
			_stretchTail.flipY = state;
		}

		/// <summary>
		/// Prevents the flying animal from being upside down when flying left.
		/// </summary>
		/// <param name="state">The desired boolean value for flipping.</param>
		private void FlightFlip (bool state) {
			_flight.flipY = state;
		}
	}
}
