using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Asteroid {
    public Asteroids container;
    public Vector2 position;
    public Vector2 velocity;
    public float rotation;
	public int size;
    public GameObject view;

    public Asteroid(Asteroids container,  Vector2 position, Vector2 velocity, float rotation, int size, GameObject view) {
        this.container = container;
        this.position = position;
        this.velocity = velocity;
        this.rotation = rotation;
		this.size = size;
        this.view = view;
        view.transform.localPosition = position;
        view.transform.rotation = Quaternion.Euler(0, 0, rotation);
        view.GetComponent<SpriteRenderer>().sprite = 
			size == 2 ? container.LARGE :
			size == 1 ? container.MEDIUM :
			container.SMALL;
		view.GetComponent<CircleCollider2D>().radius = 
			size == 2 ? 0.57f:
			size == 1 ? 0.42f :
			0.24f;
		// TODO: resize collider
    }

    public void Update() {
        position += velocity * Time.deltaTime;

        // TODO: real wrapping
        if (position.x >  container.worldDimension.x) position.x -= 2f * container.worldDimension.x;
        if (position.x < -container.worldDimension.x) position.x += 2f * container.worldDimension.x;
        if (position.y >  container.worldDimension.y) position.y -= 2f * container.worldDimension.y;
        if (position.y < -container.worldDimension.y) position.y += 2f * container.worldDimension.y;

        view.transform.localPosition = position;
        view.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}

public class Asteroids : MonoBehaviour {

    public int asteroidCount = 5;
    public Vector2 worldDimension;
    public GameObject asteroidPrefab;
	public Sprite SMALL, MEDIUM, LARGE;
	public GameObject explosionParticlesPrefab;
	public AudioClip destroySound;

	public static int asteroidsLayerMask = LayerMask.GetMask(new string[] {"Water"});

    private List<Asteroid> asteroids = new List<Asteroid>();
    private List<GameObject> pool = new List<GameObject>();

	Vector2 RandomPositionOnEdge() {
		Vector2 halfDim = worldDimension / 2f;
		Vector2 randomDistanceAlongEdge = worldDimension * Random.value - (worldDimension / 2f);
		switch (Random.Range(1, 4)) {
		case 1: return new Vector2( halfDim.x, randomDistanceAlongEdge.y); break;
		case 2: return new Vector2(-halfDim.x, randomDistanceAlongEdge.y); break;
		case 3: return new Vector2(randomDistanceAlongEdge.x,  halfDim.y); break;
		case 4: return new Vector2(randomDistanceAlongEdge.x, -halfDim.y); break;
		default:
			return Vector2.zero;
		}
	}

    void Start() {
        float kick = 0.5f;
        for (int i=0; i < asteroidCount; i++) {
			Vector2 position = RandomPositionOnEdge();
            Vector2 velocity = kick * Random.insideUnitCircle;
            float rotation = 360f * Random.value;
            asteroids.Add(new Asteroid(this, position, velocity, rotation, 2, GetView()));
        }
    }
	
	void Update () {
	    foreach (Asteroid asteroid in asteroids) {
            asteroid.Update();
        }

        DebugDrawBoxCenter(transform.position, worldDimension);
    }

    void DebugDrawBoxCenter(Vector3 center, Vector2 dimension) {
        Vector3 p1 = center + new Vector3(-dimension.x, dimension.y);
        Vector3 p2 = center + new Vector3(dimension.x, dimension.y);
        Vector3 p3 = center + new Vector3(dimension.x, -dimension.y);
        Vector3 p4 = center + new Vector3(-dimension.x, -dimension.y);

        Debug.DrawLine(p1, p2);
        Debug.DrawLine(p2, p3);
        Debug.DrawLine(p3, p4);
        Debug.DrawLine(p4, p1);
    }

    GameObject GetView() {
        if (pool.Count > 0) {
            GameObject view = pool[0];
            pool.RemoveAt(0);
			view.SetActive(true);
            return view;
        } else {
            GameObject view = (GameObject)Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            view.transform.parent = transform;
            return view;
        }
    }

    public void SplitAsteroid(Asteroid asteroid) {
		if (asteroid.size > 0) {
			int pieces = 4 - asteroid.size;
			float splitMultiplier = 0.5f;
			float kick = 0.5f;
			for (int i = 0; i < pieces; i++) {
				Vector2 spawnOffset = Random.insideUnitCircle;
				Vector2 spawnImpulse = kick * spawnOffset.normalized;
				asteroids.Add(new Asteroid(this,
					asteroid.position + spawnOffset * splitMultiplier, 
					asteroid.velocity + spawnImpulse, 
					360f * Random.value,
					asteroid.size - 1,
					GetView()));
			}
		}
        DestroyAsteroid(asteroid);
		Instantiate(explosionParticlesPrefab, asteroid.position, Quaternion.identity);
		GetComponent<AudioSource>().PlayOneShot(destroySound);
    }

	public Asteroid GetAsteroidForView(GameObject view) {
		return asteroids.Find(asteroid => asteroid.view == view);
	}

    void DestroyAsteroid(Asteroid asteroid) {
        pool.Add(asteroid.view);
		asteroid.view.SetActive(false);
        asteroid.view = null;
        asteroids.Remove(asteroid);
    }
}
