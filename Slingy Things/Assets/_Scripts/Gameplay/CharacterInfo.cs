using UnityEngine;
using System.Collections;

namespace SlingySlugs {
	public class CharacterInfo : MonoBehaviour {

		private int _team; 

		public int Team {
			get{
				return _team; 
			}
			set{
				_team = value; 
			}
		}

		private enum Species {
			Slug,
			Snail,
			Octopus,
			Siika,
		}

		[SerializeField] private Species _species;
		[SerializeField] private int _health = 100;
		[SerializeField] private GameObject _deathAnimation;
		[SerializeField] private GameObject _healthTextObject;
		[SerializeField] private GameObject _nameTextObject;
		private TextMesh _healthText;
		private TextMesh _nameText; 
		private Transform _myTransform;

		private string _characterName; 

		private bool _dead = false;

		private float _nameAlphaMinimum = 0f;
		private float _nameAlphaMaximum = 1f;
		private float _nameTime = 0f; 
		private Color _nameTempColor;  

		private float _healthAlphaMinimum = 0f;
		private float _healthAlphaMaximum = 1f;
		private float _healthTime = 0f; 
		private Color _healthTempColor;  

		private bool _makeNameVisible; 
		private bool _makeNameHidden; 
		private bool _makeHealthVisible; 
		private bool _makeHealthHidden; 

		private void Awake () {
			_myTransform = GetComponent<Transform> ();

			_healthTextObject = (GameObject)Instantiate (
				_healthTextObject, transform.position + new Vector3(0f,1f,0f), 
				Quaternion.identity, transform);

			_nameTextObject = (GameObject)Instantiate (
				_nameTextObject, transform.position + new Vector3(0f,1.9f,0f), 
				Quaternion.identity, transform);
			
			_healthText = _healthTextObject.GetComponent<TextMesh> ();
			_healthText.GetComponent<TextMesh> ().text = _health.ToString();

			_nameText = _nameTextObject.GetComponent<TextMesh> (); 
			_nameText.GetComponent<TextMesh> ().text = "defaultname"; 


		}

		void Start(){
			_nameTempColor = _nameText.color; 
			_healthTempColor = _healthText.color; 

			Debug.Log ("this slug belongs to team" + _team); 
		}

		void Update(){

			NameVisibility (); 
			HealthVisibility ();

		}

		void NameVisibility(){

			if (_makeNameVisible && _nameText.color.a < 1) {

				_nameTempColor.a = Mathf.Lerp (_nameAlphaMinimum, _nameAlphaMaximum, _nameTime ); 
				_nameTime += 2*Time.deltaTime; 
				_nameText.color = _nameTempColor; 

				if (_nameText.color.a >= 1) {
					_makeNameVisible = false; 
					_nameTime = 0; 

				}
			}

			if (_makeNameHidden && _nameText.color.a > 0) {
				_nameTempColor.a = Mathf.Lerp (_nameAlphaMaximum, _nameAlphaMinimum, _nameTime ); 
				_nameTime += 3*Time.deltaTime; 
				_nameText.color = _nameTempColor; 

				if (_nameText.color.a <= 0) {
					_makeNameHidden = false; 
					_nameTime = 0; 
				}
			}

		}

		void HealthVisibility(){

			if (_makeHealthVisible && _healthText.color.a < 1) {
				_healthTempColor.a = Mathf.Lerp (_healthAlphaMinimum, _healthAlphaMaximum, _healthTime ); 
				_healthTime += 2*Time.deltaTime; 
				_healthText.color = _healthTempColor; 

				if (_healthText.color.a >= 1) {
					_makeHealthVisible = false; 
					_healthTime = 0; 

				}
			}

			if (_makeHealthHidden && _healthText.color.a > 0) {
				_healthTempColor.a = Mathf.Lerp (_healthAlphaMaximum, _healthAlphaMinimum, _healthTime ); 
				_healthTime += 3*Time.deltaTime; 
				_healthText.color = _healthTempColor; 

				if (_healthText.color.a <= 0) {
					_makeHealthHidden = false; 
					_healthTime = 0; 
				}
			}

		}



		public int Health {
			get { return _health; }
		}

		public bool IsActive {
			set; 
			get; 
		}

		public void SetName(string name){
			_characterName = name; 
			_nameText.GetComponent<TextMesh> ().text = _characterName; 
		}

		public void DecreaseHealth(int damageAmount) {

            if (damageAmount >=1)
            {
                SoundController.Instance.PlaySoundByIndex((int)Random.Range(3, 10));
            }
			_health -= damageAmount;

			// Take away the unnecessary (below-zero) damage from team counter:
			if (_health < 0) {
				damageAmount += _health;
			}
			GameManager.Instance.DecreaseTeamHealth (_team, damageAmount); 

			if(!_dead && _health <= 0) {
				Die (); 
			}

			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
		}

		public void IncreaseHealth(int healAmount) {
			_health += healAmount;

			// Take away the excess health gain:
			if (_health > 100) {
				healAmount -= (_health - 100);
				_health = 100;
			}
			GameManager.Instance.DecreaseTeamHealth (_team, healAmount);

			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
		}

		public void Die(){
            SoundController.Instance.PlaySoundByIndex((int)Random.Range(10, 14)); 
			_dead = true;
			Instantiate(_deathAnimation, _myTransform.position, Quaternion.identity);
			GameManager.Instance.KillSlug (_team, gameObject);
		}

		public int GetSpecies () {
			return (int)_species;
		}
			

		public void SetColor(Color unityColor){

			_healthText.color = unityColor;
			_nameText.color = unityColor; 

		}

		public void ShowName(bool showname){
			if (showname) { 
				_makeNameVisible = true; 
			} else {
				_makeNameHidden = true; 
			}
		}

		public void ShowHealth(bool showHealth){
			if (showHealth) {
				_makeHealthVisible = true; 
			} else {
				_makeHealthHidden = true; 
			}
		}

		public void SetDeath () {
			_dead = true;
		}
	}
}