using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class GameController : MonoBehaviour {
	public Vector3 startPosition;
	public GameObject blockPrefab;
    public GameObject tetronimoPrefab;

    public int noOfCellsX = 10;
	public int noOfCellsY = 18;

	//public GridCell[,] grid;
	float gridSize = 0.3f;

	float gridWidth;
	float gridHeight;

    List<List<int[,]>> possibleBlocks = new List<List<int[,]>>();
    public GameObject[,] placedBlocks;

    private char[] possibleTetronimosForSpriteLoading = { 'I', 'J', 'L' };
    public int[,] grid;

	// Use this for initialization
	void Start () {
        setupTetronimos();
		setupGrid ();
        spawnTetronimo ();
    }

    private void setupTetronimos()
    {
        possibleBlocks.Add(BlockDefinitions.I());
        possibleBlocks.Add(BlockDefinitions.J());
        possibleBlocks.Add(BlockDefinitions.L());
        //possibleBlocks.Add(BlockDefinitions.O());
        //possibleBlocks.Add(BlockDefinitions.S());
        //possibleBlocks.Add(BlockDefinitions.T());
        //possibleBlocks.Add(BlockDefinitions.Z());
    }

    /* this gives us:
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     * 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
     */

    private void setupGrid() {
        placedBlocks = new GameObject[noOfCellsX, noOfCellsY];
        grid = new int[noOfCellsX, noOfCellsY];
		for (int x = 0; x < noOfCellsX; x++) {
			for (int y = 0; y < noOfCellsY; y++) {
                grid[x, y] = 0;
			}
		}
        printGrid();
	}

    public void checkForCompletedLines()
    {
        for (int y = 0; y < noOfCellsY; y++)
        {
            bool lineCompleted = true;
            for (int x = 0; x < noOfCellsX; x++)
            {
                if (grid[x, y] == 0)
                {
                    lineCompleted = false;
                    break;
                }
            }
            if (lineCompleted)
            {
                for (int x = 0; x < noOfCellsX; x++)
                {
                    GameObject.Destroy(placedBlocks[x, y]);
                }
                for (int y1 = y; y1 >= 0; y1--)
                {
                    for (int x = 0; x < noOfCellsX; x++)
                    {
                        if (y1 == 0)
                        {
                            placedBlocks[x, y1] = null;
                            grid[x, y1] = 0;
                        }
                        else
                        {
                            if (placedBlocks[x, y1 - 1]) placedBlocks[x, y1 - 1].GetComponent<PlacedBlock>().moveDown();
                            placedBlocks[x, y1] = placedBlocks[x, y1 - 1];
                            grid[x, y1] = grid[x, y1 - 1];
                        }
                    }
                }
                y--;
            }
        }
    }

    private void printGrid()
    {
        string s = "";
        for (int y = 0; y < noOfCellsY; y++)
        {
            for (int x = 0; x < noOfCellsX; x++)
            {
                s += x + ", " + y + ": |" + grid[x, y];
                if (placedBlocks[x, y])
                    s += "x";
                s += "| ";
            }
            s += "\n";
        }
        print(s);
    }

    public void gridScan()
    {
        for (int x = 0; x < noOfCellsX; x++)
        {
            for (int y = 0; y < noOfCellsY; y++)
            {
                if (grid[x, y] == 1) {
                    GameObject go = (GameObject)Instantiate(blockPrefab, new Vector3(0.3f * x + 0.15f, -0.3f * y + 0.15f), Quaternion.identity);
                    PlacedBlock pb = go.GetComponent<PlacedBlock>();
                    pb.gridX = x;
                    pb.gridY = y;
                    placedBlocks[x, y] = go;

                    //Specify that this spot already has a game object in it.
                    grid[x, y] = 2;
                } 
            }
        }
        checkForCompletedLines();
        printGrid();
    }

    public void destroyGrid()
    {
    }

    public void spawnTetronimo()
    {
        GameObject go = (GameObject)Instantiate(tetronimoPrefab, Vector3.zero, Quaternion.identity);
        int tetronimoID = UnityEngine.Random.Range(0, possibleBlocks.Count);
        List<int[,]> tetronimo = possibleBlocks[tetronimoID];
        go.GetComponent<TetronimoController>().tetronimo = tetronimo;

        char spritesToLoad = possibleTetronimosForSpriteLoading[tetronimoID];

        Sprite[] sprites = new Sprite[4];

        for (int i = 0; i < 4; i++)
        {
            String source_file = "tetronimo_images/" + spritesToLoad + "/TetronimoRotation" + (i+1);
            source_file = Regex.Replace(source_file, @"\s+", "").ToLower();
            print("Loading: " + source_file);
            sprites[i] = (Sprite)Resources.Load(source_file, typeof(Sprite));
        }
        
        go.GetComponent<TetronimoController>().sprites = sprites;
    }

    // Update is called once per frame
    void Update () {
	}
}


