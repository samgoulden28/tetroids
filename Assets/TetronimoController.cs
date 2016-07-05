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

	float gridSize = 0.3f;

    public int gridX;
    public int gridY;

    public int startX;
    public int startY;

    int[,] tetExample = new int[,]  {  { 0, 1, 0, 0 },
                                       { 0, 1, 0, 0 },
                                       { 0, 1, 0, 0 },
                                       { 0, 1, 0, 0 } };

    // Use this for initialization
    void Start () {
		game = GameObject.Find ("Game").GetComponent<GameController> ();

        gridX = startX;
        gridY = startY;

        updateGridPosition();

        game.gridScan();
    }

    public void updateGridPosition()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                print("Checking " + i + ", " + j + ": " + tetExample[i, j]);
                if (tetExample[j, i] == 1)
                {
                    game.grid[gridX + i, gridY + j] = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        timeSinceLastDrop += Time.deltaTime;

        if (timeSinceLastDrop > speed[speedLevel] || Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridY++;
            timeSinceLastDrop = 0;
            updateGridPosition();
            game.gridScan();
        }
    }
}


