using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetronimoController : MonoBehaviour {
	GameController game;
	int rotationCounter = 0;

	public float[] speed = { 1f, 0.75f, 0.5f, 0.25f, 0.2f, 0.15f, 0.1f, 0.05f };
	private float timeSinceLastDrop;
	public int speedLevel;

	float gridSize = 0.3f;

    public int gridX;
    public int gridY;

    public int startX;
    public int startY;

    private bool placed;

    public List<int[,]> tetronimo;
    int[,] tetronimoCurrentRotation;

    public SpriteRenderer tetronimoRenderer;
    public Sprite[] sprites = new Sprite[4];

    // Use this for initialization
    void Start () {
		game = GameObject.Find ("Game").GetComponent<GameController> ();
        tetronimoCurrentRotation = tetronimo[rotationCounter];
        tetronimoRenderer.sprite = sprites[rotationCounter];
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
                if (tetronimoCurrentRotation[j, i] == 1)
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
                if (tetronimoCurrentRotation[j, i] == 1)
                {
                    if (gridY + j + 1 > game.noOfCellsY - 1 || game.grid[gridX + i, gridY + j + 1] > 0)
                    {
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
                if (tetronimoCurrentRotation[j, i] == 1)
                {
                    if (gridX + i + 1 > game.noOfCellsX - 1 || game.grid[gridX + i + 1, gridY + j] > 0)
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
                if (tetronimoCurrentRotation[j, i] == 1)
                {
                    if (gridX + i == 0 || game.grid[gridX + i - 1, gridY + j] > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool positionOutOfBounds(int[,] futureRotation)
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (futureRotation[j, i] == 1)
                {
                    if (gridX + i < 0 || gridX + i >= game.noOfCellsX)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void updateRenderedPosition()
    {
            Vector3 pos = new Vector3(gridSize * gridX + (gridSize / 2), -gridSize * gridY + (gridSize/2));
            transform.position = pos;
    }

    // Update is called once per frame
    void Update () {
        timeSinceLastDrop += Time.deltaTime;

        if ((timeSinceLastDrop > speed[speedLevel] || Input.GetKeyDown(KeyCode.DownArrow)) && !placed)
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            int potentialRotationCounter = (rotationCounter + 1) % tetronimo.Count;
            if(!positionOutOfBounds(tetronimo[potentialRotationCounter])) {
                rotationCounter = potentialRotationCounter;
                //rotationCounter++;
                tetronimoCurrentRotation = tetronimo[rotationCounter];
                //For rendering.
                tetronimoRenderer.sprite = sprites[rotationCounter];
            } else
            {
                print("Position would have been out of bounds!");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            int potentialRotationCounter = (rotationCounter + tetronimo.Count - 1) % tetronimo.Count;
            if (!positionOutOfBounds(tetronimo[potentialRotationCounter]))
            {
                rotationCounter = potentialRotationCounter;
                //rotationCounter++;
                tetronimoCurrentRotation = tetronimo[rotationCounter];
                //For rendering.
                tetronimoRenderer.sprite = sprites[rotationCounter];
            }
            else
            {
                print("Position would have been out of bounds!");
            }
        }
    }
}