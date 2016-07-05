using UnityEngine;
using System.Collections;

public class DestoryAtSimEnd : MonoBehaviour {
	void Update () {
		if (!GetComponent<ParticleSystem>().isPlaying) {
			Destroy(gameObject);
		}
	}
}
