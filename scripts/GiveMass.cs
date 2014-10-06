using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveMass : MonoBehaviour
{
	private float G = Constants.G;
	private GameObject[] Players;
	private GameObject[] massiveObjects;
	private List<GameObject> attracteurs;
	private Vector2 R;
	private Vector2 gamma;
	private float Gm;
	private float K;

	void Start()
	{
		Gm = G * rigidbody.mass;
	}

	void LateUpdate()
	{
		massiveObjects = GameObject.FindGameObjectsWithTag("MassiveObjectTag");
		Players = GameObject.FindGameObjectsWithTag("Player");
		attracteurs = new List<GameObject>();
		
		for (int i = 0; i<massiveObjects.Length; i++) {
			attracteurs.Add(massiveObjects [i]);
			//Debug.Log("attracteurs[ " + i + " ] = " + attracteurs [i]);
		}
		for (int i = 0; i<Players.Length; i++) {
			attracteurs.Add(Players [i]);
			//Debug.Log("attracteurs[ " + (i + massiveObjects.Length) + " ] = " + attracteurs [i]);
		}
		
		foreach (GameObject obj in attracteurs) {
			//Debug.Log("attracteurs = " + obj);
			R = transform.position - obj.transform.position;
			if (R != Vector2.zero) {
				K = -Gm * obj.rigidbody.mass;
				gamma = K * R.normalized / R.sqrMagnitude;
				rigidbody.AddForce(gamma * Time.deltaTime);
			}
		}
	}
}