using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // enum�� static ó�� ����� �����ϳ�
    [SerializeField] Define.CameraMode _mode = Define.CameraMode.QuaterView;

    // �������Ÿ�
    [SerializeField] Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    [SerializeField] GameObject _player = null;
    void Start()
    {
         
    } 

    public  void SetPlayer(GameObject player) { _player = player; }
    void LateUpdate()
    {
        // �̹��� ī�޶� �����ɽ����� �ؼ� ��� �ν��ؼ� ��Ȳ������ ����
        if(_mode == Define.CameraMode.QuaterView)
        {
            if (!_player.IsValid())
                return;
            //Debug.DrawRay(_player.transform.position, _delta, Color.green, 2);

            // ������ ĳ������ ��°��� 
            // ĳ������ ��°ǰ�  
            // �÷��̾ �����ɽ����ϸ� ī�޶� �����ϴ°ſ���
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                // �̰� �ݴ�� �ϸ� �ȵǳ� �װǾƴϳ�
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                // ������ �Ÿ� + �÷��̾� ������ �÷��̾�ٶ󺸴¹��� 
                transform.position = _player.transform.position + _delta.normalized * dist;
                //Debug.DrawRay(transform.position, _delta, Color.blue, 2);
            }
            else
            {
                // �÷��̾�� �����Ÿ��� ��ġ�� �ڽ��� ��ġ
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
