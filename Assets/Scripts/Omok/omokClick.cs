using UnityEngine;
using UnityEngine.UI;

public class OmokClick : MonoBehaviour
{
    private GameManager gameManager;
    [Header("세로")] public int y; 
    [Header("가로")] public int x;


    private void Start() => gameManager = GameManager.Instance;
    public bool Check()
    //선택을 할 수 있는지 확인하는 스크립트
    {
        if (GameManager.Instance.IsPlay == false)
            return false;
        //해당 자리에 이미 돌이 있다면 false
        if (OmokManager.Instance.ball[y, x] != 0)
            return false;
        return true;
    }

    //현재 턴에 따라 클릭시 놓이는 돌 색깔 변화
    public void OnClickStone()
    {
        if (Check() == false)
            return;
      
        var color = transform.GetComponent<Image>().color;
        color.a = 1f;

        if (GameManager.Instance.currTurn.Equals(CurrTurn.BLACK))        
            color = OmokManager.Instance.dollcolor[0];    
        else    
            color = OmokManager.Instance.dollcolor[1];
        GetComponent<Image>().color = color;
        OmokManager.Instance.BallClick(x, y);
    }

    //오목판 초기화
    public void Clear()
    {
        var color = transform.GetComponent<Image>().color;
        color.a = 0f;
        GetComponent<Image>().color = color;
        OmokManager.Instance.ball[y, x] = 0;
    }
}
