float RemapValue(float inputvalue, Vector2 inputrange, Vector2 targetrange)
{
float remapnormalize0to1 = Mathf.InverseLerp(inputrange.x, inputrange.y, inputvalue);

return Mathf.Lerp(targetrange.x, targetrange.y, remapnormalize0to1);
}