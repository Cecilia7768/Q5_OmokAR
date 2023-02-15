using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class OmokClick1 : MonoBehaviour
{
  [Header("����")] public int column; //���� ��

  [Header("����")] public int row; //���� ��

  public bool Check()
  //������ �� �� �ִ��� Ȯ���ϴ� ��ũ��Ʈ
  {
    if (omokManager.inst.IsPlay == false)
    //�÷������� �ƴ�
    {
      return false;
    }

    //if (omokManager.inst.IsBlackTurn)
    ////����
    //{
    //  if (omokManager.inst.myPlayType != ePlayType.BLACK)
    //  //������ �� Ÿ���� ���� �ƴ϶�� false
    //  {
    //    return false;
    //  }
    //}
    //else
    //{
    //  if (omokManager.inst.myPlayType != ePlayType.WHITE)
    //  //������ �ƴϰ� ������ �� Ÿ���� ���� �ƴϸ� false
    //  {
    //    return false;
    //  }
    //}

    if (omokManager.inst.ball[column, row] != 0)
    //�ش� ���� ���� �ִٸ� false
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
      //�����̶�� �̹����� ������
    }
    else
    {
      color = omokManager.inst.dollcolor[0];

      //�����̶�� �̹����� �������
    }
    GetComponent<Image>().color = color;
    omokManager.inst.BallClick(row, column);
  }

  public void Clear()
  //�ʱ�ȭ ��ŵ�ϴ�.
  {
    var color = transform.GetComponent<Image>().color;
    color.a = 0f;
    GetComponent<Image>().color = color;
    omokManager.inst.ball[column, row] = 0;   
  }
}
