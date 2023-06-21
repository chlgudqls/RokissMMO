using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 별칭이 하나 더늘어난다는뜻 클래스 이름이 중복될때 이런식으로 구분이됨@@@@@@@@@@@@@@@@@@@@@@@@@@
// 특정한것과 관련되있다고 관리하고 표시
namespace Data
{
    #region Stat
    // 제이슨파일을 메모리로 그대로 가져오기위함
    [Serializable]
    public class Stat
    {
        // 변수명이 완벽히 일치해야하는 점이있음 제이슨파일 변수명과 함께
        // 자료형도 조심
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                // 딕셔너리로 한번더 바꿔서 갖고있는거임 꺼낼때 성능적으로 좋아짐  처음부터 딕셔너리로 받을수가없기때문에 이렇게 거치는거임
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}