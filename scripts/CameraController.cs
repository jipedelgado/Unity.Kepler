using UnityEngine;
using System.Collections;

/* version du 25 septembre 2014
 * Centre la caméra sur le barycentre de l'ensemble des gameObjects
 * taggés comme "MassiveObjectTag"
 * et fixe la taille de l'image selon les valeurs extrèmes 
 * des positions de ces GameObjects
 */

public class CameraController : MonoBehaviour
{
	public Transform Target;
	public float ZoomSpeed = 5;
	
	void FixedUpdate()
	{
		transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, 20.0f);
		transform.LookAt(Target);
	}
	
	void OnGUI()
	{
		camera.orthographicSize *= 1 - Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
	}
}
