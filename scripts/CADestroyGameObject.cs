using UnityEngine;
using System.Collections;

public class CADestroyTarget : CustomActionScript
{
	public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
	{
		yield return new WaitForFixedUpdate();
		Destroy(args);
		yield return null;
	}
}
