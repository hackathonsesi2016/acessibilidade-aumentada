using UnityEngine;
using System.Collections;
using UnityEngine.UI;
		
public class legenda : MonoBehaviour {
	public Text legendaexibida, iniciarfilme;
	public string [] bancolegenda;
	public float relogio, temporizador;
	public bool iniciar = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!iniciar)
			return;

		Debug.Log ("aaa");
			relogio += Time.deltaTime;

			if (relogio >= 0.5f && relogio <= 0.1f) {
				legendaexibida.text = bancolegenda [0];

			}

			if (relogio >= 1.1f && relogio <= 4f) {
				legendaexibida.text = bancolegenda [1];

			}
			if (relogio >= 5f && relogio <= 9f) {
			legendaexibida.text = bancolegenda [2];

			}
			if (relogio >= 10f && relogio <= 16f) {
				legendaexibida.text = bancolegenda [3];

			}
			if (relogio >= 17f && relogio <= 21f) {
				legendaexibida.text = bancolegenda [4];
			}
			if (relogio >= 22f && relogio <= 23f) {
				legendaexibida.text = bancolegenda [5];
			}
			if (relogio >= 23.2f && relogio <= 28f) {
			legendaexibida.text = bancolegenda [6];
			}
			if (relogio >= 29f && relogio <= 31f) {
				legendaexibida.text = bancolegenda [7];
			}
			if (relogio >= 32f && relogio <= 36f) {
			legendaexibida.text = bancolegenda [8];
			}
			if (relogio >= 36.1f && relogio <= 40f) {
			legendaexibida.text = bancolegenda [9];
			}
		if (relogio >= 42.1f && relogio <= 43f) {
			legendaexibida.text = "";
		}
	
	}
	public void Achou()
	{	
		iniciarfilme.text = "Seu Filme vai começar";
		iniciar = false;


	}
	public void Sumiu()
	{
		iniciar = true;	
		iniciarfilme.text = "";

		
	}
}
