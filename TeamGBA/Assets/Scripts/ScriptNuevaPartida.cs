using UnityEngine;
using System.Collections;

public class ScriptNuevaPartida : MonoBehaviour {
	
	public void nuevaPartida () {
		GameObject.Find ("Button_NuevaPartida").GetComponent<AudioSource>().Play();
		Invoke ("CargarNuevaPartida", 1);//GameObject.Find ("Button_NuevaPartida").GetComponent<AudioSource>().clip.length);
	}	

	public void volverAlMenu () {
		GameObject.Find ("Button_VolverAlMenu").GetComponent<AudioSource>().Play();
		Invoke("CargarVolverAlMenu", 1);
	}

	public void creditos () {
		GameObject.Find ("Button_Creditos").GetComponent<AudioSource>().Play();
		Invoke("CargarCreditos", 1);
	}
	
	public void comoJugar () {
		GameObject.Find ("Button_ComoJugar").GetComponent<AudioSource>().Play();
		Invoke("CargarComoJugar", 1);
	}
	
	public void salir () {
		GameObject.Find ("Button_Salir").GetComponent<AudioSource>().Play();
		Invoke("CargarSalir", 1);
	}
	
	void CargarNuevaPartida(){
		Application.LoadLevel("LevelOne");
	}
	
	void CargarVolverAlMenu(){
		Application.LoadLevel("MainMenu");
	}
	
	void CargarCreditos(){
		Application.LoadLevel("Credits");
	}
	
	void CargarComoJugar(){
		Application.LoadLevel ("Como");
	}

	void CargarSalir(){
		Application.Quit ();
	}
}
