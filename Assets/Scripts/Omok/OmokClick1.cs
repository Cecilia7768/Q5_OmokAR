using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class OmokClick1 : MonoBehaviour
{
  [Header("세로")] public int column; //세로 ↕

  [Header("가로")] public int row; //가로 ↔

  public bool Check()
  //선택을 할 수 있는지 확인하는 스크립트
  {
    if (omokManager.inst.IsPlay == false)
    //플레이중이 아님
    {
      return false;
    }

    //if (omokManager.inst.IsBlackTurn)
    ////블랙턴
    //{
    //  if (omokManager.inst.myPlayType != ePlayType.BLACK)
    //  //본인의 돌 타입이 블랙이 아니라면 false
    //  {
    //    return false;
    //  }
    //}
    //else
    //{
    //  if (omokManager.inst.myPlayType != ePlayType.WHITE)
    //  //블랙턴이 아니고 본인의 돌 타입이 흰돌이 아니면 false
    //  {
    //    return false;
    //  }
    //}

    if (omokManager.inst.ball[column, row] != 0)
    //해당 돌에 돌이 있다면 false
    {
      return false;
    }

    return true;
  }

  public void OnClickStone()
  {
    if (Check() == false)
    {
      return;
    }
    var color = transform.GetComponent<Image>().color;
    color.a = 1f;

    if (omokManager.inst.IsBlackTurn)
    {
      color = omokManager.inst.dollcolor[0];
      //블랙턴이라면 이미지를 블랙으로
    }
    else
    {
      color = omokManager.inst.dollcolor[0];

      //흰턴이라면 이미지를 흰색돌로
    }
    GetComponent<Image>().color = color;
    omokManager.inst.BallClick(row, column);
  }

  public void Clear()
  //초기화 시킵니다.
  {
    var color = transform.GetComponent<Image>().color;
    color.a = 0f;
    GetComponent<Image>().color = color;
    omokManager.inst.ball[column, row] = 0;   
  }
}
