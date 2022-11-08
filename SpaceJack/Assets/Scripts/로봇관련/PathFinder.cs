using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
static class Constants
{
    public const int NODE_RATIO = 10;
}

public class Globals
{
    public enum Status { Standby, Alert, Combat }
    public static bool alert_t;
    public static Status status = Status.Combat;
    public static GameObject player;
    public static Vector3 player_pos;
}


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; position = new Vector2(x, y); }

    public bool isWall;
    public Node ParentNode;
    public Vector2 position;
    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}
*/

public static class PathFinder
{
    public static Vector2Int bottomLeft = new Vector2Int(-100, -100);
    public static Vector2Int topRight = new Vector2Int(300, 300);

    public static bool allowDiagonal, dontCrossCorner;
    

    public static Node[,] BakeMap(float radius)
    {
        // =============================================
        // 화면 범위 지정하는 함수 필요!!
        // =============================================

        int sizeX = (topRight.x - bottomLeft.x) / Constants.NODE_RATIO;
        int sizeY = (topRight.y - bottomLeft.y) / Constants.NODE_RATIO;
        Node[,] _NodeMap = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                Vector2 currentPos = new Vector2(Constants.NODE_RATIO* i + bottomLeft.x, Constants.NODE_RATIO* j +bottomLeft.y);
                foreach (Collider2D col in Physics2D.OverlapCircleAll(currentPos, radius))
                    if (col.gameObject.layer == LayerMask.NameToLayer("wall")) isWall = true;

                _NodeMap[i, j] = new Node(isWall, Constants.NODE_RATIO * i + bottomLeft.x, Constants.NODE_RATIO * j + bottomLeft.y);
            }
        }

        return _NodeMap;
    }

    public static List<Node> Pathfinder(Vector2Int startPos, Vector2Int targetPos, float radius)
    {
        Node[,] NodeArray = BakeMap(radius);

        Node StartNode = NodeArray[(startPos.x - bottomLeft.x) / Constants.NODE_RATIO, (startPos.y - bottomLeft.y) / Constants.NODE_RATIO];
        Node TargetNode = NodeArray[(targetPos.x - bottomLeft.x) / Constants.NODE_RATIO, (targetPos.y - bottomLeft.y) / Constants.NODE_RATIO];

        List<Node> OpenList = new List<Node>() { StartNode };
        List<Node> ClosedList = new List<Node>();
        List<Node>  FinalNodeList = new List<Node>();
        Node CurNode;


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return FinalNodeList;
            }

            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + Constants.NODE_RATIO, CurNode.y + Constants.NODE_RATIO);
                OpenListAdd(CurNode.x - Constants.NODE_RATIO, CurNode.y + Constants.NODE_RATIO);
                OpenListAdd(CurNode.x - Constants.NODE_RATIO, CurNode.y - Constants.NODE_RATIO);
                OpenListAdd(CurNode.x + Constants.NODE_RATIO, CurNode.y - Constants.NODE_RATIO);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + Constants.NODE_RATIO);
            OpenListAdd(CurNode.x + Constants.NODE_RATIO, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - Constants.NODE_RATIO);
            OpenListAdd(CurNode.x - Constants.NODE_RATIO, CurNode.y);
        }
        return FinalNodeList;

        void OpenListAdd(int checkX, int checkY)
        {
            // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
            if ((checkX >= bottomLeft.x && checkX < topRight.x) && (checkY >= bottomLeft.y && checkY < topRight.y) && !NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall && !ClosedList.Contains(NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO]))
            {
                // 대각선 허용시, 벽 사이로 통과 안됨
                if (allowDiagonal) if (NodeArray[(CurNode.x - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall && NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (CurNode.y - bottomLeft.y) / Constants.NODE_RATIO].isWall) return;

                // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                if (dontCrossCorner) if (NodeArray[(CurNode.x - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall || NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (CurNode.y - bottomLeft.y) / Constants.NODE_RATIO].isWall) return;


                // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                Node NeighborNode = NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO];
                int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 * Constants.NODE_RATIO*Constants.NODE_RATIO : 14 * Constants.NODE_RATIO*Constants.NODE_RATIO);


                // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
                if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.G = MoveCost;
                    NeighborNode.H = ((NeighborNode.x - TargetNode.x) + (NeighborNode.y - TargetNode.y)) * Constants.NODE_RATIO;
                    NeighborNode.ParentNode = CurNode;

                    OpenList.Add(NeighborNode);
                }
            }
        }

    }

}
