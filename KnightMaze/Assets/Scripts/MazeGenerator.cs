using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible of generating a new maze when the level starts
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    /// <summary>
    /// walls ratio inside the maze
    /// </summary>
    [SerializeField]
    private float placementThreshold;

    [SerializeField]
    private GameObject pathTile;

    [SerializeField]
    private GameObject wallTile;

    [SerializeField]
    private Transform mazeContainer;

    public List<Transform> PathTilesList { get; private set; }

    public GameObject StartPathBlock { get; set; }

    public GameObject EndPathBlock { get; set; }

    public int[,] MazeData { get; private set; }

    private float mazeWidth;

    /// <summary>
    /// Generates new maze whenever the level starts
    /// </summary>
    /// <param name="height"></param>
    /// <param name="width"></param>
    public void GenerateNewMaze(int height, int width)
    {
        this.mazeWidth = width;

        this.PathTilesList = new List<Transform>();

        this.MazeData = this.SetDimensions(height, width);
        
        int rMax = this.MazeData.GetUpperBound(0);
        int cMax = this.MazeData.GetUpperBound(1);

        var currentPosition = new Vector3(0, 0, 1);

        for (int j = 0; j <= cMax; j++)
        {
            for (int i = 0; i <= rMax; i++)
            {
                if(this.MazeData[i,j] == 1)
                {
                    var tile = Instantiate(this.wallTile, currentPosition, Quaternion.identity, this.mazeContainer);
                }
                else
                {
                    var tile = Instantiate(this.pathTile, currentPosition, Quaternion.identity, this.mazeContainer);
                    this.PathTilesList.Add(tile.transform);
                }

                currentPosition = this.GetNextPosition(currentPosition);
            }
        }
    }

    /// <summary>
    /// Sets maze dimensions based on the given height and width.
    /// Setting the array elements 1 for walls 0 for paths, maze boundaries, walls inside the maze
    /// </summary>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <returns></returns>
    private int[,] SetDimensions(int height,int width)
    {
        var maze = new int[height, width];

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                } 
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        maze[i, j] = 1;
                        //adding a block to an adjacent cell
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }
        return maze;
    }

    /// <summary>
    /// Gets the next position to instatiate the tile
    /// </summary>
    /// <param name="currentPos"></param>
    /// <returns></returns>
    private Vector3 GetNextPosition(Vector3 currentPosition)
    {
        var nextPosition = currentPosition;

        if (currentPosition.x > (this.mazeWidth * this.wallTile.transform.localScale.x))
        {
            nextPosition = new Vector3(0, currentPosition.y - this.wallTile.transform.localScale.y, 1);
        }
        else
        {
            nextPosition = new Vector3(currentPosition.x + this.wallTile.transform.localScale.x, currentPosition.y, 1);
        }

        return nextPosition;
    }
}
