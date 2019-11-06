using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using System;

public class FaceParam
{
    public float RightEyeRatio { get; private set; }
    public FaceParam()
    {

    }
    public void CalcEyeRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] eyePoints = new Point[6];
        Array.Copy(points, 37, eyePoints, 0, 6);
        this.RightEyeRatio = Point.DistancePow2(eyePoints[1], eyePoints[5]) / Point.DistancePow2(eyePoints[0], eyePoints[3]);
    }
}
