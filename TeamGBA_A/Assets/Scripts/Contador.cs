using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class Contador : MonoBehaviour {
	
	private Player p;

	int random;
	public Light sun;
	public float tiempoRestante;

	public float sunrot;
	public float actual_sunrot;
	public float delta;
	public float summit;

	bool esNoche = false;
	double decimalValue;
	public float tiempoRestante_aux;
	public float dia_intensidad = 0.0f;
	float intensidad_actual = 0.0f;

	int nochesPasadas;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player").GetComponent<Player>();
		GameObject.Find ("Luz_aux").gameObject.SetActive(false);
		tiempoRestante_aux = tiempoRestante;
		nochesPasadas = 0;
		dia_intensidad = GameObject.Find ("dia").GetComponent<Light> ().intensity;
		intensidad_actual = dia_intensidad;
		sunrot = 360.0f / tiempoRestante;
		sunrot = sunrot / 2.0f;
		summit = 2.2f;
		actual_sunrot = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(tiempoRestante_aux > 0.0f){

			delta += Time.deltaTime;

			sun.transform.localRotation = Quaternion.Euler(actual_sunrot , 0.0f, 0.0f);

			actual_sunrot += sunrot * Time.deltaTime;
			if (actual_sunrot >= 360.0f) {

				actual_sunrot = 0.0f;
			}

			tiempoRestante_aux -= Time.deltaTime;
			decimalValue = Math.Ceiling(tiempoRestante_aux);
			GameObject.Find ("Cuenta").GetComponent<Text> ().text = decimalValue.ToString();
			if (esNoche == false) {
				
				if (tiempoRestante_aux <= summit) {
					intensidad_actual  -= (Time.deltaTime/summit);
					GameObject.Find ("dia").GetComponent<Light> ().intensity = (float)intensidad_actual; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
				}
				if (tiempoRestante_aux >= (tiempoRestante-summit)) {
					intensidad_actual  += (Time.deltaTime/summit);
					GameObject.Find ("dia").GetComponent<Light> ().intensity = (float)intensidad_actual; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
					GameObject.Find ("night_vision").GetComponent<Light> ().intensity -= Time.deltaTime*4.0f; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
				}
			} else {

				if (tiempoRestante_aux >= (tiempoRestante-summit)) {
					GameObject.Find ("night_vision").GetComponent<Light> ().intensity = (tiempoRestante - (float)tiempoRestante_aux) * 5.0f;
				}

			//GameObject.Find ("day").GetComponent<Light> ().intensity = tiempoRestante - (float)tiempoRestante_aux; //.intensity = 0.0f; GetComponent<Text> ().text = decimalValue.ToString();
			}
		}
		else
		{
			if(esNoche == true){
				intensidad_actual = 0.0f;
				esNoche = false;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Noche en:";
				p.cambiarPersonajeDia ();
			}else{
				
				esNoche = true;
				GameObject.Find ("Texto").GetComponent<Text> ().text = "Día en:";
				p.cambiarPersonajeNoche ();


        //SPAWN DE ENEMIGOS

        //Sacamos la probabilidad de que vuelva a respawnear cada enemigo dependiendo del numero de noches pasadas.

        //Probabilidad total= 10;
        //Probabilidad cada noche = nochesPasadas;

        //aleatorioObtenido = numEntre(0, 10)

        //if(aleatorioObtenido < nochesPasadas){
          //Spawnea
        //}else{
          //No spawnea
        //}
        random = 0;
				for (int i = 1; i <= 10; i++) {

				  if (GameObject.Find("Enemies").gameObject.transform.FindChild("Enemy " + i).gameObject.active == false) {
					Debug.Log ("Va a intentar respawnear el enemigo "+i+".");
					random = UnityEngine.Random.Range (0, 10);
					if (random <= nochesPasadas) {
					  Debug.Log ("Con prob=" + nochesPasadas * 10 + "%, ha spawneado el enemigo" + i + " (ha salido " + random + ")");

					  GameObject.Find("Enemies").gameObject.transform.FindChild("Enemy " + i).GetComponent<IAenemy>().revivir();
					  GameObject.Find("Enemies").gameObject.transform.FindChild("Enemy " + i).gameObject.SetActive(true);
					} else {
					  Debug.Log ("No lo consigue... (Ha salido un " + random + ")");
					}
				  }
				}

				nochesPasadas++;
					}
					tiempoRestante_aux = tiempoRestante;
				}
	}

	public bool getEsNoche(){
		return esNoche;
	}
}
