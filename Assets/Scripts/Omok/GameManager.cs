using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager inst;


  public int victory;
  public int defeat;
  [SerializeField] GameObject warningob;
  [SerializeField] TextMeshProUGUI warningText;


  public String room; // 현재 접속한 방이름
  [Range(2, 8)] public int maxRoom = 2; // 방옵션
  public bool IsChat = false; //현재 채팅중인지 아닌지

  public GameObject loginOb;
  public GameObject IdCreateOb;

  public GameObject nickNameSetob;
  public GameObject joinOb;
  public GameObject chatOb;
  public GameObject loadingOb;


  //public GameObject loginWarningOb;
  //public GameObject roomWarningOb;
  //public ChatManager chatManager;
  //public LobyManager lobyManager;

  //UI들

  private void Awake()
  {
    inst = this;
  }

  public void Warning(string s)
  {
    warningob.SetActive(true);
    warningText.text = s;
  }

}