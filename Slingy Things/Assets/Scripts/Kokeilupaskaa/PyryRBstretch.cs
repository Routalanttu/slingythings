using UnityEngine;
using System.Collections;

public class PyryRBstretch: MonoBehaviour {

	public float _maxStretch = 2.0f; 

	private bool clickedOn;

	private GameObject _idleTail;
	private GameObject _stretchTail;
	private GameObject _upperBody;
	private GameObject _counterPiece; 

	Vector2 _slugPosition;
	Vector2 _mousePos;
	Transform _gcTransform;

	[SerializeField]private AudioClip squish;

	private bool _thrown;

	[SerializeField]private SpriteRenderer _arrowOne;
	[SerializeField]private SpriteRenderer _arrowTwo;
	[SerializeField]private SpriteRenderer _arrowThree;
	[SerializeField]private SpriteRenderer _arrowFour;
	[SerializeField]private SpriteRenderer _arrowFive;

	void Awake(){

	}

	// Use this for initialization
	void Start () {

		_gcTransform = GetComponent<Transform> (); 

		_idleTail = this.gameObject.transform.GetChild(0).gameObject; 
		_stretchTail= this.gameObject.transform.GetChild(1).gameObject; 
		_upperBody= this.gameObject.transform.GetChild(2).gameObject; 
		_counterPiece= this.gameObject.transform.GetChild(3).gameObject;  

	}

	// Update is called once per frame
	void Update () {

		if (clickedOn) {
			Dragging ();
			_idleTail.GetComponent<SpriteRenderer> ().enabled = false;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = true;
			_counterPiece.GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			//_idleTail.GetComponent<SpriteRenderer> ().enabled = true;
			_stretchTail.GetComponent<SpriteRenderer> ().enabled = false;
			_counterPiece.GetComponent<SpriteRenderer> ().enabled = false;
			if (_thrown) {

			}
		}

	}

	void OnMouseDown(){
		clickedOn = true;
	}

	void OnMouseUp(){
		clickedOn = false;
		SoundController.Instance.PlaySoundByIndex (1, _gcTransform.position); 
		HideAllArrows ();
	}

	void Dragging(){

		_mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
		_slugPosition = _gcTransform.position; //have to use the vector2 form for below

		Vector2 vectorToTarget = _slugPosition - _mousePos; 

		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		float angleInRad = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x);
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		_stretchTail.transform.rotation = q;
		_counterPiece.transform.rotation = q;


		if (vectorToTarget.magnitude > 0.8f) {
			if (vectorToTarget.magnitude < _maxStretch) {
				_stretchTail.transform.localScale = new Vector3 (vectorToTarget.magnitude / 1.25f, 1f, 1f);
			} else {
				_stretchTail.transform.localScale = new Vector3 (_maxStretch / 1.25f, 1f, 1f);
			}

			if (vectorToTarget.magnitude > 1f) {
				_arrowOne.enabled = true;
				Color tmp = _arrowOne.color;
				tmp.a = (vectorToTarget.magnitude - 1f)*5f;
				_arrowOne.color = tmp;
			} else {
				_arrowOne.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.2f) {
				_arrowTwo.enabled = true;
				Color tmp = _arrowTwo.color;
				tmp.a = (vectorToTarget.magnitude - 1.2f)*5f;
				_arrowTwo.color = tmp;
			} else {
				_arrowTwo.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.4f) {
				_arrowThree.enabled = true;
				Color tmp = _arrowThree.color;
				tmp.a = (vectorToTarget.magnitude - 1.4f)*5f;
				_arrowThree.color = tmp;
			} else {
				_arrowThree.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.6f) {
				_arrowFour.enabled = true;
				Color tmp = _arrowFour.color;
				tmp.a = (vectorToTarget.magnitude - 1.6f)*5f;
				_arrowFour.color = tmp;
			} else {
				_arrowFour.enabled = false;
			}
			if (vectorToTarget.magnitude > 1.8f) {
				_arrowFive.enabled = true;
				Color tmp = _arrowFive.color;
				tmp.a = (vectorToTarget.magnitude - 1.8f)*5f;
				_arrowFive.color = tmp;
			} else {
				_arrowFive.enabled = false;
			}
		} else {
			_stretchTail.transform.localScale = new Vector3 (0.8f / 1.25f, 1f, 1f);
			HideAllArrows ();
		}

		if (vectorToTarget.x < 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = true;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = true;

		} else if (vectorToTarget.x > 0f) {
			_upperBody.GetComponent<SpriteRenderer> ().flipX = false;
			_idleTail.GetComponent<SpriteRenderer> ().flipX = false;

		}

	}

	private void HideAllArrows() {
		_arrowOne.enabled = false;
		_arrowTwo.enabled = false;
		_arrowThree.enabled = false;
		_arrowFour.enabled = false;
		_arrowFive.enabled = false;
	}




}
