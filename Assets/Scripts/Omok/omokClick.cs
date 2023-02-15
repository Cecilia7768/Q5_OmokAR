using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class omokClick : MonoBehaviour
{
  [Header("세로")] public int column; //세로 ↕

  [Header("가로")] public int row; //가로 ↔

  [Space(20)][SerializeField] private Image icon; //본인 Image
  [SerializeField] private GameObject stoneHelopOb; //네모 흰색
  [SerializeField] private GameObject noOb; // x 표시





  public void ballClick(int num)
  //num이 1이라면 흑돌이 채워지고 2면 백돌이 3이라면 X표시가 표시됩니다.
  {
    var color = transform.GetComponent<Image>().color;

    if (num == 1)
    {
       color.a = 1f;
      color = omokManager.inst.dollcolor[0];
      GetComponent<Image>().color = color;
    }
    else if (num == 2)
    {
      //var color = transform.GetComponent<Image>().color;
      color.a = 1f;     
      color = omokManager.inst.dollcolor[1];
      GetComponent<Image>().color = color;
    }
    else if (num == 3)
    {
      noOb.SetActive(true);
      //omokManager.inst.noObs.Add(gameObject);
    }
  }


  public void NoOff()
  {
    omokManager.inst.ball[column, row] = 0;
    noOb.SetActive(false);
  }




  [ContextMenu("Do Something")]
  void DoSomething()
  //디버그용
  {
    Transform a = GameObject.Find("Grid").transform;
    for (int i = 0; i <= 14; i++)
    {
      for (int j = 0; j <= 14; j++)
      {
        GameObject ob = Instantiate(gameObject, a);
        ob.name = $"{i},{j}";
      }
    }
  }

  [ContextMenu("name")]
  void nameset()
  //디버그용
  {
    string[] s = gameObject.name.Split(',');
    row = int.Parse(s[1]);
    column = int.Parse(s[0]);
  }
}