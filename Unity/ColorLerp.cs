Color ColorLerp()
    {
        // ask herman if you confused 
        _deltaTime += Time.deltaTime; // increment the delta time manually in the UPDATE function
        float lerpDuration = 0.15f;
        float flickerOffset = lerpDuration * 0.05f; //causes breaks in the smooth lerp adding a flicker effect
        float lerpPercentage = _deltaTime / lerpDuration;

        if (_deltaTime >= lerpDuration + flickerOffset)
        {
            _colorindex += 1;
            _deltaTime = 0;
        }
        if(_colorindex == ColorArray.Length - 1)
        {
            _colorindex = 0;
        }
        return Color.Lerp(ColorArray[_colorindex], ColorArray[_colorindex + 1], lerpPercentage);
    }
