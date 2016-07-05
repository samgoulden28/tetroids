﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<GameObject> blocks = new List<GameObject>();

    public int[,] grid;

	// Use this for initialization
	void Start () {
		setupGrid ();
		spawnTetronimo ();
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
        grid = new int[noOfCellsX, noOfCellsY];
		for (int x = 0; x < noOfCellsX; x++) {
			for (int y = 0; y < noOfCellsY; y++) {
                grid[x, y] = 0;
			}
		}
	}

    public void gridScan()
    {
        destroyGrid();
        blocks = new List<GameObject>();
        for (int x = 0; x < noOfCellsX; x++)
        {
            for (int y = 0; y < noOfCellsY; y++)
            {
                if (grid[x, y] == 1) {
                    Debug.Log("grid " + x + ", " + y + " Filled ");
                    blocks.Add((GameObject)Instantiate(blockPrefab, new Vector3(0.3f * x + 0.15f, -0.3f * y + 0.15f), Quaternion.identity));
                } else
                {
                    Debug.Log("grid " + x + ", " + y + " not filled ");
                }
            }
        }
    }

    public void destroyGrid()
    {
        foreach(GameObject go in blocks)
        {
            Destroy(go);
        }
    }

    public void spawnTetronimo()
    {
        Instantiate(tetronimoPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
	}
}
