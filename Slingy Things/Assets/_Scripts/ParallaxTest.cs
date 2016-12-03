using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class ParallaxTest : MonoBehaviour {
		[SerializeField] private Camera _cam;
		[SerializeField] private Transform _one;
		[SerializeField] private Transform _two;
		[SerializeField] private Transform _three;

		private void Update() {
			_one.position = new Vector3 (
				_cam.transform.position.x * 1.1f,
				24f + _cam.transform.position.y * 1.1f,
				_one.position.z
			);

			_two.position = new Vector3 (
				_cam.transform.position.x * 1.2f,
				24f + _cam.transform.position.y * 1.2f,
				_two.position.z
			);

			_three.position = new Vector3 (
				_cam.transform.position.x * 1.3f,
				40f + _cam.transform.position.y * 1.3f,
				_three.position.z
			);


		}
	}
}