using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {

	private IAenemy e;
	private GameObject destructible;

	// Use this for initialization
	void Start () {
		//e = GameObject.Find ("Enemy 1").GetComponent<IAenemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag.Equals("Enemy")) {

			e = GameObject.Find ("Enemy " + other.gameObject.name.Split (' ') [1]).GetComponent<IAenemy>();

			if (e.recibirAtaque () == 0) {
				// Ha recibido danyo
			} else {
				// Muere
				Destroy(other.gameObject);
			}
		}
		else if (other.gameObject.tag.Equals("destructible")) {

				Destroy(other.gameObject);
		}


	}
}
