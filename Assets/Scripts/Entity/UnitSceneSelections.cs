﻿using System.Collections;
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
        public override string unitSceneID
        {
            get
            {
                return unitSceneID;
            }
            protected set
            {
                if (unitSceneID == null)
                {
                    unitSceneID = value;
                }
            }
        }

        /// <summary>
        /// 次のユニットシーンのID
        /// </summary>
        public override string nextUnitSceneID
        {
            get
            {
                return nextUnitSceneID;
            }
            protected set
            {
                nextUnitSceneID = value;
            }
        }

        /// <summary>
        /// クリック送り単位での文章リスト
        /// </summary>
        public List<string> texts;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitId">ユニットシーン自身のID</param>
        /// <param name="nextUnitId">次のユニットシーンのID</param>
        /// <param name="texts">クリック送り単位での文章リスト</param>
        public UnitSceneSelections(string unitId, string nextUnitId, List<string> textList)
        {
            texts = textList;
            unitSceneID = unitId;
            nextUnitSceneID = nextUnitId;
        }
    }
}

