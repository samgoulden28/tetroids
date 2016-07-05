using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 1f;
	public float maxRange = 10f;

	private Vector3 startingPosition;

	void Start() {
		startingPosition = transform.position;
	}

	void Update () {
		Vector3 newPosition = transform.position + transform.up * speed * Time.deltaTime;
		if (Vector2.Distance(startingPosition, transform.position) > maxRange) {
			Destroy(gameObject);
		} else {
			RaycastHit2D hit = Physics2D.Linecast(transform.position, newPosition, Asteroids.asteroidsLayerMask);
			if (hit) {
				Destroy(gameObject);
				hit.collider.gameObject.SendMessage("Hit");
			} else {
				transform.position = newPosition;
			}
		}
	}
}
