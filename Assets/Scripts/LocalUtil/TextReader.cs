using System;
using System.Collections.Generic;
using UnityEngine;

namespace LocalUtil {
    public static class TextReader
    {
        public static Dictionary<int, string> getTextFileData(string filePath)
        {
            // HACK: カンマが入った文章や改行付き文章が考慮されていないので要改修

            // // アドレスを使ってロードする
            // Addressables.LoadAssetAsync<GameObject>("ExamplePrefab");

            Debug.Log("読み取り開始");
            Debug.Log(filePath);
            Debug.Log((Resources.Load(filePath, typeof(TextAsset)) as TextAsset));

            // 改行で分割
            string[] line = ((Resources.Load(filePath, typeof(TextAsset)) as TextAsset).text)
            .Split(char.Parse("\n"));

            Dictionary<int, string> text = new Dictionary<int, string>();

            for (int i = 0; i < line.Length; i++)
            {
                if (!string.IsNullOrEmpty(line[i]))
                {
                    // カンマ区切りで最初を番号、二番目を文章として取る
                    string[] SplitText = line[i].Split(char.Parse(","));
                    text.Add(int.Parse(SplitText[0]), SplitText[1]);
                }
            }

            return text;
        }
    }
}

