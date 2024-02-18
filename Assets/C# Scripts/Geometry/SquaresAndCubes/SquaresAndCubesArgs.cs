using System;
using System.Collections.Generic;
using UnityEngine;

public class SquaresAndCubesArgs : PALEventArgs
{
    public int Length { get; private set; }
    public int Dimension { get; private set; }

    public SquaresAndCubesArgs(int length, int dimension)
    {
        Length = length;
        Dimension = dimension;
    }
}

