using UnityEngine;
using System.Collections;

public class FrozenPlayer : MonoBehaviour {
		
		public GameObject deathParticles;
		
		public void Die()
		{
			Instantiate (deathParticles, transform.position, Quaternion.Euler (270, 0, 0));
			transform.position = new Vector3(1000, 1000, 10000);
			Destroy (this.gameObject, 0.1f);
		}
}
