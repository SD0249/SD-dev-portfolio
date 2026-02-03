// SHADER command contains a string with the name of the shader
// Can use forward slash characters "/" to place your shader in sub menus
Shader "Unlit/NewUnlitShader"
{
    // Contains Shader variables that will be saved as a part of the material,
    // and displayed in the material inspector
    Properties
    {
        // Texture property
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
    }

    // A shader can contain one or more SubShaders, which are primarily used to
    // implement shaders for different GPU capabilities
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100

        // Each subshader is composed of a number of passes, and each pass
        // represents an execution of the vertex and fragment code for 
        // the same object rendered with the material of the shader
        // (Shaders that interact with lighting might need more)
        Pass
        {
            CGPROGRAM                   // Surrounds poritions of HLSL code with vertex & fragment shaders
            #pragma vertex vert         // "vert" function as vertex shader
            #pragma fragment frag       // "frag" function as pixel shader
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            // Vertex shader inputs
            struct appdata
            {
                float4 vertex : POSITION;   // Shader Semantics
                float2 uv : TEXCOORD0;
            };

            // Vertex shader outputs
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            // Texture that will be sampled
            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Vertex shader
            v2f vert (appdata v)
            {
                v2f o;

                // Transform position to clip space
                // ** Clip space
                // Transform vertex position from 'Object space' into so called 'Clip space'
                // Used by the GPU to rasterize the object on the screen
                // Underhood - Multiply with Model * View * Projection Matrix
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Just pass the texture coordinate
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);   // Sampling the texture in the fragment shader
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // ** Runs on EACH and EVERY pixel that object occupies on-screen 
            //    / Executed for ALL of them
            // Pixel shader: Returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target     // Shader Semantics
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
