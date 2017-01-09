using UnityEngine;
using System.Collections;

namespace SlingySlugs
{
	//Base Slinger class where different slinger scripts are derived from
	public class BaseSlinger : MonoBehaviour
	{
		protected Transform _gcTransform; 
		protected Rigidbody2D _rigidBody;
		protected CharacterInfo _charInfo; 
		protected CharacterAnimator _charAnim;
		protected Explosion _explosion; 

		protected bool _isArmed;
		protected bool _isSlung;
	
		protected float _soundCooldown = 1f;
		protected float _forceMP; 
	
		protected void Init(float forceMultiplier){
			_gcTransform = GetComponent<Transform> (); 
			_rigidBody = GetComponent<Rigidbody2D> (); 
			_charInfo = GetComponent<CharacterInfo> ();
			_charAnim = GetComponent<CharacterAnimator> ();
			_explosion = GetComponent<Explosion> (); 
			_forceMP = forceMultiplier; 
		}

		//Sling character into air. Stretch amount defines sling force. 
		public void Sling (Vector2 stretchVector) {
			if (Random.Range (0f, 10f) < 2f) {
				SoundController.Instance.PlaySoundByIndex (21);
			}else{
				SoundController.Instance.PlaySoundByIndex((int)Random.Range(18, 20)); 
			}

			_rigidBody.AddForce (stretchVector * _forceMP, ForceMode2D.Impulse);
			_isArmed = true;
			GameManager.Instance.DeactivateCircleColliders(); 
			GameManager.Instance.SlugSlunged ();
			_charInfo.ShowName (false);
			_charInfo.ShowHealth (false); 
		}

		public void ShowNameAndHealth(){
			_charInfo.ShowName (true); 
			_charInfo.ShowHealth (true); 
		}

		//Character is in air
		public void SetToSlung () {
			_isSlung = true;
			_charAnim.SetToFlight ();
		}

		public bool GetArmedState() {
			return _isArmed;
		}
			
	}

}
