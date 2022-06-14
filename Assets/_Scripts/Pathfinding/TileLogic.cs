using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    // Our tilelogic is our nodes
    public Vector2Int Position;
    public Vector3 WorldPosition;
    public float CostFromOrigin; // usually known as gCost .is the origin, to tile - pretty much distance 
    public float CostToObjective; // known as hCost is the distance from the tile to objective - it means heuristic
    public float Score; // known as fCost. this is gcost + hcost
    public TileLogic Previous;
    public int MoveCost;
}
