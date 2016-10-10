using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class Explosion : MonoBehaviour {

		public float radius = 5.0F;
		public float _explosionForce = 10F;
		public int _explosionDamageMultiplier = 2; 
		public ParticleSystem _explosionPrefab; 
		private Transform _gcTransform; 
		private bool _fire;
		private CharacterInfo _slug; 

		private Transform xplo;
		private Animator niggers;

		// Use this for initialization
		void Start () {

			_gcTransform = GetComponent<Transform>(); 
			_slug = GetComponent<CharacterInfo> (); 

			xplo = GameObject.FindGameObjectWithTag ("Explosion").transform;
			xplo.position = new Vector2 (9000f, 9000f);
			niggers = xplo.gameObject.GetComponent<Animator> ();

		}

		// Update is called once per frame
		void Update () {


		}

		void OnCollisionEnter2D(Collision2D coll){

			if (Armed && _slug.IsActive) {
				Fire(); 
			}

		}

		void Fire(){

			xplo.position = transform.position;
			niggers.SetBool ("Blown", true);

			Vector2 explosionPos = _gcTransform.position; 
			ParticleSystem explosion; 
			explosion = Instantiate(_explosionPrefab, _gcTransform.position, Quaternion.identity) as ParticleSystem; 
			explosion.Play(); 
			SoundController.Instance.PlaySoundByIndex (0, _gcTransform.position); 

			Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
			foreach (Collider2D hit in colliders) {
				Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
				Vector2 hitPosition = hit.GetComponent<Transform>().position;
				Vector2 explosionDelta = hitPosition- explosionPos; //Get vector between explosion and hit 
				Vector2 explosionDir = Vector3.Normalize (explosionDelta); //normalize the vector to get direction only 
				float deltaDistance = radius - explosionDelta.magnitude; //get the effective blast magnitude
				int explosionDamage = (int)deltaDistance * _explosionDamageMultiplier; 

				if (rb != null && hit.gameObject.tag == "Slug"){
					rb.AddForce (explosionDir * _explosionForce, ForceMode2D.Impulse);  
					hit.GetComponent<CharacterInfo> ().DecreaseHealth (explosionDamage); 

				}
			}

			Armed = false; 
			_fire = false; 

			GameManager.Instance.NextPlayerMove();



		}

		public void ArmSlug(){
			Armed = true;
			niggers.SetBool ("Blown", false);
			xplo.position = new Vector2 (9000f, 9000f);
		}

		public void NextPlayerMove(){
			GameManager.Instance.NextPlayerMove (); 
		}

		public bool Armed {
			get;
			private set;
		}

	}
}