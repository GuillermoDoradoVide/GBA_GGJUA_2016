using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {

	private IAenemy e;
	private GameObject dest;

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
				
				other.gameObject.SetActive(false);
			}
		}
		// destruir MUROS
		if (other.gameObject.tag.Equals("destructible")) {

			dest = other.gameObject;

			StartCoroutine (destruirbloque());
		}
	}

	IEnumerator destruirbloque() {  
		while(true) {
			Debug.Log ("entro destruir");
			InvokeRepeating("desaparecer",0.0f,0.1f);
			yield return new WaitForSeconds(1.0f);
			Debug.Log ("pasa un segundo");
			CancelInvoke ();
			StopAllCoroutines ();
			Destroy (dest);
		} 
	}

	void desaparecer() { 

		if (dest != null) {

			Material mat = dest.GetComponent<Renderer> ().material;
			Debug.Log (mat.color.a);
			Color alpha = mat.color;
			alpha.a -= Time.deltaTime*6;
			mat.SetColor ("_Color", alpha);
			dest.GetComponent<Renderer> ().material = mat;

		}


	}
}
