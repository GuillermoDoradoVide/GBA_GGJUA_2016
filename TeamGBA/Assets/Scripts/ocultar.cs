using UnityEngine;
using System.Collections;

public class ocultar : MonoBehaviour {

	public bool lag_time = false;
	public float alpha = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (GameObject.Find ("GameObject").GetComponent<Contador> ().getEsNoche ()) {

		if (lag_time) {

			show();
		}


		}
		else {

			if ( !lag_time) {

				hide();
			}
		}
	}

void show() {
		
		gameObject.GetComponent<Renderer> ().enabled = true;
		lag_time = false;

}

void hide() {

		gameObject.GetComponent<Renderer> ().enabled = false;
		lag_time = true;

}





}


