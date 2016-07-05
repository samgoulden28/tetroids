using UnityEngine;
using System.Collections;

public class TetronimoSpawner : MonoBehaviour {
	public GameObject[] tetronimoGameObjects;
	private Vector3 startPosition = Vector3.zero;

	TetronimoController activeTetronimo;

	public GameObject SpawnTetronimo() {
		int randomNumber = Random.Range (0, tetronimoGameObjects.Length-1);
		GameObject go = (GameObject)Instantiate (tetronimoGameObjects[randomNumber], startPosition, Quaternion.identity);
		return go;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}