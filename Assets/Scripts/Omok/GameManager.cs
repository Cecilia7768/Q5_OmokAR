using TMPro;
using UnityEngine;

#region *모노싱글톤
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).AddComponent<T>();
                }
            }

            return instance;
        }
    }
}

#endregion
public class GameManager : MonoSingleton<GameManager>
{
    
    // 현재 누구의 턴인지
    public CurrTurn currTurn;
    //게임 종료 여부
    public Winner winner;
    //게임 시작 여부
    public bool IsPlay = false;


    public void IsGameOver(CurrTurn _winner)
    {
        switch (_winner)
        {
            case CurrTurn.BLACK:
                winner = Winner.BLACK;
                break;
            case CurrTurn.WHITE:
                winner = Winner.WHITE;
                break;
        }
    }

  
}