using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Pathfinding
{
    public class Board : MonoBehaviour
    {
        //singleton
        public static Board Instance;
        public bool DrawGizmos;
        public Transform obstacleHolder;
        public static Vector2Int[] Directions = new Vector2Int[4]
        {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
        };
        public Vector2Int MinSize;
        public Vector2Int MaxSize;
        public Dictionary<Vector2Int, TileLogic> Tiles;
        public float NodeSize;

        void Awake()
        {
            //so everyone can access static stuff through static - what singleton are for
            Instance = this;
            Tiles = new Dictionary<Vector2Int, TileLogic>();
            CreateTileLogics();
            Debug.Log(Tiles.Count);
        }
        void OnDrawGizmos()
        {
            if (!DrawGizmos || Tiles == null || Tiles.Count == 0)
                return;
            Gizmos.color = Color.red;
            foreach (TileLogic t in Tiles.Values)
            {   //otherwise this
                Gizmos.DrawCube(t.WorldPosition, Vector3.one * (NodeSize / 2));
            }
        }
        public static TileLogic GetTile(Vector2Int position)
        {
            TileLogic tile;

            if (Instance.Tiles.TryGetValue(position, out tile))
            { // this is a fix if the tiles are outside the map 
                return tile;
            }
            //return null;

            return Instance.Tiles[position];
        }

        public void ClearSearch()
        {
            foreach (TileLogic t in Tiles.Values)
            {
                t.gCost = int.MaxValue;
                t.hCost = int.MaxValue;
                t.fCost = int.MaxValue;
                t.Previous = null;
            }
        }
        public TileLogic WorldPositionToTile(Vector3 position) // this is used when we call BuildPath in the AgentAstar
        {                                                      // this essentially helps get our agent position instead of setting it ourselves
            Vector3 nodePosition = position / NodeSize;
            Vector2Int pos2D = new Vector2Int(Mathf.RoundToInt(nodePosition.x), Mathf.RoundToInt(nodePosition.z));
            TileLogic toReturn = GetTile(pos2D);
            //Debug.LogFormat("From:{0}, to {1}, Tile:{2}", position, pos2D, toReturn);
            return toReturn;

        }
        void CreateTileLogics()
        {
            for (int x = MinSize.x; x < MaxSize.x; x++)
            {
                for (int y = MinSize.y; y < MaxSize.y; y++)
                {
                    TileLogic tile = new TileLogic();
                    tile.Position = new Vector2Int(x, y);
                    Tiles.Add(tile.Position, tile);
                    SetTile(tile);
                }
            }
        }

        void SetTile(TileLogic tileLogic)
        {
            {
                tileLogic.MoveCost = 1;
                tileLogic.WorldPosition = new Vector3(tileLogic.Position.x, 0, tileLogic.Position.y) * NodeSize;
                CheckCollision(tileLogic);
            }
        }
        void CheckCollision(TileLogic tileLogic)
        {
            Collider[] colliders = Physics.OverlapSphere(tileLogic.WorldPosition, NodeSize);  //summons an invisible sphere and check if its colliding with anything
            foreach (Collider col in colliders)
            {
                if (col.transform.parent == obstacleHolder)
                {
                    //                Debug.Log("Obstacle at " + tileLogic.WorldPosition);
                    tileLogic.MoveCost = int.MaxValue;
                }
            }
        }
    }

}
