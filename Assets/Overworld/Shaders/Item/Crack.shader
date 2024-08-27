Shader "Unlit/Crack"
{
    Properties
    {
        [Header(Albedo)]
        [MainTexture] _MainTex("Texture", 2D) = "white" {}
        [Header(Crack)]
        _CrackProgress("クラック進行具合", Range(0.0, 1.0)) = 0.0
        [HDR]
        _CrackColor("クラック色", Color) = (0.0, 0.0, 0.0, 1.0)
        _CrackDetailedness("クラック模様の細かさ", Range(0.0, 8.0)) = 3.0
        _CrackWidth("クラックの幅", Range(0.01, 0.1)) = 0.05
        _CrackWallWidth("クラックの壁部分の幅", Range(0.001, 0.2)) = 0.08
        [Space]
        _RandomSeed("クラック模様のランダムシード(非負整数のみ可)", Int) = 0
    }

    CGINCLUDE

    sampler2D _MainTex;
    half4 _CrackColor;
    float _CrackProgress;
    float4 paint(float2 uv, float level)
    {
        if(_CrackProgress == 1.0f)
        {
            discard;
        }
        fixed4 col = tex2D(_MainTex, uv);
        if(level > 0.0)
        {
            col = lerp(col, _CrackColor, level);
        }
        return col;
    }

    ENDCG

    SubShader
    { 
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha 
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            float _CrackDetailedness;
            float _CrackDepth;
            float _CrackWidth;
            float _CrackWallWidth;
            uint _RandomSeed;
            float4 _MainTex_TexelSize;

            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct fin
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            uint Xorshift32(uint value) {
                value = value ^ (value << 13);
                value = value ^ (value >> 17);
                value = value ^ (value << 5);
                return value;
            }

            float ToFloat(uint value) {
                const float precion = 100000000.0;
                return (value % precion) * rcp(precion);
            }

            float3 Random3(uint3 src, int seed) {
                uint3 random;
                random.x = Xorshift32(mad(src.x, src.y, src.z));
                random.y = Xorshift32(mad(random.x, src.z, src.x) + seed);
                random.z = Xorshift32(mad(random.y, src.x, src.y) + seed);
                random.x = Xorshift32(mad(random.z, src.y, src.z) + seed);

                return float3(ToFloat(random.x), ToFloat(random.y), ToFloat(random.z));
            }

           void CreateVoronoi(float2 pos, out float2 closest, out float2 secondClosest, out float secondDistance) {
                const uint offset = 100;
                uint2 cellIdx;
                float2 reminders = modf(pos + offset, cellIdx);

                float2 closestDistances = 8.0;

                [unroll]
                for(int i = -1; i <= 1; i++)
                [unroll]
                for(int j = -1; j <= 1; j++) {
                    int2 neighborIdx = int2(i, j);

                    float2 randomPos = Random3(uint3(cellIdx + neighborIdx, _RandomSeed), _RandomSeed).xy;
                    float2 vec = randomPos + float2(neighborIdx) - reminders;
                    float distance = dot(vec, vec);

                    if (distance < closestDistances.x) {
                        closestDistances.y = closestDistances.x;
                        closestDistances.x = distance;
                        secondClosest = closest;
                        closest = vec;
                    } else if (distance < closestDistances.y) {
                        closestDistances.y = distance;
                        secondClosest = vec;
                    }
                }

                secondDistance = closestDistances.y;
            }

            float GetVoronoiBorder(float2 pos, out float secondDistance) {
                float2 a, b;
                CreateVoronoi(pos, a, b, secondDistance);

                float distance = dot(0.5 * (a + b), normalize(b - a));

                return 1.0 - smoothstep(_CrackWidth, _CrackWidth + _CrackWallWidth, distance);
            }

            float GetCrackLevel(float2 pos)
            {
                float ratio = _MainTex_TexelSize.y / _MainTex_TexelSize.x;
                float2 normalizedPos = float2(pos.x * ratio, pos.y);
                
                float secondDistance;
                float level = GetVoronoiBorder(normalizedPos * _CrackDetailedness, secondDistance);

                float f2Factor = 1.0 - sin(_CrackProgress * 3.14 * 0.5);
                float minTh = (2.9 * f2Factor);
                float maxTh = (3.5 * f2Factor);
                float factor = smoothstep(minTh, maxTh, secondDistance * 2.0);
                level *= factor;

                return level;
            }            
            fin vert(appdata v)
            {
                fin o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 frag(fin IN) : SV_TARGET
            {
                float level = GetCrackLevel(IN.texcoord.xy);
                return paint(IN.texcoord.xy , level);
            }

            ENDCG
        }
    }
}
