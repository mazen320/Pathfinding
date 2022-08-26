using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pathfinding
{
    public class TileLogic
    {
        // Our tilelogic is our nodes
        public Vector2Int Position;
        public Vector3 WorldPosition;
        public float gCost; // usually known as gCost .is the origin, to tile - pretty much distance 
        public float hCost; // known as hCost is the distance from the tile to objective - it means heuristic
        public float fCost; // known as fCost. this is gcost + hcost
        public TileLogic Previous;
        public int MoveCost;
    }

}
