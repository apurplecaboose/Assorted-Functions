//lerps from point 1 to point 2 guided by animation curve.


void CurvedLerp(AnimationCurve curve, float targetTime, float dTime, Vector3 pos1, Vector2 pos2)
    {
        float curvedLerpPerct = curve.Evaluate(dTime / targetTime);
        transform.position = Vector3.Lerp(pos1, pos2, curvedLerpPerct);
    }
