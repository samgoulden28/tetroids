using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public float acceleration = 5f;
	public float maxSpeed = 2;
	public float angularSpeed = 10f;
	public float rateOfFire = 3f;
	public float minThrustTime = 0.2f;

	public GameObject bulletPrefab;
	public Transform muzzle;
	public ParticleSystem thrustParticles;
	public GameObject deathParticles;

	public AudioClip[] fireSounds;
	public AudioClip deathSound;

	private Asteroids asteroids;
	private Vector2 velocity = Vector2.zero;
	private float fireCooldown = 0f;
	private float thrustCooldown = 0f;

	void Start() {
		asteroids = GameObject.Find("Asteroids").GetComponent<Asteroids>();
	}

	void Left() {
		transform.Rotate(new Vector3(0, 0, angularSpeed));
	}

	void Right() {
		transform.Rotate(new Vector3(0, 0, -angularSpeed));
	}

	void Forward() {
		velocity += (Vector2)transform.up * acceleration * Time.deltaTime;
		Vector2.ClampMagnitude(velocity, maxSpeed);
	}

	void Fire() {
		Instantiate(bulletPrefab, muzzle.position, transform.rotation);
		int soundIdx = Random.Range(0, fireSounds.Length - 1);
		asteroids.GetComponent<AudioSource>().PlayOneShot(fireSounds[soundIdx]);
		Debug.Log("Pew pew!");
	}

	void Die() {
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		asteroids.GetComponent<AudioSource>().PlayOneShot(deathSound);
		Destroy(gameObject);
	}

	void Update () {
		// TODO: Movement sounds
		if (Input.GetKey(KeyCode.A)) Left();
		if (Input.GetKey(KeyCode.D)) Right();
		if (Input.GetKey(KeyCode.W)) {
			Forward();
			if (!thrustParticles.isPlaying) {
				thrustParticles.Play();
				GetComponent<AudioSource>().volume = 1.0f;
				GetComponent<AudioSource>().Play();
			}
			thrustCooldown = minThrustTime;
		} else if (thrustParticles.isPlaying) {
			if (thrustCooldown <= 0f) {
				thrustParticles.Stop();
				GetComponent<AudioSource>().Stop();
			} else {
				GetComponent<AudioSource>().volume = thrustCooldown / minThrustTime;
				thrustCooldown -= Time.deltaTime;
			}
		}
		if (fireCooldown <= 0f) {
			if (Input.GetKey(KeyCode.Space)) {
				fireCooldown = 1f / rateOfFire;
				Fire();
			}
		} else {
			fireCooldown -= Time.deltaTime;
		}

		transform.position += (Vector3)velocity * Time.deltaTime;

		if (Physics2D.OverlapCircle(transform.position, 0.2f, Asteroids.asteroidsLayerMask)) {
            Die();
        }

		// TODO: real wrapping
		if (transform.parent) {
			Asteroids container = transform.parent.GetComponent<Asteroids>();
			Vector3 position = transform.localPosition;
			if (position.x >  container.worldDimension.x) position.x -= 2f * container.worldDimension.x;
			if (position.x < -container.worldDimension.x) position.x += 2f * container.worldDimension.x;
			if (position.y >  container.worldDimension.y) position.y -= 2f * container.worldDimension.y;
			if (position.y < -container.worldDimension.y) position.y += 2f * container.worldDimension.y;
			transform.localPosition = position;
		}
	}
}
