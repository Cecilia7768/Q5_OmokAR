using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameDoneUI : MonoBehaviour
{
  [Header("���� ���� �˸� UI")]
  [SerializeField] private GameObject resultObj;
  [SerializeField] private TMP_Text resultText;


  //���ӿ����� �Ǿ����� �¸� UI ǥ��
  private void Start()
  {
    Data.Winner = Winner.None;

    this.UpdateAsObservable()
        .Where(_ => Data.Winner != Winner.None)
        .Subscribe(_ =>
        {
          string name = Enum.GetName(typeof(Winner), Data.Winner);
          IsGameOverUISet(name);
        });
  }

  void IsGameOverUISet(string winner)
  {
    resultObj.SetActive(true);
    resultText.text = winner + " Win !";
    Data.Winner = Winner.None;
  }
}
