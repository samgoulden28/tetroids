using UnityEngine;
using System.Collections;

public class TetronimoController : MonoBehaviour {
	GameController game;
	bool controlling = true;
	public float[] rotations;
	int rotationCounter = 1;

	public float[] speed = { 1f, 0.75f, 0.5f, 0.25f, 0.2f, 0.15f, 0.1f, 0.05f };
	private float timeSinceLastDrop;
	public int speedLevel;

	public TetrisBlock[] blocks;

	float gridSize = 0.3f;
	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game").GetComponent<GameController> ();

		foreach (TetrisBlock tb in blocks) {
			tb.gridX = game.gridPositionFromXPos (tb.transform.position.x) + 5;
			tb.gridY = game.gridPositionFromYPos (tb.transform.position.y) + 9;

			float tbX = tb.transform.localPosition.x / gridSize;
			float tbY = tb.transform.localPosition.y / gridSize;

			tb.relativeX = Mathf.RoundToInt(tbX);
			tb.relativeY = Mathf.RoundToInt(tbY);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!controlling) {
			return;
		}
		Vector2 pos = transform.position;

		timeSinceLastDrop += Time.deltaTime;
		if (timeSinceLastDrop > speed [speedLevel] || Input.GetKeyDown(KeyCode.DownArrow)) {
			for (int i = 0; i < blocks.Length; i++) {
				if (blocks [i].blockUnderneathFull()) {
					Place ();
					return;
				}
			}
			pos.y -= gridSize;
			timeSinceLastDrop = 0;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			for (int i = 0; i < blocks.Length; i++) {
				if (blocks [i].canMoveRight()) {
					break;
				}
				if (i == blocks.Length - 1) {
					pos.x += gridSize;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			for (int i = 0; i < blocks.Length; i++) {
				if (blocks [i].canMoveLeft()) {
					break;
				}
				if (i == blocks.Length - 1) {
					pos.x -= gridSize;
				}
			}
		}

		int oldRotationCounter = rotationCounter;
		if (Input.GetKeyDown (KeyCode.A)) {
			rotationCounter = (rotationCounter + 1) % rotations.Length;
		}
		if ( Input.GetKeyDown(KeyCode.D)) {
			rotationCounter = (rotationCounter + rotations.Length - 1) % rotations.Length;
		}

		transform.rotation = Quaternion.Euler(0, 0, rotations[rotationCounter]);

		for (int i = 0; i < blocks.Length; i++) {
			if (blocks [i].gridX >= game.grid.GetLength(0) || blocks [i].gridX < 0 ||
				game.grid[blocks [i].gridX, blocks [i].gridY].getObject() != null) {
				rotationCounter = oldRotationCounter;
				transform.rotation = Quaternion.Euler (0, 0, rotations [rotationCounter]);
				break;
			}
		}

		transform.position = pos;
	}

	void deleteLine (int rowToDelete)
	{
		for (int x = 0; x < game.grid.GetLength(0); x++) {
			GameObject goToDestroy = game.grid [x, rowToDelete].getObject();
			for (int y = rowToDelete; y > 0; y--) {
				game.grid [x, y].assignObject(game.grid [x, y - 1].getObject());
				game.grid [x, y].getObject ().transform.position = game.grid [x, y].getPosition ();
				//TODO: This isnt working because the grid position x,y is holding the reference to the gameobject that is in the 
				//cell above it, however the game object its self hasnt moved. I can see two ways of fixing this:
				//1. Loop through the entire grid creating gameobjects for cells that are on and deleting ones that arent.
				//2. Use mikes suggestion and start placing blocks at a grid position, rather than looking at their transform and then calculating
				//the grid position aftewards.
			}

			GameObject.Destroy (goToDestroy);
		}

	}

	public void Place() {
		for (int i = 0; i < blocks.Length; i++) {
			game.grid [blocks [i].gridX, blocks [i].gridY].assignObject((GameObject)Instantiate (game.blockPrefab, blocks [i].transform.position, Quaternion.identity));
		}

		for (int y = 0; y < game.grid.GetLength(1); y++) {
			bool completeLine = true;
			for (int x = 0; x < game.grid.GetLength (0); x++) {
				if (game.grid [x, y].getObject() == null) {
					completeLine = false;
					break;
				}
			}
			if (completeLine) {
				deleteLine (y);
			}
		}

		game.spawnTetronimo (0, 4);
		GameObject.Destroy (this.gameObject);
	}
}


