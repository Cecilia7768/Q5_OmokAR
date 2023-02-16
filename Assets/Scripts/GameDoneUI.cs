using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameDoneUI : MonoBehaviour
{
  [Header("게임 승패 알림 UI")]
  [SerializeField] private GameObject resultObj;
  [SerializeField] private TMP_Text resultText;


  //게임오버가 되었을때 승리 UI 표시
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
