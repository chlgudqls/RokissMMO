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
        // �̺�Ʈ �̴ϼȷ� ��������
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        // �������� ���� ����
        while(_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0,_spawnTime));
        // GameScene�� �ִ��� �״�� ������ 
        //  ���� while ���� ī��Ʈ�� �������� �߰��ϱ⶧���� reservecount ���� ���μ���
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        // �����ִ� ����üũ - NavMeshAgent 
        // �׷� ���Ϳ� nav������Ʈ �ٿ��߰ڳ�
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while (true)
        {
            // 0�� �߽����� 1¥�� ���� ���� ���� �׼��� ������ǥ�� ���� �������� ����ϰ� �������� ���ؼ� ���� Ű���� ������ ��ǥ�� ���� 
            // �ű⿡ ������ǥ�� ���ؼ� ������ Pos�� ����
            // �Ÿ��� �������� ������ ������ ��ǥ �����ϱ�����    
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            // �� �� �ִ��� üũ
            // ��θ� �� �����ϴ� ���� path �̰�
            // �׸��� CalculatePath�� üũ�ϴ°Ű�
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
