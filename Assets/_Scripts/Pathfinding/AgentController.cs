using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    List<AgentAStar> agents;
    // Start is called before the first frame update
    void Awake()
    {
        agents = new List<AgentAStar>();
        agents.AddRange(GetComponentsInChildren<AgentAStar>());
        Invoke("VeryLateStart", 3);
    }
    public void VeryLateStart()
    {
        foreach(AgentAStar agent in agents)
        {
            agent.FollowPath();
        }
    }
}
