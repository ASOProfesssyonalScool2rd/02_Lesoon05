using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    [Serializable]
    public class Parameter
    {
        public Transform trackTarget;
        public Vector3 position;
        public Vector3 angles = new Vector3(10f, 0f, 0f);
        public float distance = 7f;
        public float fieldOfView = 45f;
        public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
        public Vector3 offsetAngles;
    }
    
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _child;
    [SerializeField] private Camera _camera;
    [SerializeField] public Parameter _parameter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(true)return;   //return True　でUpdateをカット
        // マウスホイールの値を取得
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log("wheel > " + mouseWheel);

        // パラメータに設定
        _parameter.distance += mouseWheel;

        // マウスのX軸・Y軸の移動量を取得
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        Debug.Log("Mouse Move ( " + mouseX + ", " + mouseY + " )");

        // 回転
        _parameter.angles.x += mouseY;
        _parameter.angles.y += mouseX;
    }


    private void LateUpdate()
    {
        if (_parent == null || _child == null || _camera == null)
        {
            return;     // カメラの各オブジェクトがNullなら、以下の処理をしない
        }

        if (_parameter.trackTarget != null)
        {   // ターゲットが設定されていれば、ターゲットを追いかける
            _parameter.position = Vector3.Lerp(
                _parameter.position,                // 自身の座標
                _parameter.trackTarget.position,    // ターゲットの座標
                Time.deltaTime * 4f                 // 追いつくまでのラグ（時間）
                );
        }

        // 「親」の座標と回転を更新
        _parent.position = _parameter.position;
        _parent.eulerAngles = _parameter.angles;

        // 「子」の座標を更新
        var childPos = _child.localPosition;
        childPos.z = -_parameter.distance;
        _child.localPosition = childPos;

        // 「カメラ」の設定を更新
        _camera.fieldOfView = _parameter.fieldOfView;
        _camera.transform.localPosition = _parameter.offsetPosition;
        _camera.transform.localEulerAngles = _parameter.offsetAngles;
    }
}
