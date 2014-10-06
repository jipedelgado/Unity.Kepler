using UnityEngine;
using System.Collections;

public class CreateNewUniverse : MonoBehaviour
{
	public GameObject ObjectPrefab;
	public float G = Constants.G ;
	public int SizeOfUniverse = Constants.SizeOfUniverse;
	public int NumberOfStars = Constants.NumberOfStars;
	public float StarMinMass = Constants.StarMinMass;
	public float StarMaxMass = Constants.StarMaxMass;
	public float InitialVelocity = Constants.InitialVelocity;
	private GameObject[] attracteurs;

	void Start()
	{
		// delete Old MassiveObjects
		attracteurs = GameObject.FindGameObjectsWithTag("MassiveObjectTag");
		foreach (GameObject obj in attracteurs) {
			obj.tag = "Untagged";
			Destroy(obj);
		}
		for (int i = 0; i<NumberOfStars; i++) {

			// randomize position
			int xInit = Random.Range(-SizeOfUniverse, SizeOfUniverse);
			int yInit = Random.Range(-SizeOfUniverse, SizeOfUniverse);

			// create object
			GameObject newObject = Instantiate(ObjectPrefab, new Vector2(xInit, yInit), Quaternion.identity) as GameObject;
			
			//give each object different name
			newObject.name = ((48 + (char)i)).ToString();

			newObject.tag = "MassiveObjectTag";

			// random initial velocity
			float vx, vy, vz;
			vx = Random.Range(-InitialVelocity, InitialVelocity);
			vy = Random.Range(-InitialVelocity, InitialVelocity);
			vz = Random.Range(-InitialVelocity, InitialVelocity);
			newObject.rigidbody.velocity = new Vector3(vx, vy, vz);	

			newObject.rigidbody.mass = Random.Range(StarMinMass, StarMaxMass);
		
			//set size as mass
			float k = Mathf.Pow(3 / (4 * Mathf.PI), 1 / 3);
			float newScale = k * Mathf.Pow(newObject.rigidbody.mass, (float)1 / 3);
			newObject.transform.localScale = new Vector3(newScale, newScale, newScale);

		}
	}
		

}
