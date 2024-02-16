using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace DualSenseX
{
    public static class LightFlicker
    {
        private static string _flickerPattern = "mmamammmmammamamaaamammma"; //"mmnmmommommnonmmonqnmmo";
        private static float _maxIntensity = 2.2f;
        private static float _standardIntensity = 1.0f;
        private static float _range = 1.3f;
        private static float _standardRange = 1.0f;
        private static float _fps = 10f;

        private static float _currentIntensity;
        private static int _tempX, _tempY;
      
        public static Color _currentColor = new Color(0,0,0,1);

        public static bool _ledActive = false;

        public enum FlickerPattern { Flicker1, SlowStrongPulse, Candle1, FastStrobe, GentlePulse, Flicker2, Candle2, Candle3, SlowStrobe, Fluorescent, SlowPulse };

        private static string[] _patternStorage = {    "mmnmmommommnonmmonqnmmo",
                                                "abcdefghijklmnopqrstuvwxyzyxwvutsrqponmlkjihgfedcba",
                                                "mmmmmaaaaammmmmaaaaaabcdefgabcdefg",
                                                "mamamamamama",
                                                "jklmnopqrstuvwxyzyxwvutsrqponmlkj",
                                                "nmonqnmomnmomomno",
                                                "mmmaaaabcdefgmmmmaaaammmaamm",
                                                "mmmaaammmaaammmabcdefaaaammmmabcdefmmmaaaa",
                                                "aaaaaaaazzzzzzzz",
                                                "mmamammmmammamamaaamammma",
                                                "abcdefghijklmnopqrrqponmlkjihgfedcba"  };


        public static void SetFlickerPattern(string pattern)
        {
            if (_flickerPattern != pattern)
            {
                _flickerPattern = pattern;
                _tempX = 0;
                _tempY = 0;
                _currentIntensity = 0;
            }
        }

        public static void SwitchStateLED(bool newState)
        {
            if(_ledActive != newState)
            {
                _ledActive = newState;

                if(!_ledActive)
                {
                    UDPManager.SetRGB(new Color(1,1,1));
                }
            }
        }

       

        public static void SetFlickerPattern(FlickerPattern pattern)
        {
            if (_flickerPattern != _patternStorage[(int)pattern])
            {
                _flickerPattern = _patternStorage[(int)pattern];
                _tempX = 0;
                _tempY = 0;
                _currentIntensity = 0;
            }
        }

        public static void UpdateFlicker()
        {
            if(_ledActive)
            {
                if (GameManager.GetAuroraManager().GetNormalizedAlpha() >= 0.05f)
                {
                    if (GameManager.GetAuroraManager().GetNormalizedAlpha() <= 0.20f)
                    {
                        SetFlickerPattern(FlickerPattern.FastStrobe);
                    }
                    else if (GameManager.GetAuroraManager().GetNormalizedAlpha() <= 0.30f)
                    {
                        SetFlickerPattern(FlickerPattern.Fluorescent);
                    }
                    else if (GameManager.GetAuroraManager().GetNormalizedAlpha() <= 0.40f)
                    {
                        SetFlickerPattern(FlickerPattern.SlowStrobe);
                    }
                    else if (GameManager.GetAuroraManager().GetNormalizedAlpha() <= 0.70f)
                    {
                        SetFlickerPattern(FlickerPattern.SlowPulse);
                    }
                    else if (GameManager.GetAuroraManager().GetNormalizedAlpha() <= 1f)
                    {
                        SetFlickerPattern(FlickerPattern.SlowStrongPulse);
                    }

                    _tempX = (int)(Time.time * _fps);

                    _tempY = _tempX % _flickerPattern.Length;
                    _currentIntensity = (_flickerPattern[_tempY] - 'a') / (float)('m' - 'a');

                    _currentColor = GameManager.GetAuroraManager().GetAuroraColourCorrected();
                    _currentColor = new Color(_currentColor.r * _currentIntensity, _currentColor.g * _currentIntensity, _currentColor.b * _currentIntensity, 1f);

                    UDPManager.SetRGB(_currentColor);                  
                }              
            }            
        }

        public static int ConvertFloatToInt(float floatValue)
        {
            if (floatValue < 0.0f)
                floatValue = 0.0f;
            else if (floatValue > 1.0f)
                floatValue = 1.0f;

            int intValue = (int)(floatValue * 254.0f);
            return intValue;
        }
    }
}
