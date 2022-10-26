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
        if(true)return;   //return True�@��Update���J�b�g
        // �}�E�X�z�C�[���̒l���擾
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log("wheel > " + mouseWheel);

        // �p�����[�^�ɐݒ�
        _parameter.distance += mouseWheel;

        // �}�E�X��X���EY���̈ړ��ʂ��擾
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        Debug.Log("Mouse Move ( " + mouseX + ", " + mouseY + " )");

        // ��]
        _parameter.angles.x += mouseY;
        _parameter.angles.y += mouseX;
    }


    private void LateUpdate()
    {
        if (_parent == null || _child == null || _camera == null)
        {
            return;     // �J�����̊e�I�u�W�F�N�g��Null�Ȃ�A�ȉ��̏��������Ȃ�
        }

        if (_parameter.trackTarget != null)
        {   // �^�[�Q�b�g���ݒ肳��Ă���΁A�^�[�Q�b�g��ǂ�������
            _parameter.position = Vector3.Lerp(
                _parameter.position,                // ���g�̍��W
                _parameter.trackTarget.position,    // �^�[�Q�b�g�̍��W
                Time.deltaTime * 4f                 // �ǂ����܂ł̃��O�i���ԁj
                );
        }

        // �u�e�v�̍��W�Ɖ�]���X�V
        _parent.position = _parameter.position;
        _parent.eulerAngles = _parameter.angles;

        // �u�q�v�̍��W���X�V
        var childPos = _child.localPosition;
        childPos.z = -_parameter.distance;
        _child.localPosition = childPos;

        // �u�J�����v�̐ݒ���X�V
        _camera.fieldOfView = _parameter.fieldOfView;
        _camera.transform.localPosition = _parameter.offsetPosition;
        _camera.transform.localEulerAngles = _parameter.offsetAngles;
    }
}
