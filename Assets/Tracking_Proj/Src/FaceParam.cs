using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using System;

public class FaceParam
{
    public float RightEyeRatio { get; private set; }
    public float MouthRatio { get; private set; }

    public FaceParam()
    {

    }
    public void CalcEyeRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] eyePoints = new Point[6];
        //右目は37番から
        Array.Copy(points, 36, eyePoints, 0, 6);
        int horizontal = Point.DistancePow2(eyePoints[0], eyePoints[3]);
        if (horizontal == 0) return;
        int vertical = Point.DistancePow2(eyePoints[1], eyePoints[5]);
        this.RightEyeRatio = (float)vertical / (float)horizontal;
    }

    public void CalcMouthRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] mouthPoints = new Point[8];
        //口の内側は61番から
        Array.Copy(points, 60, mouthPoints, 0, 8);
        int horizontal = Point.DistancePow2(mouthPoints[0], mouthPoints[4]);
        if (horizontal == 0) return;
        int vertical = Point.DistancePow2(mouthPoints[2], mouthPoints[6]);
        this.MouthRatio = (float)vertical / (float)horizontal;
    }

    public void CalcParams(Point[] points)
    {
        CalcEyeRatio(points);
        CalcMouthRatio(points);
    }
}
