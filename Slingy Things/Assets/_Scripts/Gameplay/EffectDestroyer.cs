using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class EffectDestroyer : MonoBehaviour {
		void Start () {
			// Destroys the object when the last frame of the animation is played.
			Destroy (transform.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
		}
	}
}