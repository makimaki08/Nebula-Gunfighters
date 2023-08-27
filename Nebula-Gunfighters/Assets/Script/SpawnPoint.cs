using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // 宇宙ごみプレファブを設定
    public GameObject debri;
    // 宇宙ごみ発生間隔
    public float interbal = 1F;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnDevris() コルーチンを開始する
        StartCoroutine("SpawnDevris");
    }

    // 宇宙ごみ発生
    IEnumerator SpawwnDebris()
    {
        // 無限ループ
        while (true)
        {
            // 宇宙ごみプレハブをSpawnPointオブジェクトの位置にインスタンス化する
            Instantiate(debri, transform.position, Quaternion.identity); // Quaternion.identity=無回転
            // interbal分だけ処理を停止する
            yield return new WaitForSeconds(interbal);
        }
    }
}
