using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    // 1秒あたりの回転角度
    public float angle = 30F;
    // 破壊時の時点
    public int score = 10;

    // 回転の中心左表
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        // シーン中のEarthオブジェクトへアクセスして、EarthオブジェクトのTransformコンポーネントへアクセスする
        Transform target = GameObject.Find("Earth").transform;
        // Earthオブジェクトの位置情報を取得する
        targetPos = target.position;
        // 宇宙ごみの正面のむきを、Earthの方向に向ける
        transform.LookAt(target);
        // 宇宙ごみを0から360の範囲でZ軸を中心に回転させておく
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        // Earthを中心に宇宙ごみの現在の上方向に、毎秒angle分だけ回転する
        Vector3 axis = transform.TransformDirection(Vector3.up);
        transform.RotateAround(targetPos, axis, angle * Time.deltaTime);
    }

    void OnMouseDown()
    {
        // クリックされたら宇宙ごみを消す
        Destroy(gameObject);
    }
}
