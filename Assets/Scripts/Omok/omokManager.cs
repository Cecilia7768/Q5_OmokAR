using UnityEngine;

public class OmokManager : MonoSingleton<OmokManager>
{
    private GameManager gameManager;// = GameManager.Instance;

    [Header("0:흑돌 / 1:백돌")]
    public Color[] dollcolor;

    private const int SIZE = 19; // 바둑판 사이즈
    public int[,] ball = new int[,]
        //좌표 1 흑돌 ,2 흰돌
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
        };

    public GameObject[] ballOb; //돌을 미리 담자

    private void Start() => gameManager = GameManager.Instance;
    //게임 시작시 바둑판 초기화
    public void GameStart()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                ballScan(j, i).GetComponent<OmokClick>().y = i;
                ballScan(j, i).GetComponent<OmokClick>().x = j;
                if (ball[i, j] == 0)
                    continue;
                ballScan(j, i).GetComponent<OmokClick>().Clear();
            }
        }
        GameManager.Instance.IsPlay = true;
        gameManager.currTurn = CurrTurn.BLACK;
    }

    //대상 돌이 놓여진 위치를 추적
    public GameObject ballScan(int x, int y)
    {
        return ballOb[x + (y * SIZE)];
    }

    //오목판 클릭시 클릭 이벤트
    public void BallClick(int x, int y)
    {
        if (gameManager.currTurn.Equals(CurrTurn.BLACK))
            ball[y, x] = 1;
        else
            ball[y, x] = 2;

        //마지막 돌 놓은 뒤 게임오버가 된건지 체크 
        //안끝났다면 턴 계속 진행
        if (VictoryCheck(x, y))
            GameManager.Instance.IsGameOver(gameManager.currTurn);
        else
        {
            if (gameManager.currTurn.Equals(CurrTurn.WHITE))
            {
                gameManager.currTurn = CurrTurn.BLACK;
                Debug.Log("검정 차례");
            }
            else
            {
                gameManager.currTurn = CurrTurn.WHITE;
                Debug.Log("흰색 차례");
            }
        }
    }

    bool VictoryCheck(int x, int y)
    {
        //검증할 마지막 턴이 누구였는지 확인 후 놓여진 돌 전체 계산
        //흑1 / 백2
        int ballNum = 1;
        if (!gameManager.currTurn.Equals(CurrTurn.BLACK))
            ballNum = 2;

        //검증할 현재 턴의 돌이 다섯개가 이어져 있는가?
        if (FiveCheck(ballNum))
            return true;
        return false;
    }

    //git open source
    //이어진 돌이 5개만 있어도 승패확인이 가능
    //검증 포인트부터 +@ 확인 범위 설정
    bool InRange(params int[] v)
    {
        for (int i = 0; i < v.Length; i++)
            if (!(v[i] >= 0 && v[i] < SIZE))
                return false;

        return true;
    }

    //git open source
    //완전탐색 시작
    bool FiveCheck(int ballNum)
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (ball[i, j] != ballNum)
                    continue;
                //→
                //왼쪽으로 돌을 확인합니다.
                if (InRange(j + 4) && ball[i, j + 1] == ballNum && ball[i, j + 2] == ballNum &&
                    ball[i, j + 3] == ballNum && ball[i, j + 4] == ballNum)
                {
                    return true;
                }
                //↓
                //아래칸으로 돌을 확인합니다.
                else if (InRange(i + 4) && ball[i + 1, j] == ballNum && ball[i + 2, j] == ballNum &&
                         ball[i + 3, j] == ballNum && ball[i + 4, j] == ballNum)
                {
                    return true;
                }
                //↘
                //대각선아래로 돌을 확인합니다.
                else if (InRange(i + 4, j + 4) && ball[i + 1, j + 1] == ballNum && ball[i + 2, j + 2] == ballNum &&
                         ball[i + 3, j + 3] == ballNum && ball[i + 4, j + 4] == ballNum)
                {
                    return true;
                }
                //↙
                //왼쪽대각선아래로 돌을 확인합니다.
                else if (InRange(i + 4, j - 4) && ball[i + 1, j - 1] == ballNum && ball[i + 2, j - 2] == ballNum &&
                         ball[i + 3, j - 3] == ballNum && ball[i + 4, j - 4] == ballNum)
                {
                    return true;
                }
            }
        }

        return false;
    }
}