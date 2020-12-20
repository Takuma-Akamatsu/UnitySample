using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Entity;


// 構造体のシリアライズ
// 構造体を紐づけられるプロパティみたいなもんにする
[Serializable]
public struct PlayerStats
{
    public int movementSpeed;
    public int hitPoints;
    public bool hasHealthPotion;
}

public class GameController : MonoBehaviour {

    // 検証用コードなので意味はない
    // #region Serialize検証

    // [SerializeField] PlayerStats stats;

    // [SerializeField] Text scenarioMessageHoge;

    // [SerializeField] int maxScore;

    // [SerializeField] bool allFlag;

    // // publicでgetset設定するとＵｎｉｔｙを介さず外部で参照できるようになる
    // // Ｕｎｉｔｙはフロントを通すのでそれを飛ばした方が性能が良いかもしれない
    // public int hoge { get; set; }

    // #endregion


    // Unity内で許可したオブジェクトからは参照を許す修飾子
    // [SerializeField]は外部参照可能アクセス制限（ドラッグドロップで紐づけたオブジェクトだけ許可する）（publicとかprotectedみたいな話）
    [SerializeField] Text scenarioMessage;

    // 選択ボタン
    [SerializeField] Button optionButton;

    // ボタン表示領域パネル
    [SerializeField] Transform buttonPanel;

    Scenario currentScenario;
    int index = 0;

    // シナリオオブジェクト保持
    List<Scenario> scenarios = new List<Scenario>();

    HashSet<string> items = new HashSet<string>();

    Scenario scenario02;

    bool isCheckedKey = false;
    
    // 初期化
    void Start()
    {
        var scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "目を覚ますと、知らない部屋にいた"
            },
            NextScenarioID = "scenario02"
        };

        scenario02 = new Scenario()
        {
            ScenarioID = "scenario02",
            //コマンド選択させる場合、Textsの要素は一個のみ
            Texts = new List<string>()
            {
                "どうする？",
            },
            Options = new List<Option>
            {
                new Option()
                {
                    Text = "辺りを見渡す",
                    Action = LookAround
                },
                new Option()
                {
                    Text = "鍵を拾う",
                    Action = TakeKey,
                    IsFlagOK = () =>
                    {
                        return isCheckedKey && !items.Contains("Key");
                    }
                },
                new Option()
                {
                    Text = "扉を開ける",
                    Action = OpenDoor,
                }
            }
        };

        scenarios.Add(scenario02);
        SetScenario(scenario01);
    }
    
    void Update()
    {
        if (currentScenario != null)
        {
            // マウスで左クリックされた時
            if (Input.GetMouseButtonDown(0))
            {
                // イベントの反応チェック
                Debug.Log(EventSystem.current);
                Debug.Log(EventSystem.current.IsPointerOverGameObject(0));
                Debug.Log(buttonPanel.GetComponentsInChildren<Button>().Length);


                // マウス操作の場合
                // UIがクリックされていなかったら
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    // 処理をここに書く
                    Debug.Log("ボタン押下以外のクリック");
                    SetNextMessage();
                }

                // //ボタンをクリックしたときに反応しないようにする
                // if (EventSystem.current.IsPointerOverGameObject())
                // // if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                // {
                //     Debug.Log("クリックに反応させない");
                //     return;
                // }

                // if (buttonPanel.GetComponentsInChildren<Button>().Length < 1)
                // {
                //     Debug.Log("ボタン押下");
                //     SetNextMessage();
                // }
            }
        }
    }

    
    // シナリオの初期設定 
    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;

        scenarioMessage.text = currentScenario.Texts[0];

        if (currentScenario.Options.Count > 0)
        {
            SetNextMessage();
        }
    }

    // シナリオ内の文書送り
    void SetNextMessage()
    {
        if (currentScenario.Texts.Count > index + 1)
        {
            index++;
            scenarioMessage.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        // scenarioMessage.text = "";
        // index = 0;
        // if (string.IsNullOrEmpty(currentScenario.NextScenarioID))
        // {
        //     currentScenario = null;
        // }
        // else
        // {
        //     var nextScenario = scenarios.Find
        //         (s => s.ScenarioID == currentScenario.NextScenarioID);
        //     currentScenario = nextScenario;
        // }
        index = 0;
        if (currentScenario.Options.Count > 0)
        {
            SetOptions();
        }
        else
        {
            scenarioMessage.text = "";
            var nextScenario = scenarios.Find
            (s => s.ScenarioID == currentScenario.NextScenarioID);
            if (nextScenario != null)
            {
                SetScenario(nextScenario);
            }
            else
            {
                currentScenario = null;
            }
        }
    }

    public void LookAround()
    {
        var scenario = new Scenario();
        scenario.NextScenarioID = "scenario02";
        if (!items.Contains("Key"))
        {
            scenario.Texts.Add("足元に鍵が落ちている");
            isCheckedKey = true;
        }
        else
        {
            scenario.Texts.Add("足元には何もない");
        }
        SetScenario(scenario);
    }

    public void OpenDoor()
    {
        var scenario = new Scenario();
        if (items.Contains("Key"))
        {
            scenario.Texts.Add("鍵を使って扉を開いた");
            scenario.Texts.Add("クリアー！");
        }
        else
        {
            scenario.Texts.Add("鍵がかかっていて開かない");
            scenario.NextScenarioID = "scenario02";
        }
        SetScenario(scenario);
    }

    public void TakeKey()
    {
        var scenario = new Scenario();
        scenario.Texts.Add("鍵を拾った");
        scenario.NextScenarioID = "scenario02";
        SetScenario(scenario);
        items.Add("Key");
    }

    void SetOptions()
    {
        foreach (Option o in currentScenario.Options)
        {
            if (o.IsFlagOK())
            {
                Button b = Instantiate(optionButton);
                Text text = b.GetComponentInChildren<Text>();
                text.text = o.Text;
                b.onClick.AddListener(() => o.Action());
                b.onClick.AddListener(() => ClearButtons());
                b.transform.SetParent(buttonPanel, false);
            }
        }
    }

    void ClearButtons()
    {
        foreach (Transform t in buttonPanel)
        {
            Destroy(t.gameObject);
        }
    }

}