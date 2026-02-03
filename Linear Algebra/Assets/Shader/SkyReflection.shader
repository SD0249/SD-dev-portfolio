// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/SkyReflection"
{
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

            struct v2f
            {
                half3 worldRefl : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);

                // Compute world space position of the vertex
                float3 worldPos = mul(unity_ObjectToWorld, vertex).xyz;

                // Compute the world space view direction
                float worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));

                // World Space normal
                float3 worldNormal = UnityObjectToWorldNormal(normal);

                // World Space Reflection vector
                o.worldRefl = reflect(-worldViewDir, worldNormal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the default reflection cubemap, using the reflection vector
                half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, i.worldRefl);

                // Decode cubemap data into actual color
                half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);
                
                // Output it!
                fixed4 c = 0;
                c.rgb = skyColor;
                return c;
            }
            ENDCG
        }
    }
}
