using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // タイトル画像
    public Image guiTitle;
    // タイムアップ画像
    public Image guiTimeup;

    // ゲームの状態定義
    public enum GameState
    {
        TITLE, // タイトル
        PLAYING, // プレイ中
        TIMEUP, // タイムアップ
        TIMEUP_TO_TOTILE, // タイムアップからタイトルへ変更中
    }

    // ゲームオブジェクトの設定
    private GameState state; // ゲームの状態
    private GameObject spawnPoint; // SpawnPointゲームオブジェクト
    private GameObject score; // Scoreゲームオブジェクト
    private Timer timer; // Timerゲームオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        // 状態をタイトルに
        state = GameState.TITLE;
        // タイトル画像を表示
        guiTitle.enabled = true;
        // タイムアップ画像を非表示
        guiTimeup.enabled = false;

        // ゲームオブジェクトを取得
        spawnPoint = GameObject.Find("SpawnPoint"); // SpawnPointゲームオブジェクト
        score = GameObject.Find("Score"); // Scoreゲームオブジェクト
        timer = GameObject.Find("Timer").GetComponent<Timer>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.TITLE:
                // タイトル状態でマウスを左クリックされたらプレイ状態へ
                if (Input.GetMouseButtonUp(0))
                {
                    state = GameState.PLAYING;
                    // SpawnPointゲームオブジェクトにStartSpawn()関数を実行させ、デブリの生成を開始
                    spawnPoint.SendMessage("StartSpawn");
                    // ScoreオブジェクトにInitScore()関数を実行させ、スコアを初期化
                    score.SendMessage("InitScore");
                    // TimerコンポーネントのStartTime()関数を実行し、タイマーを開始
                    timer.StartTimer();
                    // タイトル画像を非表示
                    guiTitle.enabled = false;
                }
                break;

            case GameState.PLAYING:
                // プレイ中にTimerコンポーネントの残り時間が0になったら、タイムアップ状態に
                if (timer.GetTimeRemaining()==0)
                {
                    state = GameState.TIMEUP;
                    // SpawnPointゲームオブジェクトに、StopSpawn()関数を実行させ、デブリの生成を停止
                    spawnPoint.SendMessage("StopSpawn");
                    // TimerコンポーネントのSpopTimer()関数を実行し、タイマーを停止
                    timer.StopTimer();
                    // 画面内の宇宙ゴミを全て削除
                    DestroyAllDebris();
                    // タイムアップ画像を表示
                    guiTimeup.enabled = true;
                }
                break;

            case GameState.TIMEUP:
                // タイムアップ状態でマウス左クリックで3秒後にタイトル状態に変更する
                if (Input.GetMouseButtonUp(0))
                {
                    state = GameState.TIMEUP_TO_TOTILE;
                    StartCoroutine("ShowTitleDelayed", 3f);
                }
                break;
        }
    }

    // シーン中の全てのデブリを削除
    void DestroyAllDebris()
    {
        GameObject[] debris = GameObject.FindGameObjectsWithTag("debri");
        foreach (GameObject debri in debris)
        {
            Destroy(debri);
        }
    }

    // delayTime秒後にタイトルを表示
    IEnumerator ShowTitleDelayed(float delayTime)
    {
        // delayTime秒処理を停止
        yield return new WaitForSeconds(delayTime);
        state = GameState.TITLE;
        // タイマーをリセット
        timer.ResetTimer();
        // タイトル画像を表示
        guiTitle.enabled = true;
        // タイムアップ画像を非表示
        guiTimeup.enabled = false;
    }
}
