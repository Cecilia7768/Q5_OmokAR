using UnityEngine;
using UnityEngine.UI;

public class OmokClick : MonoBehaviour
{
    private GameManager gameManager;
    [Header("����")] public int y; 
    [Header("����")] public int x;


    private void Start() => gameManager = GameManager.Instance;
    public bool Check()
    //������ �� �� �ִ��� Ȯ���ϴ� ��ũ��Ʈ
    {
        if (GameManager.Instance.IsPlay == false)
            return false;
        //�ش� �ڸ��� �̹� ���� �ִٸ� false
        if (OmokManager.Instance.ball[y, x] != 0)
            return false;
        return true;
    }

    //���� �Ͽ� ���� Ŭ���� ���̴� �� ���� ��ȭ
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

    //������ �ʱ�ȭ
    public void Clear()
    {
        var color = transform.GetComponent<Image>().color;
        color.a = 0f;
        GetComponent<Image>().color = color;
        OmokManager.Instance.ball[y, x] = 0;
    }
}
