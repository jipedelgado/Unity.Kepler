using UnityEngine;
using System.Collections;

public class TurnIntoBlackHole : CustomActionScript
{
	
	public int numero = 3;
	public GameObject explosionPrefab;
	public GameObject ObjectPrefab;

	private float k;
	private float r, R, m, M;
	private Vector3 v, V, distance, newPosition;
	private GameObject[] attracteurs ;
	private GameObject Object;

	public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
	{
		r = args.transform.localScale.magnitude;
		m = args.rigidbody.mass;
		v = args.rigidbody.velocity;
		R = sender.transform.localScale.magnitude;
		M = sender.rigidbody.mass;
		V = sender.rigidbody.velocity;
		k = Mathf.Pow(3 / (4 * Mathf.PI), 1 / 3);
		newPosition = Barycenter(sender.gameObject, args);
		attracteurs = GameObject.FindGameObjectsWithTag("MassiveObjectTag");

		/*  la collision de 2 objets A et B de attracteur va déclencher 2 événements e1 et e2, paramétrés par le couple (sender, args),
		 * de valeur respectives ( A, B) et (B, A)
		 * le traitement d'un événement e(x, y) consiste à créer un nouvel objet de type Object de attracteur, de nom x.name + y.name
		 * puis de détruire A et B.
		 * Lors du traitement de e1(A, B) on teste la présence dans attracteur d'un objet nommé B.name + A.name. 
		 * La procédure d'initialisation GameController.cs garantit que ce nom n'existe pas
		 * Donc on crée un objet nommé A.name + B.name
		 * Lors du traitement de e2(B, A) on teste la présence dans attracteur d'un objet nommé A.name + B.name. 
		 * cet objet a été créé lors du traitement de e1(A, B), donc on ne créé pas de nouvel objet.
		 * Un seul objet a été créé, de nom A.name + B.name
		*/
		
		if ((sender.name != "SpaceShip") && (args.name != "SpaceShip")) {
			bool insertOK = true;
			foreach (GameObject objet in attracteurs) {
				if (objet.name == args.name + sender.name)
					insertOK = false;
			}
			if (insertOK) {
				GameObject newObject = Instantiate(ObjectPrefab, sender.transform.position, sender.transform.rotation) as GameObject;
				newObject.name = sender.name + args.name;
				newObject.tag = "MassiveObjectTag";
				newObject.rigidbody.velocity = (M * V + m * v) / (M + m);		// conservation de la quantié de mouvement
				newObject.rigidbody.mass = M + m;
				newObject.audio.Play();

				// création d'un trou noir, si la masse dépasse la masse critique entrainant une vitesse de libération égale à la vitesse de la lumière
				if (newObject.rigidbody.mass > Constants.BlackHoleCriticalMass) {
					float newScale = 5f;
					newObject.transform.localScale = new Vector3(newScale, newScale, newScale);
					newObject.renderer.material.color = Color.black;
					explosionPrefab.transform.localScale = new Vector3(150, 150, 150);
					GameObject explosionInstance = Instantiate(explosionPrefab, sender.transform.position, sender.transform.rotation) as GameObject;
				} else {
					float newScale = k * Mathf.Pow(m + M, (float)1 / 3);
					newObject.transform.localScale = new Vector3(newScale, newScale, newScale);
				}
			}
			sender.tag = "Untagged";
			args.tag = "Untagged";
			Destroy(sender);
			Destroy(args);	
		}
		
		if ((sender.name == "SpaceShip") && (args.name != "SpaceShip")) {
			args.tag = "Untagged";
			Destroy(args);	
		}
		if ((sender.name != "SpaceShip") && (args.name == "SpaceShip")) {
			sender.tag = "Untagged";
			Destroy(sender);	
		}	
		
		yield return null;
	}

	Vector3 Barycenter(GameObject obj1, GameObject obj2)
	{
		return (obj1.rigidbody.mass * obj1.transform.position + obj2.rigidbody.mass * obj2.transform.position) / (obj1.rigidbody.mass + obj2.rigidbody.mass);
	}


}
