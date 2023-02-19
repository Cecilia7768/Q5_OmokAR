using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnTouchPlane : MonoBehaviour
{
  [Header("터치시 설치 할 오브젝트")]
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
        //앞에 진행했던 걸 통해서 싷제 공간을 스캔한 다음에 가상의 폴리곤 생성 
        //가상의 평면에다가 레이를 쏴서 결과를 받음
        //처음 hit된 결과물을 받아옴
        if(raycastManager.Raycast(t.position, hitList, TrackableType.PlaneWithinPolygon))
        {
          var h = hitList[0].pose;
          Instantiate(obj, h.position, h.rotation);          
        }
      }
    }
  }

}
