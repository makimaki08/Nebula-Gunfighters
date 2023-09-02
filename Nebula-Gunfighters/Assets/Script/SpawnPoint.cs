using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // 宇宙ごみプレファブを設定
    public GameObject debri;
    // 宇宙ごみ発生間隔
    public float interbal = 1F;

    // 宇宙ごみを発生中であることを表すフラグ
    private bool spawnStarted = false;

    // 宇宙ごみ発生開始
    void StartSpawn()
    {
        if (!spawnStarted)
        {
            spawnStarted = true;
            StartCoroutine("SpawnDebris");
        }
    }


    // 宇宙ごみ発生
    IEnumerator SpawnDebris()
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
