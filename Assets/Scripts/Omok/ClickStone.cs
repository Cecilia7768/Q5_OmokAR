using UnityEngine;
using UnityEngine.UI;

public class ClickStone : Stone, IStoneSet
{
  //������ �� �� �ִ��� Ȯ���ϴ� ��ũ��Ʈ
  public bool Check()
  {
    if (Data.IsStart == false)
      return false;
    //�ش� �ڸ��� �̹� ���� �ִٸ� false
    if (OmokPan.ball[y, x] != 0)
      return false;
    return true;
  }

  //���� �Ͽ� ���� Ŭ���� ���̴� �� ���� ��ȭ
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

  //�� �ʱ�ȭ
  public void ClearColor()
  {
    var color = transform.GetComponent<Image>().color;
    color.a = 0f;
    GetComponent<Image>().color = color;
  }
}
