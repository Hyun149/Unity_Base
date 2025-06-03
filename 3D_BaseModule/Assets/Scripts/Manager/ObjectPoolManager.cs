using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <b>ObjectPoolManager</b><br/>
/// 오브젝트 풀을 <b>키(key)</b>를 기준으로 중앙에서 생성 및 관리하는 <b>싱글톤 매니저</b>입니다.
/// - 각 풀은 타입에 맞게 제네릭으로 생성되며, string 키로 식별됩니다.
/// - ObjectPoolManager는 Singleton<ObjectPoolManager>를 상속하여 자동 싱글톤으로 구성됩니다.
/// </summary>
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    /// <summary>
    /// 모든 풀을 저장하는 딕셔너리입니다.
    /// - string 키를 기준으로 관리됩니다.
    /// - value는 다양한 ObjectPool<T>를 보관하기 위해 object로 저장됩니다.
    /// - 나중에 Spawn/Despawn 시 T 타입으로 캐스팅되어 사용됩니다.
    /// </summary>
    private readonly Dictionary<string, object> pools = new();

    /// <summary>
    /// 새로운 오브젝트 풀을 생성하고 등록합니다.
    /// </summary>
    /// <typeparam name="T">풀링할 오브젝트 타입 (MonoBehaviour + IPoolable)</typeparam>
    /// <param name="key">풀을 구분하기 위한 고유 문자열 키</param>
    /// <param name="prefab">풀링 대상이 될 프리팹</param>
    /// <param name="count">초기 생성할 오브젝트 수</param>
    /// <param name="parent">생성될 오브젝트들의 부모 Transform (선택)</param>
    public void CreatePool<T>(string key, T prefab, int count, Transform parent = null) where T : MonoBehaviour, IPoolable
    {
        // 이미 같은 키가 존재하면 생성하지 않음
        if (!pools.ContainsKey(key))
        {
            var pool = new ObjectPool<T>(prefab, count, parent); // ObjectPool<T> 생성
            pools[key] = pool; // object로 저장 (캐스팅 필요)
        }
        else
        {
            Debug.LogWarning($"[ObjectPoolManager] 이미 존재하는 키로 풀을 생성하려 했습니다: {key}");
        }
    }

    /// <summary>
    /// 특정 풀에서 오브젝트를 꺼내 활성화합니다.
    /// </summary>
    /// <typeparam name="T">풀링할 오브젝트 타입</typeparam>
    /// <param name="key">등록된 풀의 키</param>
    /// <param name="pos">생성할 위치</param>
    /// <param name="rot">생성할 회전값</param>
    /// <returns>활성화된 오브젝트, 실패 시 null</returns>
    public T Spawn<T>(string key, Vector3 pos, Quaternion rot) where T : MonoBehaviour, IPoolable
    {
        if (pools.TryGetValue(key, out object pool))
        {
            // 해당 풀에서 오브젝트를 꺼냄
            return ((ObjectPool<T>)pool).Spawn(pos, rot);
        }

        Debug.LogError($"[ObjectPoolManager] 존재하지 않는 키로 Spawn 시도: {key}");
        return null;
    }

    /// <summary>
    /// 특정 풀로 오브젝트를 반환(비활성화)합니다.
    /// </summary>
    /// <typeparam name="T">반환할 오브젝트 타입</typeparam>
    /// <param name="key">등록된 풀의 키</param>
    /// <param name="obj">반환할 오브젝트 인스턴스</param>
    public void Despawn<T>(string key, T obj) where T : MonoBehaviour, IPoolable
    {
        if (pools.TryGetValue(key, out object pool))
        {
            ((ObjectPool<T>)pool).Despawn(obj); // 해당 풀로 반환
        }
        else
        {
            Debug.LogError($"[ObjectPoolManager] 존재하지 않는 키로 Despawn 시도: {key}");
        }
    }
}
