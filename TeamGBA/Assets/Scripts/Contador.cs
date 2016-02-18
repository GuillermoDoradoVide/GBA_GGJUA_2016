using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class Contador : MonoBehaviour {
	
	private Player p;
	public float tiempoRestante;
	bool esNoche = false;
	double decimalValue;
	float tiempoRestante_aux; 

	int nochesPasadas;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player").GetComponent<Player>();
		tiempoRestante_aux = tiempoRestante;
		nochesPasadas = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(tiempoRestante_aux > 0){
			tiempoRestante_aux -= Time.deltaTime;
			decimalValue = Math.Ceiling(tiempoRestante_aux);
			GameObject.Find ("Cuenta").GetComponent<Text> ().text = decimalValue.ToString();
			if (esNoche == false) {
				GameObject.Find ("night").GetComponent<Light> ().intensity = 0;
				GameObject.Find ("Point light").GetComponent<Light> ().intensity = (float)tiempoRestante_aux; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
			} else {
				GameObject.Find ("night").GetComponent<Light> ().intensity = 5;

				//GameObject.Find ("Point light").GetComponent<Light> ().intensity = tiempoRestante - (float)tiempoRestante_aux; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
			}
		}
		else
		{
			if(esNoche == true){
				esNoche = false;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Noche en:";
				p.cambiarPersonajeDia ();
			}else{
				esNoche = true;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Día en:";
				p.cambiarPersonajeNoche ();
			}
			tiempoRestante_aux = tiempoRestante;
		}
	}

	public bool getEsNoche(){
		return esNoche;
	}
}
