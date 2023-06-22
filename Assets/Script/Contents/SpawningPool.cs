using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 15.0f;
    [SerializeField]
    float _spawnTime = 5.0f;
    
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        // 이벤트 이니셜로 볼수있음
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        // 같아지면 루프 종료
        while(_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0,_spawnTime));
        // GameScene에 있던거 그대로 가져옴 
        //  위의 while 안의 카운트는 스폰에서 추가하기때문에 reservecount 변수 새로선언
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        // 갈수있는 영역체크 - NavMeshAgent 
        // 그럼 몬스터에 nav컴포넌트 붙여야겠네
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while (true)
        {
            // 0을 중심으로 1짜리 구를 만든 다음 그선의 랜덤좌표를 뽑음 방향으로 사용하고 반지름을 곱해서 구를 키워서 랜덤한 좌표를 생성 
            // 거기에 고정좌표를 구해서 랜덤한 Pos를 만듦
            // 거리도 랜덤으로 구안의 랜덤한 좌표 생성하기위해    
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            // 갈 수 있는지 체크
            // 경로를 다 포함하는 건지 path 이게
            // 그리고 CalculatePath는 체크하는거고
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
