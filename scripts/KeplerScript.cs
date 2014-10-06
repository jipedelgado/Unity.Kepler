using UnityEngine;
using System.Collections;

public class KeplerScript : MonoBehaviour {

	public GameObject centre;
	public float energie = 1f;
	public float demiGrandAxe = 10f;
	public float demiPetitAxe = 8f;
	public float AngleDepart = 0f;
	public float Inclinaison = 0f;

	private float theta;
	private Vector3 rayonVecteur;
	private float rayonVecteurModule;
	private float demiFocale;
	private float excentricite;
	private float param;
	private float surface;
	private float deltaSurface;
	private float inclinaison;

	static float pi = 3.14159f;

	void Start () {

		demiFocale = Mathf.Sqrt (demiGrandAxe * demiGrandAxe - demiPetitAxe * demiPetitAxe);
		excentricite = demiFocale / demiGrandAxe;
		param = demiPetitAxe * demiPetitAxe / demiGrandAxe;
		surface = pi * demiGrandAxe * demiPetitAxe;
		deltaSurface = surface * energie / 100f;
		rayonVecteurModule = demiGrandAxe - demiFocale;
		inclinaison = Inclinaison * pi / 180;
		theta = AngleDepart * pi / 180;
		rayonVecteur = new Vector2(rayonVecteurModule * Mathf.Cos(theta) , rayonVecteurModule * Mathf.Sin(theta));
		transform.position =  rayonVecteur;	
	}
	
	void Update () {
			theta += deltaSurface / (rayonVecteurModule * rayonVecteurModule);
			rayonVecteurModule = param / (1f + excentricite * Mathf.Cos(theta));
		rayonVecteur = new Vector2(rayonVecteurModule * Mathf.Cos(theta+inclinaison) , rayonVecteurModule * Mathf.Sin(theta+inclinaison));
		transform.position =  centre.transform.position + rayonVecteur;
    
	}
}
