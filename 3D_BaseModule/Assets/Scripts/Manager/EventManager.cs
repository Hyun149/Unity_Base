using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

/// <summary>
/// <b>이벤트 키 문자열을 기반으로 이벤트를 등록/호출하는 범용 이벤트 매니저입니다.</b><br/>
/// - StartListening: 이벤트에 리스너(메서드) 등록<br/>
/// - StopListening: 이벤트 리스너 제거<br/>
/// - TriggerEvent: 해당 이벤트 호출
/// </summary>
public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Action> eventDicionary = new Dictionary<string, Action>();  // 이벤트 이름과 연결된 델리게이트 저장소

    public static void StartListening(string eventName, Action listener)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent += listener;
            Instance.eventDicionary[eventName] = thisEvent;
        }
        else
        {
            Instance.eventDicionary[eventName] = listener;
        }
    }

    /// <summary>
    /// 해당 이벤트에서 메서드를 제거합니다.
    /// </summary>
    /// <param name="eventName">이벤트 키 문자열</param>
    /// <param name="listener">등록 해제할 메서드</param>
    public static void StopListening(string eventName, Action lisener)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent -= lisener;
            if (thisEvent == null)
            {
                Instance.eventDicionary.Remove(eventName);
            }
            else
            {
                Instance.eventDicionary[eventName] = thisEvent;
            }
        }
    }

    /// <summary>
    /// 해당 이벤트를 트리거하여 모든 리스너를 실행합니다.
    /// </summary>
    /// <param name="eventName">트리거할 이벤트 키</param>
    public static void TriggerEvent(string eventName)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    /// <summary>
    /// 모든 이벤트 리스너를 초기화합니다. (보통 씬 전환 시)
    /// </summary>
    public static void ClearAllEvents()
    {
        if (Instance == null) return;

        Instance.eventDicionary.Clear();
    }
}
