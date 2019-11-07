using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using System;

public class FaceParam
{
    public float RightEyeRatio { get; private set; }
    public float LeftEyeRatio { get; private set; }
    public float MouthRatio { get; private set; }

    public FaceParam()
    {

    }
    public void CalcEyeRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] reyePoints = new Point[6];
        Point[] leyePoints = new Point[6];

        //右目は37番から
        Array.Copy(points, 36, reyePoints, 0, 6);
        int horizontal = Point.DistancePow2(reyePoints[0], reyePoints[3]);
        if (horizontal == 0) return;
        int vertical = Point.DistancePow2(reyePoints[1], reyePoints[5]);
        this.RightEyeRatio = (float)vertical / (float)horizontal;
        //左目は43番から
        Array.Copy(points, 42, leyePoints, 0, 6);
        horizontal = Point.DistancePow2(leyePoints[0], leyePoints[3]);
        if (horizontal == 0) return;
        vertical = Point.DistancePow2(leyePoints[1], leyePoints[5]);
        this.LeftEyeRatio = (float)vertical / (float)horizontal;
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
