using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pathfinding
{
    public class AgentController : MonoBehaviour
    {
        List<AgentAStar> agents;
        // Start is called before the first frame update
        void Awake()
        {
            agents = new List<AgentAStar>();
            agents.AddRange(GetComponentsInChildren<AgentAStar>());
            VeryLateStart();
        }
        public void VeryLateStart()
        {
            foreach (AgentAStar agent in agents)
            {
                agent.FollowPath();
            }
        }
    }

}
