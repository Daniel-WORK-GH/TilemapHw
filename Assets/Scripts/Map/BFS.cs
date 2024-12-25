using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public class BFS
{
    public static void FindPath<NodeType>(
            IGraph<NodeType> graph, 
            NodeType startNode, NodeType endNode, 
            List<NodeType> outputPath, int maxiterations=1000)
    {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int i; 

        for (i = 0; i < maxiterations; ++i)
        { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0)
            {
                break;
            }
            else
            {
                NodeType searchFocus = openQueue.Dequeue();

                if (searchFocus.Equals(endNode))
                {
                    // We found the target -- now construct the path:
                    outputPath.Add(endNode);
                    while (previous.ContainsKey(searchFocus))
                    {
                        searchFocus = previous[searchFocus];
                        outputPath.Add(searchFocus);
                    }
                    outputPath.Reverse();
                    break;
                }
                else
                {
                    // We did not found the target yet -- develop new nodes.
                    foreach (var neighbor in graph.Neighbors(searchFocus))
                    {
                        if (openSet.Contains(neighbor))
                        {
                            continue;
                        }
                        openQueue.Enqueue(neighbor);
                        openSet.Add(neighbor);
                        previous[neighbor] = searchFocus;
                    }
                }
            }
        }
    }

    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations=1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }


    public static bool CheckConnectedToXTiles<NodeType>(IGraph<NodeType> graph, NodeType startNode, int count)
    {        
        HashSet<NodeType> openSet = new HashSet<NodeType>();

        bool CheckConnected(NodeType node)
        {       
            UnityEngine.Debug.Log("Node = " + node);
            if(openSet.Contains(node)) return false;
            
            openSet.Add(node);

            if(openSet.Count >= count)
            {
                return true;
            }
            
            var neigh = new List<NodeType>(graph.Neighbors(node));

            bool result = false;

            foreach (var neighbor in neigh)
            {            
                result = result || CheckConnected(neighbor);
            }

            return result;
        }

        if(!graph.IsAllowed(startNode)) return false;

        bool result = CheckConnected(startNode);;
        UnityEngine.Debug.Log(openSet.Count);
        return result;
    }
}