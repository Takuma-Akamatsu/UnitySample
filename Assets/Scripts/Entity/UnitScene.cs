using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    /// <summary>
    /// あらゆるイベント（文章表示・効果音再生・選択肢表示・別シーンへの移動）をユニットシーンという単位で扱う為の基底クラス
    /// </summary>
    public abstract class UnitScene
    {
        // HACK: 継承先のクラスのインスタンス生成時に入れるのみで読み取り専用にしたいがなぜかできない
        /// <summary>
        /// ユニットシーン自身のID
        /// </summary>
        public abstract string unitSceneID {
            get; protected set;
        }

        // HACK: 継承先のクラスでprivate set（自身の処理でのみ更新可能）にしたいのだが、なぜかできない
        /// <summary>
        /// 次のユニットシーンのID
        /// </summary>
        public abstract string nextUnitSceneID { 
            get; protected set;
        }
    }
}