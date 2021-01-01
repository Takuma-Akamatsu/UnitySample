using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Randoms
{
    public static bool calcPercentEqualDownWithin(int successPercent)
    {
        return Random.Range(1, 101) < successPercent;
    }
}
