using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public class UnitSceneSelections : UnitScene
    {
        /// <summary>
        /// ユニットシーン自身のID
        /// </summary>
        public override string UnitSceneID
        {
            get
            {
                return UnitSceneID;
            }
            protected set
            {
                if (UnitSceneID == null)
                {
                    UnitSceneID = value;
                }
            }
        }

        /// <summary>
        /// 次のユニットシーンのID
        /// </summary>
        public override string NextUnitSceneID
        {
            get
            {
                return NextUnitSceneID;
            }
            protected set
            {
                NextUnitSceneID = value;
            }
        }

        /// <summary>
        /// クリック送り単位での文章リスト
        /// </summary>
        public List<string> Texts;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitId">ユニットシーン自身のID</param>
        /// <param name="nextUnitId">次のユニットシーンのID</param>
        /// <param name="texts">クリック送り単位での文章リスト</param>
        public UnitSceneSelections(string unitId, string nextUnitId, List<string> texts)
        {
            Texts = texts;
            UnitSceneID = unitId;
            NextUnitSceneID = nextUnitId;
        }
    }
}

