using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // === �I�u�W�F�N�g�ݒ� >>>
    [SerializeField] public GameObject UpperPoint = null;
    [SerializeField] public GameObject CannonPoint = null;
    [SerializeField] public GameObject ShotPoint = null;
    [SerializeField] public GameObject BulletPrefab = null;

    // === �p�����[�^�ݒ� >>>
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _rotSpeed = 90.0f;
    [SerializeField] private float _upperRotSpeed = 90.0f;
    [SerializeField] private float _cannonRotSpeed = 90.0f;
    [SerializeField] private float _shotPower = 50.0f;
    [SerializeField] public CameraManager MainCamera = null;

    // Start is called before the first frame update
    
    public class Prameter
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
    [SerializeField] private Prameter _prameter;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 30;
        float deltaMoveSpeed = _moveSpeed * Time.deltaTime;
        float deltaRotSpeed = _rotSpeed * Time.deltaTime;
        float deltaUpperRotSpeed = _upperRotSpeed * Time.deltaTime;
        float deltaCannonRotSpeed = _cannonRotSpeed * Time.deltaTime;
         
        
        
        if(Input.GetKey(KeyCode.UpArrow) == true)
        {   // �O�i
            this.transform.Translate(0, 0, deltaMoveSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {   // ���
            this.transform.Translate(0, 0, -deltaMoveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {   // ����]
            this.transform.Rotate(0, -deltaRotSpeed, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {   // �E����
            this.transform.Rotate(0, deltaRotSpeed, 0);
        }


        // === �ˌ� >>>
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            // ����
            GameObject bullet = Instantiate(
                BulletPrefab, ShotPoint.transform.position, this.transform.rotation);

            // ��΂�
            bullet.GetComponent<Rigidbody>().AddForce(ShotPoint.transform.forward * _shotPower, ForceMode.Impulse);

            // �폜
            Destroy(bullet.gameObject, 5);
        }
        
        //�^�[�Q�b�g�̎w��
        MainCamera._parameter.trackTarget = this.transform; 
        // �J��������
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        MainCamera._parameter.distance += mouseWheel;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        //Debug.Log("Mouse Move ( " + mouseX + ", " + mouseY + " )");

        MainCamera._parameter.angles.x += mouseY;
        MainCamera._parameter.angles.y += mouseX;
        
        //�C�����]
        Vector3 upperEuler = UpperPoint.transform.eulerAngles;
        UpperPoint.transform.eulerAngles = upperEuler;
    }
}
