using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("���� ���� �˸� UI")]
    [SerializeField] private GameObject resultObj;
    [SerializeField] private TMP_Text resultText;


    //���ӿ����� �Ǿ����� �¸� UI ǥ��
    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => GameManager.Instance.winner != Winner.None)
            .Subscribe(_ =>
            {
                string name = Enum.GetName(typeof(Winner), GameManager.Instance.winner);
                IsGameOverUISet(name);
            });
    }

    void IsGameOverUISet(string winner)
    {
        resultObj.SetActive(true);
        resultText.text = winner + " Win !";
        GameManager.Instance.winner = Winner.None;
    }
}
