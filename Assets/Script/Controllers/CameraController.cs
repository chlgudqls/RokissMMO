using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // enum을 static 처럼 사용이 가능하네
    [SerializeField] Define.CameraMode _mode = Define.CameraMode.QuaterView;

    // 떨어진거리
    [SerializeField] Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    [SerializeField] GameObject _player = null;
    void Start()
    {
         
    } 

    public  void SetPlayer(GameObject player) { _player = player; }
    void LateUpdate()
    {
        // 이번엔 카메라가 레이케스팅을 해서 어떤걸 인식해서 상황에따라 반응
        if(_mode == Define.CameraMode.QuaterView)
        {
            if (!_player.IsValid())
                return;
            //Debug.DrawRay(_player.transform.position, _delta, Color.green, 2);

            // 광선을 캐릭으로 쏘는거임 
            // 캐릭에서 쏘는건가  
            // 플레이어가 레이케스팅하면 카메라가 반응하는거였네
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                // 이거 반대로 하면 안되네 그건아니네
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                // 떨어진 거리 + 플레이어 방향은 플레이어바라보는방향 
                transform.position = _player.transform.position + _delta.normalized * dist;
                //Debug.DrawRay(transform.position, _delta, Color.blue, 2);
            }
            else
            {
                // 플레이어에서 일정거리의 위치가 자신의 위치
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }
    
    // 
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
