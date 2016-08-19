using UnityEngine;
using System.Collections;

public class FlyIn : MonoBehaviour {
	float lerpTime = 1f;
	float currentLerpTime;

	float moveDistance = 10f;

	Vector3 startPos;
	public Vector3 endPos;

	protected void Start() {
		startPos = transform.position;
	}

	protected void Update() {
		//reset when we press spacebar
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentLerpTime = 0f;
		}

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		float perc = currentLerpTime / lerpTime;
		perc = Mathf.Sin (perc * Mathf.PI * 0.5f);
		transform.position = Vector3.Lerp(startPos, endPos, perc);
	}
}
