﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Global
{

    public static float GetRandomNumberInRange(RangedFloat minMaxRange)
    {
        return UnityEngine.Random.Range(minMaxRange.minValue, minMaxRange.maxValue);
    }
}

