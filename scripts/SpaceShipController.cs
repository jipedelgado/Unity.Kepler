using UnityEngine;
using System.Collections;

/* version du 25 septembre 2014
 * Détermine une position initiale au hasard, dans un rectangle donné.
 * Accélère ou déccélère à la roulette de la souris ou par les touches up et down
 */

public class SpaceShipController : MonoBehaviour
{
	public float Acceleration = 0.01f;

	void Start()
	{
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.UpArrow)) {
			rigidbody.velocity *= 1 + Acceleration;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			rigidbody.velocity *= 1 - Acceleration;
		}

	}
}
