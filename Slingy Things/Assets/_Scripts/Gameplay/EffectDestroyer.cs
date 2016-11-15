using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class EffectDestroyer : MonoBehaviour {
		void Start () {
			Destroy (transform.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
		}
	}
}