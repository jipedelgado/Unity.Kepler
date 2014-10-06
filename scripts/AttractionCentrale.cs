using UnityEngine;
using System.Collections;

public class AttractionCentrale : MonoBehaviour {

	public GameObject attracteur;
	public float vitesseInitialeZ = 1.0f;
	public float G = Constants.G;

	private Vector3 vitesseInitiale;
	private Vector2 R;
	private Vector2 gamma;
	private float K;
	
	void Start(){

		K = -G * rigidbody.mass * attracteur.rigidbody.mass;
		rigidbody.AddForce (new Vector3(0.0f, vitesseInitialeZ));
	}

	void LateUpdate () {

		R = transform.position - attracteur.transform.position;
		gamma = K * R.normalized / R.sqrMagnitude;
		rigidbody.AddForce (gamma * Time.deltaTime);

		transform.rotation = Quaternion.FromToRotation(Vector2.right, rigidbody.velocity);
	}

}
