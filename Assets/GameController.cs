﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    public List<GameObject> placedBlocks = new List<GameObject>();

    public List<Sprite> possibleSprites = new List<Sprite>();

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
        //possibleBlocks.Add(BlockDefinitions.J());
        //possibleBlocks.Add(BlockDefinitions.L());
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
        grid = new int[noOfCellsX, noOfCellsY];
		for (int x = 0; x < noOfCellsX; x++) {
			for (int y = 0; y < noOfCellsY; y++) {
                grid[x, y] = 0;
			}
		}
        printGrid();
	}

    private void printGrid()
    {
        string s = "";
        for (int y = 0; y < noOfCellsY; y++)
        {
            for (int x = 0; x < noOfCellsX; x++)
            {
                s += grid[x, y] + ", ";
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
                    placedBlocks.Add((GameObject)Instantiate(blockPrefab, new Vector3(0.3f * x + 0.15f, -0.3f * y + 0.15f), Quaternion.identity));
                    grid[x, y] = 2;
                    printGrid();
                } else
                {
                }
            }
        }
    }

    public void destroyGrid()
    {
    }

    public void spawnTetronimo()
    {
        GameObject go = (GameObject)Instantiate(tetronimoPrefab, Vector3.zero, Quaternion.identity);
        int tetronimoID = UnityEngine.Random.Range(0, possibleBlocks.Count);
        List<int[,]> tetronimo = possibleBlocks[tetronimoID];
        print(tetronimo.Count);
        go.GetComponent<TetronimoController>().tetronimo = tetronimo;

        Sprite sprite = possibleSprites[tetronimoID];
        go.GetComponent<TetronimoController>().tetronimoRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update () {
	}
}
