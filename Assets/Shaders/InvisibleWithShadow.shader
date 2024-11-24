Shader "Custom/InvisibleWithShadow"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        
        Pass
        {
            // Object invisible, but still casts shadows
            ColorMask 0 // Don't render the object
            ZWrite On
            ZTest LEqual
            Lighting Off

            // Enable shadow casting
            SetTexture[_MainTex] { combine primary }
        }
    }
    Fallback "Diffuse"
}