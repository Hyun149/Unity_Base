/// <summary>
/// 풀링 오브젝트가 구현해야 하는 인터페이스입니다.
/// 생성 시 초기화 및 반환 시 비활성화를 지원합니다.
/// </summary>
public interface IPoolable 
{
    void OnSpawn();  // 오브젝트가 사용될 때 호출
    void OnDespawn();  // 오브젝트가 풀로 돌아갈 때 호출
}
