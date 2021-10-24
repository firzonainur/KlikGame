using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AStar
{
    public class AStarNode
    {
        public float g = 0;
        public float h = 0;
        public float f = 0;
        public AStarNode parent;
        public Vector3Int position;

        public AStarNode(AStarNode parent, Vector3Int position)
        {
            this.parent = parent;
            this.position = position;
        }
    }

    public class AStarSearch
    {
        private int maxIteration = 100;
        private int iteration = 0;

        public AStarSearch() {

        }

        List<Vector3Int> RetracePath(AStarNode startNode)
        {
            List<Vector3Int> path = new List<Vector3Int>();
            AStarNode current_node = startNode;
            while (current_node != null)
            {
                path.Add(current_node.position);
                current_node = current_node.parent;
            }

            path.Reverse();
            return path;
        }

        private bool same(AStarNode a, AStarNode b)
        {
            return (a.position == b.position);
        }

        private float getDistance(AStarNode a, AStarNode b)
        {
            float dx = Mathf.Abs(a.position.x - b.position.x);
            float dy = Mathf.Abs(a.position.y - b.position.y);

            return dx + dy;
        }

        public List<Vector3Int> Search(Tilemap map, Vector3 startPos, Vector3 endPos)
        {
            AStarNode start_node = new AStarNode(null, map.WorldToCell(startPos));
            AStarNode end_node = new AStarNode(null, map.WorldToCell(endPos));

            start_node.f = start_node.g + getDistance(start_node, end_node);

            List<AStarNode> open_list = new List<AStarNode>();
            HashSet<AStarNode> closed_list = new HashSet<AStarNode>();

            List<Vector3Int> path = null;

            if (!map.HasTile(end_node.position)) return path;

            open_list.Add(start_node);

            iteration = 0;

            while (open_list.Count > 0)
            {
                AStarNode current_node = open_list[0];

                int index = 0;
                for (int i = 0; i < open_list.Count; i++)
                {
                    if (open_list[i].f < current_node.f || open_list[i].f == current_node.f && open_list[i].h < current_node.h)
                    {
                        current_node = open_list[i];
                        index = i;
                    }
                }

                open_list.RemoveAt(index);
                closed_list.Add(current_node);

                if (iteration == maxIteration)
                {
                    path = RetracePath(current_node);

                    return path;
                }

                if (same(current_node, end_node))
                {
                    path = RetracePath(current_node);
                    
                    return path;
                }

                List<Vector2Int> neighbors = new List<Vector2Int>();
                neighbors.Add(new Vector2Int(1, 0));
                neighbors.Add(new Vector2Int(-1, 0));
                neighbors.Add(new Vector2Int(0, 1));
                neighbors.Add(new Vector2Int(0, -1));

                List<AStarNode> children = new List<AStarNode>();

                foreach(var neighbor in neighbors)
                {
                    Vector3Int node_position = new Vector3Int(current_node.position.x + neighbor.x, current_node.position.y + neighbor.y, 0);
                    if (map.HasTile(node_position))
                    {
                        AStarNode new_node = new AStarNode(current_node, node_position);

                        children.Add(new_node);
                    }
                }

                foreach(var child in children)
                {
                    foreach(var closed_child in closed_list)
                    {
                        if (same(child, closed_child)) continue;
                    }

                    child.g = current_node.g + 1;
                    child.h = getDistance(child, end_node);
                    child.f = child.g + child.h;

                    foreach (var open_node in open_list)
                    {
                        if (same(child, open_node) && child.g > open_node.g) continue;
                    }

                    open_list.Add(child);
                }

                iteration += 1;
            }
            
            return path;
        }
    }
}