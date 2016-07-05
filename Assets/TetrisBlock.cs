using UnityEngine;
using System.Collections;

public class TetrisBlock : MonoBehaviour {
	public GameObject gameGO;
	GameController game;

	public int relativeX;
	public int relativeY;

	public int gridX;
	public int gridY;

	// Use this for initialization
	void Start () {
		gameGO = GameObject.Find ("Game");
		game = gameGO.GetComponent<GameController> ();
	}

	public void updatePosition () {
		transform.position = game.grid [gridX + relativeX, gridY + relativeY].getPosition();
	}
	
	// Update is called once per frame
	void Update () {
		updatePosition ();
	}

	public bool blockUnderneathFull() {
		if (gridY <= 0)
			return true;
		else {
			return game.grid[gridX, gridY - 1].getObject() != null;
		}
	}

	public bool canMoveLeft() {
		return (gridX <= 0 || game.grid [gridX - 1, gridY].getObject() != null);
	}

	public bool canMoveRight() {
		return (gridX >= game.grid.GetLength(0) - 1 || game.grid [gridX + 1, gridY].getObject() != null);
	}
}
