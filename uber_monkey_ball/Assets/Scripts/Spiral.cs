using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class Spiral
{
    float MaxTheta;
    float MaxRadius;
    float alpha;
    float beta;
    float MaxHeight;
    Vector3 offset;
    float OffsetYRot;
    
    public Spiral(float maxRadius, float maxTheta, float height, Vector3 center, float offsetYRot)
    {
        //
        this.MaxRadius = maxRadius;
        this.MaxTheta = maxTheta;
        this.beta = 1.00535f; // golden spiral beta for angle in degrees
        this.alpha = maxRadius / (Mathf.Pow(beta, maxTheta));
        this.MaxHeight = height;
        this.offset = SolveFib3D(0) - center;
        this.OffsetYRot = offsetYRot;
    }


    public Vector3 SolveFib3D(float t)
    {
        Vector2 cartesian = SolveFib2D(t);
        float h = t * MaxHeight;
        return new Vector3(cartesian.x, h, cartesian.y) - offset;
    }
    Vector2 SolveFib2D(float t)
    {
        float angle = MaxTheta * t;
        float radius = alpha * Mathf.Pow(beta, angle);
        return new Vector2(radius * Mathf.Cos(Mathf.Deg2Rad * (angle)), radius * Mathf.Sin(Mathf.Deg2Rad * (angle)));
    }
   

}
