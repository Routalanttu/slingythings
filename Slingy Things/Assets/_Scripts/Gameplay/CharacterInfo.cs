using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

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
		[SerializeField] private TextMesh _damageText; 
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

		private float _damageTimer; 

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
		}

		void Update(){

			NameVisibility (); 
			HealthVisibility ();

		}

		//Lerp name visibility during character sling 
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

		//Lerp health visibility during character sling 
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

		//Decrease character health by damage amount
		public void DecreaseHealth(int damageAmount) {

            if (damageAmount >=1)
            {
                SoundController.Instance.PlaySoundByIndex((int)Random.Range(3, 10));
            }

			if (damageAmount < 0) {
				damageAmount = 0; 
			}
			_health -= damageAmount;


			if (damageAmount != 0) {
				TextMesh tmpDamageText = (TextMesh)Instantiate(_damageText, _myTransform.position, _myTransform.rotation);
				tmpDamageText.text = "-" + damageAmount; 
				tmpDamageText.color = _healthText.color; 
			}


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

		//Increase character health by heal amount
		public void IncreaseHealth(int healAmount) {

			if (healAmount < 0) {
				healAmount = 0; 
			}

			if (healAmount != 0) {
				TextMesh tmpDamageText = (TextMesh)Instantiate(_damageText, _myTransform.position, _myTransform.rotation);
				tmpDamageText.text = "+" + healAmount; 
				tmpDamageText.color = _healthText.color; 
			}

			_health += healAmount;

			// Take away the excess health gain:
			if (_health > 100) {
				healAmount -= (_health - 100);
				_health = 100;
			}
			GameManager.Instance.IncreaseTeamHealth (_team, healAmount);

			_healthText.GetComponent<TextMesh> ().text = _health.ToString();
	
		}

		//Remove character and apply effects
		public void Die(){
			TextMesh tmpDamageText = (TextMesh)Instantiate(_damageText, _myTransform.position, _myTransform.rotation); 

			tmpDamageText.text = RandomizeDeathText ();
			tmpDamageText.color = Color.gray;
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

		//Display memoir text for deceased character
		private string RandomizeDeathText() {
			string deathMessage;

			switch ((int)Random.Range (0, 69)) {
			case 0:
				deathMessage = _characterName + " assumed" + "\n" + "room temperature";
				break;
			case 1:
				deathMessage = _characterName + " is" + "\n" + "counting worms";
				break;
			case 2:
				deathMessage = _characterName + " bit" + "\n" + "the big one";
				break;
			case 3:
				deathMessage = _characterName + " made" + "\n" + "like a tree"; 
				break;
			case 4:
				deathMessage = _characterName + " is" + "\n" + "wrapped in plastic"; 
				break;
			case 5:
				deathMessage = _characterName + " did" + "\n" + "not make it"; 
				break;
			case 6:
				deathMessage = _characterName + " dropped" + "\n" + "like a fly"; 
				break;
			case 7:
				deathMessage = _characterName + " was" + "\n" + "exterminated"; 
				break;
			case 8:
				deathMessage = _characterName + " is" + "\n" + "food for worms"; 
				break;
			case 9:
				deathMessage = _characterName + "\n" + "went bung"; 
				break;
			case 10:
				deathMessage = _characterName + " went the" + "\n" + "way of all flesh"; 
				break;
			case 11:
				deathMessage = _characterName + " went" + "\n" + "out with a bang"; 
				break;
			case 12:
				deathMessage = _characterName + " cashed" + "\n" + "in the chips"; 
				break;
			case 13:
				deathMessage = _characterName + " took" + "\n" + "a last bow"; 
				break;
			case 14:
				deathMessage = _characterName + " answered" + "\n" + "the final summons"; 
				break;
			case 15:
				deathMessage = _characterName + " rode" + "\n" + "the pale horse"; 
				break;
			case 16:
				deathMessage = _characterName + "\n" + "hopped the twig"; 
				break;
			case 17:
				deathMessage = _characterName + " joined" + "\n" + "the choir invisible"; 
				break;
			case 18:
				deathMessage = _characterName + " joined" + "\n" + "the great majority"; 
				break;
			case 19:
				deathMessage = _characterName + "\n" + "kicked the bucket"; 
				break;
			case 20:
				deathMessage = _characterName + " was sent" + "\n" + "to the farm upstate"; 
				break;
			case 21:
				deathMessage = _characterName + " made the" + "\n" + "ultimate sacrifice"; 
				break;
			case 22:
				deathMessage = _characterName + " was" + "\n" + "murder-death-killed"; 
				break;
			case 23:
				deathMessage = _characterName + " is not" + "\n" + "with us anymore"; 
				break;
			case 24:
				deathMessage = _characterName + " is" + "\n" + "off on a boat"; 
				break;
			case 25:
				deathMessage = _characterName + " is" + "\n" + "off the hooks"; 
				break;
			case 26:
				deathMessage = _characterName + "'s" + "\n" + "hour came"; 
				break;
			case 27:
				deathMessage = _characterName + "'s" + "\n" + "number was up"; 
				break;
			case 28:
				deathMessage = _characterName + "\n" + "pegged out"; 
				break;
			case 29:
				deathMessage = _characterName + " was" + "\n" + "put to sleep"; 
				break;
			case 30:
				deathMessage = _characterName + " shuffled" + "\n" + "off this mortal coil"; 
				break;
			case 31:
				deathMessage = _characterName + "\n" + "snuffed it"; 
				break;
			case 32:
				deathMessage = _characterName + " took" + "\n" + "a dirt nap"; 
				break;
			case 33:
				deathMessage = _characterName + "\n" + "is no more"; 
				break;
			case 34:
				deathMessage = _characterName + " has" + "\n" + "ceased to be"; 
				break;
			case 35:
				deathMessage = _characterName + " is" + "\n" + "bereft of life"; 
				break;
			case 36:
				deathMessage = _characterName + " bought" + "\n" + "a one-way ticket"; 
				break;
			case 37:
				deathMessage = _characterName + "\n" + "checked out"; 
				break;
			case 38:
				deathMessage = _characterName + "\n" + "climbed the stairs"; 
				break;
			case 39:
				deathMessage = _characterName + "\n" + "is defunct"; 
				break;
			case 40:
				deathMessage = _characterName + "\n" + "expired"; 
				break;
			case 41:
				deathMessage = _characterName + "\n" + "flatlined"; 
				break;
			case 42:
				deathMessage = _characterName + " launched" + "\n" + "into eternity"; 
				break;
			case 43:
				deathMessage = _characterName + " has" + "\n" + "left the building"; 
				break;
			case 44:
				deathMessage = _characterName + "\n" + "was liquidated"; 
				break;
			case 45:
				deathMessage = _characterName + " met" + "\n" + "an untimely end"; 
				break;
			case 46:
				deathMessage = _characterName + " is" + "\n" + "out of business"; 
				break;
			case 47:
				deathMessage = _characterName + " rang" + "\n" + "down the curtain"; 
				break;
			case 48:
				deathMessage = _characterName + " took a" + "\n" + "permanent vacation"; 
				break;
			case 49:
				deathMessage = _characterName + " has" + "\n" + "walked the plank"; 
				break;
			case 50:
				deathMessage = _characterName + " was" + "\n" + "called home"; 
				break;
			case 51:
				deathMessage = _characterName + " became" + "\n" + "a root inspector"; 
				break;
			case 52:
				deathMessage = _characterName + " bought" + "\n" + "a pine condo"; 
				break;
			case 53:
				deathMessage = _characterName + "\n" + "crossed the bar"; 
				break;
			case 54:
				deathMessage = _characterName + " is" + "\n" + "living-impaired"; 
				break;
			case 55:
				deathMessage = _characterName + " paid" + "\n" + "Charon's fare"; 
				break;
			case 56:
				deathMessage = _characterName + " paid a" + "\n" + "debt to nature"; 
				break;
			case 57:
				deathMessage = _characterName + " is" + "\n" + "out of print"; 
				break;
			case 58:
				deathMessage = _characterName + "\n" + "popped off"; 
				break;
			case 59:
				deathMessage = _characterName + "\n" + "was put down"; 
				break;
			case 60:
				deathMessage = _characterName + " reached" + "\n" + "the finish line"; 
				break;
			case 61:
				deathMessage = _characterName + " returned" + "\n" + "to the ground"; 
				break;
			case 62:
				deathMessage = _characterName + " returned" + "\n" + "to the source"; 
				break;
			case 63:
				deathMessage = _characterName + " has" + "\n" + "sprouted wings"; 
				break;
			case 64:
				deathMessage = _characterName + "\n" + "succumbed"; 
				break;
			case 65:
				deathMessage = _characterName + " was" + "\n" + "all she wrote"; 
				break;
			case 66:
				deathMessage = _characterName + "\n" + "took a harp"; 
				break;
			case 67:
				deathMessage = _characterName + "\n" + "faded away"; 
				break;
			case 68:
				deathMessage = _characterName + " is" + "\n" + "at rest now"; 
				break;
			case 69:
				deathMessage = _characterName + " made" + "\n" + "like a tree"; 
				break;
			default: 
				deathMessage = _characterName + "\n" + "passed away";  
				break; 
			}

			return deathMessage;
		}
	}
}