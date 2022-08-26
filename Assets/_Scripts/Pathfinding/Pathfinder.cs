using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    public Vector2Int ObjectivePosition;
    public int SearchLength;
    protected List<TileLogic> tileSearch;

    [ContextMenu("Print Path")]
    void TriggerPrintPath()
    {
        TileLogic objective = Board.GetTile(ObjectivePosition);
        if (tileSearch.Contains(objective))
        {
            List<TileLogic> path = BuildPath(objective);
            PrintPath(path);
        }
        else
        {
            Debug.Log("Objective not found");
        }
    }
    public abstract void Search(TileLogic start, TileLogic objective);

    public List<TileLogic> BuildPath(TileLogic lastTile)
    {
        List<TileLogic> path = new List<TileLogic>();
        TileLogic temp = lastTile;
        while (temp.Previous != null)
        {
            path.Add(temp);
            temp = temp.Previous;
        }
        path.Add(temp);
        path.Reverse(); //retrace We are only going to need a temporary list to hold our intermediate results during the processing.
                        //this is to ensure that our enemy moves from the beginning till the end of the established route.
        return path;
    }
    void PrintPath(List<TileLogic> path)
    {
        foreach (TileLogic t in path)
        {
            Debug.Log(t.Position);
        }
    }
    /*
    protected bool ValidateMovement(TileLogic from, TileLogic to)
    {
        if (to.CostFromOrigin > SearchLength)
        {
            return false;
        }
        return true;
    }*/
}
