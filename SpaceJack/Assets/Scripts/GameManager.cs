using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int NODE_RATIO = 10;
}

public static class Globals
{
    public enum Status { Standby, Alert, Combat }
    public static bool alert_t;
    public static Status status = Status.Combat;
    public static GameObject player;
    public static Vector3 player_pos;
    public static float[,] weapon_Info = new float[8, 7] { { 4f, 3.8f, 21.8f,100f, 0.15f, 40f, 0.4f },//비살상 미니건(ID-1)
        {30f, 6f, 18.43f, 72.73f, 0.2f, 2f, 0.66f },//미니로켓(ID-2)
        {11f,1.8f,11.31f,114.29f,0.33f,6f,0.42f },//자동소총(ID-3)
        {24f,5f,17.35f,57.14f,1.2f,5f,1.12f },//자동유탄발사기(ID-4)
        {7f,2.2f,19.98f,114.29f,0.33f,10f,0.385f },//대구경 기관단총(ID-5)
        {15f,2f,33.69f,133.33f,0f,10f,0.18f },//샷건(ID-6)
        {20f,2f,3.58f,80f,0.8f,3f,0.8f },//딱총(ID-7)
        {7f,2.2f,2.6f,114.29f,0.33f,10f,0.385f } };//방탄방패+기관단총(ID-8)
    //데미지,장전시간,발사각,탄속,발사간격,탄창,탄수명
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


public class GameManager : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    ///public void PathFinding()//
    public List<Node> PathFinding(Vector2Int startPos, Vector2Int targetPos, float radius = 3.0f) // transform.position
    {
        /*
        // 입력받은 변수를 Vector2Int형식으로 변환
        Vector2Int startPos = new Vector2Int(int(_startPos.x), int(_startPos.y));
        Vector2Int dstPos = new Vector2Int(int(_dstPos.x), int(_dstPos.y));
        */

        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = (topRight.x - bottomLeft.x) / Constants.NODE_RATIO + 1;
        sizeY = (topRight.y - bottomLeft.y) / Constants.NODE_RATIO + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(Constants.NODE_RATIO * i + bottomLeft.x, Constants.NODE_RATIO * j + bottomLeft.y), radius))
                    if (col.gameObject.layer == LayerMask.NameToLayer("wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, Constants.NODE_RATIO * i + bottomLeft.x, Constants.NODE_RATIO * j + bottomLeft.y);
            }
        }

        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[(startPos.x - bottomLeft.x) / Constants.NODE_RATIO, (startPos.y - bottomLeft.y) / Constants.NODE_RATIO];
        TargetNode = NodeArray[(targetPos.x - bottomLeft.x) / Constants.NODE_RATIO, (targetPos.y - bottomLeft.y) / Constants.NODE_RATIO];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


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

                for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
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
        /*
        if(FinalNodeList.Count != 0){
            return FinalNodeList
        }

        */
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall && !ClosedList.Contains(NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[(CurNode.x - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall && NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (CurNode.y - bottomLeft.y) / Constants.NODE_RATIO].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[(CurNode.x - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO].isWall || NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (CurNode.y - bottomLeft.y) / Constants.NODE_RATIO].isWall) return;


            // 이웃노드에 넣고, 직선은 100, 대각선은 141비용
            Node NeighborNode = NodeArray[(checkX - bottomLeft.x) / Constants.NODE_RATIO, (checkY - bottomLeft.y) / Constants.NODE_RATIO];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 * Constants.NODE_RATIO : 14 * Constants.NODE_RATIO);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10 * Constants.NODE_RATIO;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}