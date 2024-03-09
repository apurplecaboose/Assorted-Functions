using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CamShaker : MonoBehaviour
{
    ////For Use in Development

    //[SerializeField][Tooltip("Attack and Decay rate of change")][Range(1,4)] int Nth_Power;
    //[SerializeField] float Magnitude, AttackTime, PeakTime, DecayTime, FreqX, FreqY;
    //[Header("Optional Overloads")]
    //[SerializeField]/*[HideInInspector]*/ bool XorY;
    //[SerializeField]/*[HideInInspector]*/ float SeedX, SeedY, Amplitude;

    //void Update()
    //{
    //    TestShake();
    //}

    //void TestShake()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //PerlinCameraShake1D(Nth_Power, true , Magnitude, AttackTime, PeakTime, DecayTime, FreqX);
    //        //PerlinCameraShake2D(Nth_Power, Magnitude, AttackTime, PeakTime, DecayTime, FreqX, FreqY);
    //        //RawPerlinNoise1D(true, FreqX, Amplitude);
    //        //RawPerlinNoise2D(FreqX, FreqY, Amplitude);
    //    }
    //}

    //Hardcoded Seed, w/ Amplitude Control
    public async void PerlinCameraShake1D(int nth_Power, bool X_or_Y, float peak, float attackTime, float peakTime, float decayTime, float freq)
    {
        Vector2 perlinOutput;
        float timeElapsed = 0;
        float amplitude;
        //while loop paramatrizes time T
        while (timeElapsed <= attackTime + peakTime + decayTime + 0.001f)
        {
            perlinOutput = RawPerlinNoise1D();
            amplitude = ParametricAmplitude();
            timeElapsed += Time.deltaTime;

            this.transform.position = new Vector3(amplitude * perlinOutput.x, amplitude * perlinOutput.y, this.transform.position.z);

            if (timeElapsed > (attackTime + peakTime + decayTime + 0.001f))
            {
                break;
            }
            await Task.Yield();
        }
        Vector2 RawPerlinNoise1D()
        {
            //X axis = true
            //Y axis = false
            float NormalizePerlin(float seed, float frequency, float t)
            {
                return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
            }
            float component = NormalizePerlin(1, freq, Time.time);
            if (X_or_Y)
            {
                return new Vector2(component, this.transform.position.y);
            }
            else
            {
                return new Vector2(this.transform.position.x, component);
            }
        }
        float ParametricAmplitude()
        {
            float amplitude_t;
            if (timeElapsed <= attackTime)
            {
                amplitude_t = (peak / attackTime) * Mathf.Pow(timeElapsed, nth_Power); //y = mx^n
            }
            else if (timeElapsed <= attackTime + peakTime)
            {
                amplitude_t = peak;
            }
            else if (timeElapsed <= attackTime + peakTime + decayTime)
            {
                float decayslope = peak / (-decayTime); // (y-y1)/(x-x1) then simplify
                amplitude_t = decayslope * (Mathf.Pow(timeElapsed, nth_Power) - attackTime + peakTime) + peak; //same as before now to the nth power
            }
            else
            {
                amplitude_t = 0; // resets camera back to 0 ;
            }
            return amplitude_t;
        }
    }
    public async void PerlinCameraShake2D(int nth_Power, float peak, float attackTime, float peakTime, float decayTime, float freq_X, float freq_Y)
    {
        Vector2 perlinOutput;
        float timeElapsed = 0;
        float amplitude;
        //while loop paramatrizes time T
        while (timeElapsed <= attackTime + peakTime + decayTime + 0.001f)
        {
            perlinOutput = RawPerlinNoise2D();
            amplitude = ParametricAmplitude();
            timeElapsed += Time.deltaTime;

            this.transform.position = new Vector3(amplitude * perlinOutput.x, amplitude * perlinOutput.y, this.transform.position.z);

            if (timeElapsed > (attackTime + peakTime + decayTime + 0.001f))
            {
                break;
            }
            await Task.Yield();
        }
        Vector2 RawPerlinNoise2D()
        {
            float NormalizePerlin(float seed, float frequency, float t)
            {
                return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
            }
            float x_component = NormalizePerlin(1, freq_X, Time.time);
            float y_component = NormalizePerlin(5, freq_Y, Time.time);
            return new Vector2(x_component, y_component);
        }
        float ParametricAmplitude()
        {
            float amplitude_t;
            if (timeElapsed <= attackTime)
            {
                amplitude_t = (peak / attackTime) * Mathf.Pow(timeElapsed, nth_Power); //y = mx^n
            }
            else if (timeElapsed <= attackTime + peakTime)
            {
                amplitude_t = peak;
            }
            else if (timeElapsed <= attackTime + peakTime + decayTime)
            {
                float decayslope = peak / (-decayTime); // (y-y1)/(x-x1) then simplify
                amplitude_t = decayslope * (Mathf.Pow(timeElapsed, nth_Power) - attackTime + peakTime) + peak; //same as before now to the nth power
            }
            else
            {
                amplitude_t = 0; // resets camera back to 0 ;
            }
            return amplitude_t;
        }
    }
    
    //Dynamic Seed, w/ Amplitude Control
    public async void PerlinCameraShake1D(float seed_XY, int nth_Power, bool X_or_Y, float peak, float attackTime, float peakTime, float decayTime, float freq)
    {
        Vector2 perlinOutput;
        float timeElapsed = 0;
        float amplitude;
        //while loop paramatrizes time T
        while (timeElapsed <= attackTime + peakTime + decayTime + 0.001f)
        {
            perlinOutput = RawPerlinNoise1D();
            amplitude = ParametricAmplitude();
            timeElapsed += Time.deltaTime;

            this.transform.position = new Vector3(amplitude * perlinOutput.x, amplitude * perlinOutput.y, this.transform.position.z);

            if (timeElapsed > (attackTime + peakTime + decayTime + 0.001f))
            {
                break;
            }
            await Task.Yield();
        }
        Vector2 RawPerlinNoise1D()
        {
            //X axis = true
            //Y axis = false
            float NormalizePerlin(float seed, float frequency, float t)
            {
                return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
            }
            float component = NormalizePerlin(seed_XY, freq, Time.time);
            if (X_or_Y)
            {
                return new Vector2(component, this.transform.position.y);
            }
            else
            {
                return new Vector2(this.transform.position.x, component);
            }
        }
        float ParametricAmplitude()
        {
            float amplitude_t;
            if (timeElapsed <= attackTime)
            {
                amplitude_t = (peak / attackTime) * Mathf.Pow(timeElapsed, nth_Power); //y = mx^n
            }
            else if (timeElapsed <= attackTime + peakTime)
            {
                amplitude_t = peak;
            }
            else if (timeElapsed <= attackTime + peakTime + decayTime)
            {
                float decayslope = peak / (-decayTime); // (y-y1)/(x-x1) then simplify
                amplitude_t = decayslope * (Mathf.Pow(timeElapsed, nth_Power) - attackTime + peakTime) + peak; //same as before now to the nth power
            }
            else
            {
                amplitude_t = 0; // resets camera back to 0 ;
            }
            return amplitude_t;
        }
    }
    public async void PerlinCameraShake2D(float seed_X, float seed_Y, int nth_Power, float peak, float attackTime, float peakTime, float decayTime, float freq_X, float freq_Y)
    {
        Vector2 perlinOutput;
        float timeElapsed = 0;
        float amplitude;
        //while loop paramatrizes time T
        while (timeElapsed <= attackTime + peakTime + decayTime + 0.001f)
        {
            perlinOutput = RawPerlinNoise2D();
            amplitude = ParametricAmplitude();
            timeElapsed += Time.deltaTime;

            this.transform.position = new Vector3(amplitude * perlinOutput.x, amplitude * perlinOutput.y, this.transform.position.z);

            if (timeElapsed > (attackTime + peakTime + decayTime + 0.001f))
            {
                break;
            }
            await Task.Yield();
        }
        Vector2 RawPerlinNoise2D()
        {
            float NormalizePerlin(float seed, float frequency, float t)
            {
                return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
            }
            float x_component = NormalizePerlin(seed_X, freq_X, Time.time);
            float y_component = NormalizePerlin(seed_Y, freq_Y, Time.time);
            return new Vector2(x_component, y_component);
        }
        float ParametricAmplitude()
        {
            float amplitude_t;
            if (timeElapsed <= attackTime)
            {
                amplitude_t = (peak / attackTime) * Mathf.Pow(timeElapsed, nth_Power); //y = mx^n
            }
            else if (timeElapsed <= attackTime + peakTime)
            {
                amplitude_t = peak;
            }
            else if (timeElapsed <= attackTime + peakTime + decayTime)
            {
                float decayslope = peak / (-decayTime); // (y-y1)/(x-x1) then simplify
                amplitude_t = decayslope * (Mathf.Pow(timeElapsed, nth_Power) - attackTime + peakTime) + peak; //same as before now to the nth power
            }
            else
            {
                amplitude_t = 0; // resets camera back to 0 ;
            }
            return amplitude_t;
        }
    }
    
    //Hardcoded Seed, constant Amplitude
    public async void RawPerlinNoise1D(bool X_or_Y, float freq, float amp, float duration)
    {
        //X axis = true
        //Y axis = false
        float elapsedTime = 0;
        while (elapsedTime <= duration + 0.01f)
        {
            float component = amp * NormalizePerlin(1, freq, Time.time);
            if (X_or_Y)
            {
                this.transform.position = new Vector3(component, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, component, this.transform.position.z);
            }
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= duration)
            {
                this.transform.position = new Vector3(0, 0, this.transform.position.z);
                return;
            }
            await Task.Yield();
        }
        
        float NormalizePerlin(float seed, float frequency, float t)
        {
            return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
        } 
    }
    public async void RawPerlinNoise2D(float freq_X, float freq_Y, float amp, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime <= duration + 0.01f)
        {
            float x_component = amp * NormalizePerlin(1, freq_X, Time.time);
            float y_component = amp * NormalizePerlin(5, freq_Y, Time.time);
            this.transform.position = new Vector3(x_component, y_component, this.transform.position.z);

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                this.transform.position = new Vector3(0, 0, this.transform.position.z);
                return;
            }
            await Task.Yield();
        }
        float NormalizePerlin(float seed, float frequency, float t)
        {
            return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
        }
    }
    //Dynamic Seed, constant Amplitude
    public async void RawPerlinNoise1D(int seed, bool X_or_Y, float freq, float amp, float duration)
    {
        //X axis = true
        //Y axis = false
        float elapsedTime = 0;
        while (elapsedTime <= duration + 0.01f) 
        {
            float component = amp * NormalizePerlin(seed, freq, Time.time);
            if (X_or_Y)
            {
                this.transform.position = new Vector3(component, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, component, this.transform.position.z);
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                this.transform.position = new Vector3(0, 0, this.transform.position.z);
                return;
            }
            await Task.Yield();
        }

        float NormalizePerlin(float seed, float frequency, float t)
        {
            return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
        }
    }
    public async void RawPerlinNoise2D(int seed_X, int seed_Y, float freq_X, float freq_Y, float amp, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime <= duration + 0.01f)
        {
            float x_component = amp * NormalizePerlin(seed_X, freq_X, Time.time);
            float y_component = amp * NormalizePerlin(seed_Y, freq_Y, Time.time);
            this.transform.position = new Vector3(x_component, y_component, this.transform.position.z);

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                this.transform.position = new Vector3(0, 0, this.transform.position.z);
                return;
            }
            await Task.Yield();
        }
        float NormalizePerlin(float seed, float frequency, float t)
        {
            return 2f * (Mathf.Clamp(Mathf.PerlinNoise(seed, frequency * t), 0, 1) - 0.5f);
        }
    }
}
