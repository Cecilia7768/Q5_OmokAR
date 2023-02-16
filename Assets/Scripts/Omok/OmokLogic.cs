using UnityEngine;

public class OmokLogic : MonoBehaviour 
{  
  public static OmokLogic Instance { get; private set; }

  public GameObject[] ballOb; //돌을 미리 담자

  private void Awake()
  {
    Instance = this;
    Data.dollcolor.Add(new Color(0, 0, 0, 0));
    Data.dollcolor.Add( new Color(1, 1, 1, 0));
  }

  //게임 시작시 바둑판 초기화
  public void GameStart()
  {
    for (int i = 0; i < OmokPan.size; i++)
    {
      for (int j = 0; j < OmokPan.size; j++)
      {
        ballScan(j, i).GetComponent<ClickStone>().y = i;
        ballScan(j, i).GetComponent<ClickStone>().x = j;
        if (OmokPan.ball[i, j] == 0)
          continue;
        ballScan(j, i).GetComponent<ClickStone>().ClearColor();
        Clear(j, i);
      }
    }
    Data.IsStart = true;
    Data.CurrTurn = CurrTurn.BLACK;
  }

  //좌표 정보 초기화
  public void Clear(int x, int y)
  {
    OmokPan.ball[y, x] = 0;
  }

  //대상 돌이 놓여진 위치를 추적
  public GameObject ballScan(int x, int y)
  {
    return ballOb[x + (y * OmokPan.size)];
  }

  //오목판 클릭시 클릭 이벤트
   public void BallClick(int x, int y)
  {
    if (Data.CurrTurn.Equals(CurrTurn.BLACK))
      OmokPan.ball[y, x] = 1;
    else
      OmokPan.ball[y, x] = 2;

    //마지막 돌 놓은 뒤 게임오버가 된건지 체크 
    //안끝났다면 턴 계속 진행
    if (VictoryCheck(x, y))
      Data.IsGameOver(Data.CurrTurn);
    else
    {
      if (Data.CurrTurn.Equals(CurrTurn.WHITE))
      {
        Data.CurrTurn = CurrTurn.BLACK;
        Debug.Log("검정 차례");
      }
      else
      {
        Data.CurrTurn = CurrTurn.WHITE;
        Debug.Log("흰색 차례");
      }
    }
  }

  bool VictoryCheck(int x, int y)
  {
    //검증할 마지막 턴이 누구였는지 확인 후 놓여진 돌 전체 계산
    //흑1 / 백2
    int ballNum = 1;
    if (!Data.CurrTurn.Equals(CurrTurn.BLACK))
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
      if (!(v[i] >= 0 && v[i] < OmokPan.size))
        return false;

    return true;
  }

  //git open source
  //완전탐색 시작
  bool FiveCheck(int ballNum)
  {
    for (int i = 0; i < OmokPan.size; i++)
    {
      for (int j = 0; j < OmokPan.size; j++)
      {
        if (OmokPan.ball[i, j] != ballNum)
          continue;
        //→
        //왼쪽으로 돌을 확인합니다.
        if (InRange(j + 4) && OmokPan.ball[i, j + 1] == ballNum && OmokPan.ball[i, j + 2] == ballNum &&
            OmokPan.ball[i, j + 3] == ballNum && OmokPan.ball[i, j + 4] == ballNum)
        {
          return true;
        }
        //↓
        //아래칸으로 돌을 확인합니다.
        else if (InRange(i + 4) && OmokPan.ball[i + 1, j] == ballNum && OmokPan.ball[i + 2, j] == ballNum &&
                 OmokPan.ball[i + 3, j] == ballNum && OmokPan.ball[i + 4, j] == ballNum)
        {
          return true;
        }
        //↘
        //대각선아래로 돌을 확인합니다.
        else if (InRange(i + 4, j + 4) && OmokPan.ball[i + 1, j + 1] == ballNum && OmokPan.ball[i + 2, j + 2] == ballNum &&
                 OmokPan.ball[i + 3, j + 3] == ballNum && OmokPan.ball[i + 4, j + 4] == ballNum)
        {
          return true;
        }
        //↙
        //왼쪽대각선아래로 돌을 확인합니다.
        else if (InRange(i + 4, j - 4) && OmokPan.ball[i + 1, j - 1] == ballNum && OmokPan.ball[i + 2, j - 2] == ballNum &&
                 OmokPan.ball[i + 3, j - 3] == ballNum && OmokPan.ball[i + 4, j - 4] == ballNum)
        {
          return true;
        }
      }
    }

    return false;
  }
}