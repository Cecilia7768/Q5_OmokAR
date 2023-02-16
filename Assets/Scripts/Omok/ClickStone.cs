using UnityEngine;
using UnityEngine.UI;

public class ClickStone : Stone, IStoneSet
{
  //선택을 할 수 있는지 확인하는 스크립트
  public bool Check()
  {
    if (Data.IsStart == false)
      return false;
    //해당 자리에 이미 돌이 있다면 false
    if (OmokPan.ball[y, x] != 0)
      return false;
    return true;
  }

  //현재 턴에 따라 클릭시 놓이는 돌 색깔 변화
  public void OnClickStone()
  {
    if (Check() == false)
      return;

    var color = transform.GetComponent<Image>().color;
    if (Data.CurrTurn.Equals(CurrTurn.BLACK))
      color = Data.dollcolor[0];
    else
      color = Data.dollcolor[1];
    color.a = 1f;
    GetComponent<Image>().color = color;
    OmokLogic.Instance.BallClick(x, y);
  }

  //색 초기화
  public void ClearColor()
  {
    var color = transform.GetComponent<Image>().color;
    color.a = 0f;
    GetComponent<Image>().color = color;
  }
}
