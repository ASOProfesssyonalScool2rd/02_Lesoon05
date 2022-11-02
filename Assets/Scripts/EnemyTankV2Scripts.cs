using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyTankV2Scripts : BaseTank
{
    public Transform Target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移動先の高さ
        Vector3 targetPos = Target.transform.position;
        targetPos.y = transform.position.y;
        
        //移動する場所を向く
        transform.LookAt(Target);
        
        //全身
        transform.Translate(0,0,_moveSpeed*Time.deltaTime);
        
    }
}
