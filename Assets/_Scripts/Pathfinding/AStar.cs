using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : Pathfinder // sort the list and give prirorty to the lowest score
{
    public override void Search(TileLogic start, TileLogic objective) 
    {
        tileSearch = new List<TileLogic>();
        int iterationCount = 0;

        tileSearch.Add(start);  
        Board.Instance.ClearSearch();

        TileLogic current;

        List<TileLogic> openSet = new List<TileLogic>();
        openSet.Add(start); //adding start to open list
        start.CostFromOrigin = 0;   //setting g cost at the start to 0

        while (openSet.Count > 0)
        {
            openSet.Sort((x, y) => x.Score.CompareTo(y.Score)); //getting the currentnode in open list with lowest f cost
            current = openSet[0];
            tileSearch.Add(current);

            openSet.RemoveAt(0);    //remove current from openlist

            if (current == objective)
            {
//                Debug.Log("Objective found");
                break;
            }

            for (int i = 0; i < Board.Directions.Length; i++)   //generate children/neighbours
            {
                TileLogic next = Board.GetTile(current.Position + Board.Directions[i]);
                iterationCount++;

                if (next == null || next.CostFromOrigin <= current.CostFromOrigin + next.MoveCost)
                    continue;   //so skip to next adjacent cell


                next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;   //setting f cost
                next.Previous = current;    //setting parent
                next.CostToObjective = Vector2Int.Distance(next.Position, objective.Position) * 2;  //So with a stronger Ristic, as soon as it discovers that, oh, here it is very rapidly reducing the costs to objectives.
                next.Score = next.CostFromOrigin + next.CostToObjective;

                if (!tileSearch.Contains(next)) //if not in open list 
                {
                    openSet.Add(next);  //add to open list
                }

            }
        }
    }
}

