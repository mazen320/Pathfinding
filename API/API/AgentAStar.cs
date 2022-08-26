using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pathfinding
{ 
    public class AgentAStar : MonoBehaviour
    {
        /*      FIELDS
        */
        public const float DistanceToChangeWaypoint = 2;
        public AStar AStar;
        public float Speed = 2;
        public Vector2Int CurrentPosition;
        public Transform Objective;
        bool followingPath;
        List<TileLogic> path;
        Rigidbody rb;
        int currentWaypoint;

        /*      UNITY EVENTS
        */
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (!followingPath)
            {
                return;
            }
            Move();
            CheckWaypoint();
        }

        /*      METHODS
        */
        public void FollowPath()    // the player's position was not updating so i made this script
        {
            CancelInvoke();
            InvokeRepeating("BuildPath", 0, 2);
            //BuildPath()
        }
        void BuildPath()
        {
            TileLogic currentTile = Board.Instance.WorldPositionToTile(rb.position);
            TileLogic targetTile = Board.Instance.WorldPositionToTile(Objective.position);
            currentWaypoint = 0;
            AStar.Search(currentTile, targetTile);
            path = AStar.BuildPath(targetTile);
            followingPath = true;
        }
        void Move()
        {
            Vector3 targetDirection = path[currentWaypoint].WorldPosition - rb.position;
            rb.MovePosition(rb.position + (targetDirection * Time.fixedDeltaTime * Speed));
        }
        void CheckWaypoint()
        {
            if (Vector3.Distance(rb.position, path[currentWaypoint].WorldPosition) < DistanceToChangeWaypoint)
            {
                CurrentPosition = path[currentWaypoint].Position;
                currentWaypoint++;
                if (currentWaypoint == path.Count)   //if we reached the end the path, and we change the following to false
                {
                    followingPath = false;
                }
            }
        }
    }

}
