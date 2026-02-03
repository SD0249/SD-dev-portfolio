Shader "Unlit/SingleColorShader"
{
    Properties
    {
       // Color property for material inspector, default to white
       _Color ("Main Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            // Vertex Shader
            // --> This time instead of using "appdata" struct, just spell inputs manually,
            //     and instead of returning v2f struct, also just return a single output
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                // Multiplies the local vertex with the 'Model * View * Projection' to screen space
                return UnityObjectToClipPos(vertex);
            }

            // Color from the material 
            fixed4 _Color;

            // Pixel shader, no inputs needed
            fixed4 frag() : SV_Target
            {
                return _Color; // Just return it
            }
            ENDCG
        }
    }
}
