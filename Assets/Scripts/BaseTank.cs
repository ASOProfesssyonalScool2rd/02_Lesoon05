using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//全ての戦車の「親」となるクラス
public class BaseTank : MonoBehaviour
{
    [SerializeField] public GameObject UpperPoint = null;
    [SerializeField] public GameObject CannonPoint = null;
    [SerializeField] public GameObject ShotPoint = null;
    [SerializeField] public GameObject BulletPrefab = null;
    [SerializeField] public CameraManager MainCamera = null;


    [SerializeField] protected float _moveSpeed = 5.0f;
    [SerializeField] protected float _rotSpeed = 90.0f;
    [SerializeField] protected float _upperRotSpeed = 90.0f;
    [SerializeField] protected float _cannonRotSpeed = 90.0f;
    [SerializeField] protected float _shotPower = 200;


    public void Shot()
    {
        Application.targetFrameRate = 60;
        float deltaMoveSpeed = _moveSpeed * Time.deltaTime;
        float deltaRotSpeed = _rotSpeed * Time.deltaTime;
        float deltaUpperRotSpeed = _upperRotSpeed * Time.deltaTime;
        float deltaCannonRotSpeed = _cannonRotSpeed * Time.deltaTime;
         
        
        
        if(Input.GetKey(KeyCode.UpArrow) == true)
        {   // 前進
            this.transform.Translate(0, 0, deltaMoveSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {   // 後退
            this.transform.Translate(0, 0, -deltaMoveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {   // 左回転
            this.transform.Rotate(0, -deltaRotSpeed, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {   // 右旋回
            this.transform.Rotate(0, deltaRotSpeed, 0);
        }


        // === 射撃 >>>
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            // 複成
            GameObject bullet = Instantiate(
                BulletPrefab, ShotPoint.transform.position, this.transform.rotation);

            // 飛ばす
            bullet.GetComponent<Rigidbody>().AddForce(ShotPoint.transform.forward * _shotPower, ForceMode.Impulse);

            // 削除
            Destroy(bullet.gameObject, 1);
        }
        
        //ターゲットの指定
        MainCamera._parameter.trackTarget = this.transform; 
        // カメラ操作
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        MainCamera._parameter.distance += mouseWheel;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        //Debug.Log("Mouse Move ( " + mouseX + ", " + mouseY + " )");

        MainCamera._parameter.angles.x += mouseY;
        MainCamera._parameter.angles.y += mouseX;
        
        //砲台を回転
        Vector3 upperEuler = UpperPoint.transform.eulerAngles;
        UpperPoint.transform.eulerAngles = upperEuler;
    }
}
