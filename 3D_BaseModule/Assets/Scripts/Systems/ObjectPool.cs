using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 제네릭 기반 오브젝트 풀링 시스템입니다.
/// MonoBehaviour와 IPoolable을 구현한 객체에 사용됩니다.
/// 미리 생성된 객체를 재활용하여 성능을 최적화합니다.
/// </summary>
/// <typeparam name="T">MonoBehaviour 및 IPoolable을 구현한 컴포넌트 타입</typeparam>
public class ObjectPool<T> where T : MonoBehaviour, IPoolable
{
    private readonly Queue<T> pool = new Queue<T>();  // 비활성화된 오브젝트를 저장하는 큐입니다.
    private readonly T prefab;                        // 풀링될 프리팹입니다.  
    private readonly Transform parent;                // 생성된 오브젝트들을 정리할 부모 트랜스폼입니다.

    /// <summary>
    /// 지정된 수만큼 오브젝트를 미리 생성하여 풀에 저장합니다.
    /// </summary>
    /// <param name="prefab">풀링할 프리팹</param>
    /// <param name="initialSize">초기 생성할 오브젝트 수</param>
    /// <param name="parent">생성된 오브젝트의 부모 오브젝트 (null)</param>
    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// 풀에서 오브젝트를 하나 꺼내어 활성화하고, 지정된 위치와 회전으로 배치합니다.
    /// 풀에 여유가 없으면 새로 생성합니다.
    /// </summary>
    /// <param name="position">오브젝트 월드 위치</param>
    /// <param name="rotation">오브젝트의 회전값</param>
    /// <returns>활성화된 오브젝트</returns>
    public T Spawn(Vector3 position, Quaternion rotation)
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab, parent);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        obj.OnSpawn();  // 사용자 정의 초기화 처리
        return obj;
    }

    /// <summary>
    /// 오브젝트를 풀에 반환하여 비활성화하고 대기 상태로 돌려놓습니다.
    /// </summary>
    /// <param name="obj">풀에 반환할 오브젝트</param>
    public void Despawn(T obj)
    {
        obj.OnDespawn();  // 사용자 정의 정리 처리
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
