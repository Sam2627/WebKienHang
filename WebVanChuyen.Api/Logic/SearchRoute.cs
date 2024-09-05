using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Logic
{
    public class SearchRoute
    {
        private readonly DataContextWeb DbContext;

        public string StartId, EndId, WarehouseIds;

        public Node StartNode { get; set; }

        public Node EndNode { get; set; } = new Node();

        public List<Node> ListNodes = new List<Node>();

        public List<Node> NewListNodes = new List<Node>();

        public SearchRoute(DataContextWeb appDbContext) { this.DbContext = appDbContext; }

        public SearchRoute(DataContextWeb appDbContext, string startId, string endId)
        {
            this.DbContext = appDbContext;
            StartId = startId;
            EndId = endId;
        }

        public async Task CreateListNodes()
        {
            var warehouse = await DbContext.Warehouse.AsQueryable().ToListAsync();
            foreach (var wh in warehouse)
            {
                // Create node with warehouse Id
                Node node = new Node { NodeId = wh.WarehouseId };

                // Add list of path conn to node
                node.ListPaths = await DbContext.WHPath.AsQueryable()
                    .Where(p => p.StartPoint == wh.WarehouseId || p.EndPoint == wh.WarehouseId).ToListAsync();

                // Add new node to List
                ListNodes.Add(node);
            }
        }

        public void SearchRoutePath()
        {
            // Set StartNode and EndNode
            StartNode = ListNodes.FirstOrDefault(n => n.NodeId == StartId);
            EndNode = ListNodes.FirstOrDefault(n => n.NodeId == EndId);

            // Config StartNode and create new list node
            StartNode.MinCost = 0;
            var prioQueue = new List<Node> { StartNode };

            do
            {
                prioQueue = prioQueue.OrderBy(n => n.MinCost).ToList();     // Order Queue Nodes
                var node = prioQueue.First();                               // Choose node have low cost
                prioQueue.Remove(node);                                     // Remove node

                foreach (var path in node.ListPaths)    // loop for each path in node
                {
                    // Get connNodeId from path
                    string connNodeId;
                    if (path.StartPoint == node.NodeId) connNodeId = path.EndPoint;
                    else connNodeId = path.StartPoint;

                    var connNode = ListNodes.First(id => id.NodeId == connNodeId);  // Get connect node in path

                    if (connNode.Visited) continue;    // Skip loop if already visited

                    if (connNode.MinCost == null || node.MinCost + path.Cost < connNode.MinCost)
                    {
                        connNode.MinCost = node.MinCost + path.Cost;
                        connNode.ShipCost = node.ShipCost + path.ShipCost;
                        connNode.NearestNodeStart = node.NodeId;

                        if (!prioQueue.Contains(connNode) && connNode.Visited == false) prioQueue.Add(connNode);
                    }
                }
                node.Visited = true;
            }
            while (prioQueue.Any());
        }

        
        public async void BuildListNode()
        {
            if (EndNode.MinCost > 0)
            {
                NewListNodes.Add(EndNode);
                var nearestId = EndNode.NearestNodeStart;
                Node addNode;

                do
                {
                    addNode = ListNodes.FirstOrDefault(n => n.NodeId == nearestId);
                    NewListNodes.Add(addNode);
                    nearestId = addNode.NearestNodeStart;
                }
                while (addNode.MinCost > 0);

                NewListNodes.Reverse();
            }

            foreach (var node in NewListNodes)   // Create string attribute for WHRoute
            {
                if (node.NodeId != EndId) WarehouseIds += $"{node.NodeId}, ";
                else WarehouseIds += node.NodeId;
            }
        }

        public string BuildListNode(Node endNode)
        {
            if (endNode.MinCost > 0)
            {
                string warehouseIds = string.Empty;
                Node addNode;
                List<Node> newListNodes = new List<Node> { endNode };
                var nearestId = endNode.NearestNodeStart;

                do
                {
                    addNode = ListNodes.FirstOrDefault(n => n.NodeId == nearestId);
                    newListNodes.Add(addNode);
                    nearestId = addNode.NearestNodeStart;
                }
                while (addNode.MinCost > 0);

                newListNodes.Reverse();

                foreach (var node in newListNodes)   // Create string attribute for WHRoute
                {
                    if (node.NodeId != endNode.NodeId) warehouseIds += $"{node.NodeId}, ";
                    else warehouseIds += node.NodeId;
                }

                return warehouseIds;
            }

            return null;
        }
    }
}
