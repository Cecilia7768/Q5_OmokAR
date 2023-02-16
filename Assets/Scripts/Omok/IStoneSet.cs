using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoneSet 
{
  public abstract bool Check();
  public abstract void OnClickStone();
  public abstract void ClearColor();
}
