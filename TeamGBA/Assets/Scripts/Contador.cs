using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Contador : MonoBehaviour {
	
	float tiempoRestante = 10;
	bool esNoche = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(tiempoRestante > 0){
			tiempoRestante -= Time.deltaTime;
			double decimalValue = Math.Ceiling(tiempoRestante);
			GameObject.Find ("Cuenta").GetComponent<Text> ().text = decimalValue.ToString();
			if (esNoche == false) {
				GameObject.Find ("Point light").GetComponent<Light> ().intensity = (float)tiempoRestante; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
			} else {
				GameObject.Find ("Point light").GetComponent<Light> ().intensity = 10.0f-(float)tiempoRestante; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
			}
		}
		else
		{

			if(esNoche == true){
				esNoche = false;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Noche en:";
			}else{
				esNoche = true;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Día en:";
			}
			tiempoRestante = 10;
//			//Debug.Log ("Voy a subir el numero de ronda de " + EstadoJuego.estadoJuego.numRonda + " a " + (EstadoJuego.estadoJuego.numRonda+1).ToString());
//			EstadoJuego.estadoJuego.numRonda++;
//			if(EstadoJuego.estadoJuego.numRonda != 4){
//				Application.LoadLevel(EstadoJuego.estadoJuego.arrayNiveles[(EstadoJuego.estadoJuego.numRonda-1)]);
//			}
		}
	}
}
