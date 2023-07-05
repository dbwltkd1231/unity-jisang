using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer
{
    
   


}
public interface ISubject
{
    void ResisterMonster(IObserver observer);
    void RemoveMonster(IObserver observer);
    void ResisterSkillSlot(IObserver observer);
    void RemoveSkillSlot(IObserver observer);
    void NotifyMonster();
    void NotifySkillSlot();
}
public interface IObserver
{
    void UpdateData(bool hide);
    void UpdateData(int lv);
}