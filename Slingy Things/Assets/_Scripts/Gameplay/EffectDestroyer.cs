using UnityEngine;
using System.Collections;

public class EffectDestroyer : MonoBehaviour {
	void Start () {
		Destroy (transform.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
	}
}