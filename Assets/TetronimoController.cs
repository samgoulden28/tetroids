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

    int[,] tetExample = new int[,]  {  { 1, 1, 1, 1 },
                                       { 0, 0, 0, 0 },
                                       { 0, 0, 0, 0 },
                                       { 0, 0, 0, 0 } };

    List<GameObject> renderedBlocks = new List<GameObject>();

    // Use this for initialization
    void Start () {

		game = GameObject.Find ("Game").GetComponent<GameController> ();
        GetComponent<SpriteRenderer>().sprite = spriteI;
        gridX = startX;
        gridY = startY;

        updateRenderedPosition();
        game.gridScan();
    }

    public void place()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (tetExample[j, i] == 1)
                {
                    game.grid[gridX + i, gridY + j] = 1;
                }
            }
        }
        game.spawnTetronimo();
        Destroy(this.gameObject);
    }

    public bool nothingUnderneath()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (tetExample[j, i] == 1)
                {
                    if (gridY + 1 > game.noOfCellsY - 1 || game.grid[gridX, gridY + 1] == 1)
                    {
                        print("PLACE!");
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
    }
}


