using UnityEngine;
using System.Collections;

public class HitDelegate : MonoBehaviour {
	public void Hit() {
		Asteroids asteroids = (Asteroids)transform.parent.GetComponent<Asteroids>();
		Asteroid asteroid = asteroids.GetAsteroidForView(gameObject);
		if (asteroid != null) {
			asteroids.SplitAsteroid(asteroid);
			Debug.Log("HIt");
		}
	}
}
