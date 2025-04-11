using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player")
		{
			Debug.Log("Player is near by! Need to atttack!");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player")
		{
			Debug.Log("Player is far enough");
		}
	}
}
