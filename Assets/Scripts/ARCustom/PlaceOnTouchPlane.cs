using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnTouchPlane : MonoBehaviour
{
  [Header("��ġ�� ��ġ �� ������Ʈ")]
  [SerializeField] private GameObject obj; 
  [SerializeField] private ARRaycastManager raycastManager;

  private List<ARRaycastHit> hitList = new List<ARRaycastHit>();

  private void Update()
  {
    if(Input.touchCount > 0)
    {
      var t = Input.GetTouch(0);
      if(t.phase == TouchPhase.Began)
      {
        //�տ� �����ߴ� �� ���ؼ� ���� ������ ��ĵ�� ������ ������ ������ ���� 
        //������ ��鿡�ٰ� ���̸� ���� ����� ����
        //ó�� hit�� ������� �޾ƿ�
        if(raycastManager.Raycast(t.position, hitList, TrackableType.PlaneWithinPolygon))
        {
          var h = hitList[0].pose;
          Instantiate(obj, h.position, h.rotation);          
        }
      }
    }
  }

}
