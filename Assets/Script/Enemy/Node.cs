using UnityEngine;

public class Node 
{
    public bool isWalkable;
    public Vector2 worldPosition;
    public int gridX, gridY;
    public int gCost, hCost;
    public Node parent;

    public Node(bool isWalkable, Vector2 worldPosition, int gridX, int gridY)
    {
        this.isWalkable = isWalkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }
    public int fCost
    {
        get { return gCost + hCost; }
    }
}
