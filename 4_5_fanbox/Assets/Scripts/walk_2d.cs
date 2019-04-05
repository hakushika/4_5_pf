using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk_2d : MonoBehaviour
{

    // playerの移動など。


    // 移動速度。
    public float speed = 2;
    // 移動するためのvector2
    Vector2 vec;
    // クリックしたゲームオブジェクトを入れておく。
    public GameObject clickedGameObject;


    // 角度用。
    private float angle;
    // vector2をvector3にする用。
    private Vector3 angleVec = new Vector3(0,0,0);
    // アニメーション制御。
    public Animator anim;




    // ボタンを押したところ(collider 2dがあるところ)へ移動する。
    void Update () {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
 
            if (hit2d) {
                clickedGameObject = hit2d.transform.gameObject;
                vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 角度確認。
                AngleCheck();
            }
        }

        // 移動する部分。
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(vec.x,vec.y), speed *Time.deltaTime); 
        PlayerIdle();    

    }


    // Playerの角度を取得したい。
    // http://tsubakit1.hateblo.jp/entry/2018/02/05/235634
	void AngleCheck () {
        // vector3 = (start position - target position)
		var diff = (this.transform.position - (angleVec + (Vector3)vec));
        // vecotor3 = 
        var axis = Vector3.Cross (transform.up, diff);
        // :(
        angle  = Vector3.Angle (transform.up, diff) * (axis.z < 0 ? - 1 : 1);

        
        // 角度によって。アニメーションの内容を変更。
        if (angle < -90 && angle > -180) {
            anim.Play("b_left");
        }
        if (angle > 90 && angle < 180) {
            anim.Play("b_right");
        }
        if (angle > 0 && angle < 90) {
            anim.Play("f_right");
        }
        if (angle < 0 && angle > -90) {
            anim.Play("f_left");
        }
    }


    // 近似値チェック。
    // 停止しているか確認。
    void PlayerIdle() {
        if (this.transform.localPosition.x < vec.x + 0.2 && this.transform.localPosition.x > vec.x - 0.2
        && this.transform.localPosition.y < vec.y + 0.2 && this.transform.localPosition.y > vec.y - 0.2) {
            print("停止しています");
            
        }
    }

}
