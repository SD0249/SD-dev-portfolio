Shader "Unlit/WorldSpaceNormals"
{
    // No properties block this time bc the colors are decided by each normal
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"    // This include file contains UnityObjectToWorldNormal helper function

            struct v2f
            {
                // We'll output world space normal as one of regular ("texcoord")
                half3 worldNormal : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
            };

            // Vertex shader takes object space normal as input too
            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);

                // UnityCG.cginc file contains function to transform
                // normal from object to world space, use that
                o.worldNormal = UnityObjectToWorldNormal(normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target     // i stands for interpolators here, typically labeled with TEXCOORDn semantic, and each can be up to a 4-component vector
            {
                fixed4 c = 0;

                // normal is a 3D vector with xyz components; in -1 .. 1 range
                // To display it as color, bring the range into 0 .. 1
                // and put into red, green, blue components
                c.rgb = i.worldNormal * 0.5 + 0.5;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return c;
            }
            ENDCG
        }
    }
}
