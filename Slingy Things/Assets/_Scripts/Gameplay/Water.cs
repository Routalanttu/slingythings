using UnityEngine;
using System.Collections;

namespace SlingySlugs {

	public class Water : MonoBehaviour {

		[SerializeField] private float _waterSpeed = 1f;
		private Transform _gcTransform; 
		private float _startPosX;
		private float _startPosY;
		private float _posX; 
		private float _posY; 
		private float _objectWidth;
		private float _bobAngle;

		private void Start () {
			_gcTransform = GetComponent<Transform> (); 
			_startPosX = _gcTransform.position.x; 
			_startPosY = _gcTransform.position.y;

			_posX = _startPosX; 
			_posY = _startPosY; 

			_objectWidth = GetComponent<Renderer> ().bounds.size.x;
		}

		void Update () {
			_posX += Time.deltaTime * _waterSpeed;
			if (_bobAngle < Mathf.PI*2) {
				_bobAngle += Time.deltaTime * _waterSpeed;
			} else {
				_bobAngle = 0f;
			}
			_posY = _startPosY + Mathf.Sin (_bobAngle)*0.2f;

			_gcTransform.position = new Vector2 (_posX, _posY); 

			if (_posX > (_startPosX + _objectWidth)) {
				_posX = _startPosX; 
			}
		}
	}
}
