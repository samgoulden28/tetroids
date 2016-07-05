using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public Sprite spriteI;

    private bool placed;

    int[,] lineRotation1 = new int[,]  {  { 1, 1, 1, 1 },
                                          { 0, 0, 0, 0 },
                                          { 0, 0, 0, 0 },
                                          { 0, 0, 0, 0 } };

    int[,] lineRotation2 = new int[,]  {  { 1, 0, 0, 0 },
                                          { 1, 0, 0, 0 },
                                          { 1, 0, 0, 0 },
                                          { 1, 0, 0, 0 } };

    int[,] currentRotation;

    List<GameObject> renderedBlocks = new List<GameObject>();

    // Use this for initialization
    void Start () {
		game = GameObject.Find ("Game").GetComponent<GameController> ();
        currentRotation = lineRotation1;
        GetComponent<SpriteRenderer>().sprite = spriteI;
        gridX = startX;
        gridY = startY;

        updateRenderedPosition();
    }

    public void place()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (currentRotation[j, i] == 1)
                {
                    game.grid[gridX + i, gridY + j] = 1;
                }
            }
        }
        game.spawnTetronimo();
        game.gridScan();
        Destroy(this.gameObject);
    }

    public bool nothingUnderneath()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (currentRotation[j, i] == 1)
                {
                    if (gridY + j + 1 > game.noOfCellsY - 1 || game.grid[gridX + i, gridY + j + 1] > 0)
                    {
                        print("PLACE!");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool canMoveRight()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (currentRotation[j, i] == 1)
                {
                    if (gridX + 1 > game.noOfCellsX - 1 || game.grid[gridX + 1, gridY] > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool canMoveLeft()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (currentRotation[j, i] == 1)
                {
                    if (gridX == 0 || game.grid[gridX - 1, gridY] > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void updateRenderedPosition()
    {
            Vector3 pos = new Vector3(0.3f * gridX + 0.15f, -0.3f * gridY + 0.15f);
            transform.position = pos;
    }

    // Update is called once per frame
    void Update () {
        timeSinceLastDrop += Time.deltaTime;

        if ((timeSinceLastDrop > speed[speedLevel] || Input.GetKeyDown(KeyCode.DownArrow))&& !placed)
        {
            if(nothingUnderneath())
            {
                gridY++;
                timeSinceLastDrop = 0;
                updateRenderedPosition();
            } else
            {
                placed = true;
                place();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (canMoveRight())
            {
                gridX++;
                updateRenderedPosition();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (canMoveLeft())
            {
                gridX--;
                updateRenderedPosition();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentRotation == lineRotation1)
            {
                currentRotation = lineRotation2;
                updateRenderedPosition();
            }

            if (currentRotation == lineRotation2)
            {
                currentRotation = lineRotation1;
                updateRenderedPosition();
            }
        }
    }
}


