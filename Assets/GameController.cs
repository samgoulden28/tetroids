using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Vector3 startPosition;
	public GameObject blockPrefab;
	int noOfCellsX = 10;
	int noOfCellsY = 18;

	public GridCell[,] grid;
	float gridSize = 0.3f;

	float gridWidth;
	float gridHeight;

	public GameObject[] tetronimos;

	// Use this for initialization
	void Start () {
		grid = new GridCell[noOfCellsX, noOfCellsY];
		setupGrid ();
		gridWidth = gridSize * noOfCellsX;
		gridHeight = gridSize * noOfCellsY;
		startPosition = new Vector3 (-0.15f, 0.15f, -2);
		spawnTetronimo (0, 4);
	}

	private void setupGrid() {
		for (int x = 0; x < noOfCellsX; x++) {
			for (int y = 0; y < noOfCellsY; y++) {
				Vector3 position = new Vector3 (-0.15f + (0.3f * x), 0.15f + (0.3f * y), -2);
				grid [x, y] = new GridCell (position);
			}
		}
	}

	public void spawnTetronimo(int x, int y) {
		int r = Random.Range (0, tetronimos.Length);
		GameObject go = (GameObject)GameObject.Instantiate (tetronimos [r], grid[x, y].getPosition(), Quaternion.identity); //This could be better if you have the start position as a member variable of the tetronimo
	}
	
	// Update is called once per frame
	void Update () {
	}

	public int gridPositionFromXPos(float xPos) {
		return Mathf.FloorToInt ((xPos / gridWidth) * noOfCellsX);
	}

	public int gridPositionFromYPos(float yPos) {
		return Mathf.FloorToInt ((yPos / gridHeight) * noOfCellsY);
	}
}

public class GridCell {
	bool occupado = false;
	GameObject cellObject;
	Vector3 position;

	public GridCell(Vector3 position) {
		this.position = position;
	}

	public void assignObject(GameObject go) {
		cellObject = go;
	}

	public GameObject getObject() {
		return cellObject;
	}

	public Vector3 getPosition() {
		return position;
	}
}
