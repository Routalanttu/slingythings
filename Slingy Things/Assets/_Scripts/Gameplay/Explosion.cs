using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Explosion : MonoBehaviour {

		[SerializeField] private float radius = 5.0F;
		[SerializeField] private float _explosionForce = 10F;
		[SerializeField] private int _explosionDamageMultiplier = 2; 
		[SerializeField] private ParticleSystem _explosionPrefab; 
		[SerializeField] private GameObject _explosionAnimation; 
		private Transform _gcTransform;
		private Animator _xploAnim;

		void Awake () {
			_gcTransform = GetComponent<Transform>(); 
			//_xploAnim = _explosionAnimation.GetComponent<Animator> ();
		}

		public void Arm () {
			//_xploAnim.SetBool ("Blown", false);
		}

		public bool Fire(){
			//_xploAnim.SetBool ("Blown", true);
			Vector2 xploPos = _gcTransform.position; 
			Instantiate(_explosionPrefab, _gcTransform.position, Quaternion.identity);
			Instantiate(_explosionAnimation, _gcTransform.position, Quaternion.identity);
			SoundController.Instance.PlaySoundByIndex (0); 

			Collider2D[] colliders = Physics2D.OverlapCircleAll(xploPos, radius);
			foreach (Collider2D hit in colliders) {
				Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

				Vector2 hitPosition = hit.GetComponent<Transform>().position;
				Vector2 explosionDelta = hitPosition- xploPos; //Get vector between explosion and hit 
				Vector2 explosionDir = Vector3.Normalize (explosionDelta); //normalize the vector to get direction only 
				float deltaDistance = radius - explosionDelta.magnitude; //get the effective blast magnitude
				int explosionDamage = (int)deltaDistance * _explosionDamageMultiplier; 

				if (rb != null && hit.gameObject.tag == "Slug" && rb!= this.gameObject.GetComponent<Rigidbody2D>()){
					rb.AddForce (explosionDir * _explosionForce, ForceMode2D.Impulse);  
					hit.GetComponent<CharacterInfo> ().DecreaseHealth (explosionDamage); 
					hit.GetComponent<Slinger> ().SetToSlung ();
				}
			}

			GameManager.Instance.NextPlayerMove();

			return false;
		}
	}
}