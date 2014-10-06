using UnityEngine;
using System.Collections;

public class AlignWithVelocity : MonoBehaviour
{
	
	void LateUpdate()
	{
		transform.rotation = Quaternion.FromToRotation(Vector3.down, rigidbody.velocity);
	}
}
