Shader "Unlit/lensing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SkyCube("Skycube", Cube) = "defaulttexture" {}
        _HoleRadian ("Hole Radian",Float) = 0.001
        _DiskRadian ("Disk Radian", Float) = 0.8
        _DistortionH ("Distortion H",Float) = 0.015
        _DistortionP ("Distortion P",Float) = 3
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define MAX_DIST 100.0
            #define MAX_STEP 1000
            #define MIN_DIST 0.001
            #define EFFECT_RADIAN  1
            #define PI 3.1415926

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 ro : TEXCOORD1;
                float3 hitPos: TEXCOORD2;
            };


            sampler2D _MainTex;
            samplerCUBE _SkyCube;
            float4 _MainTex_ST;
            float _CoreRadian,_HoleRadian,_DiskRadian,_DistortionH,_DistortionP;

            float GetDist(float3 p){
                return length(p)-.5;
            }

            float3 GetNormal(float3 p){
                float2 e = float2(1e-2,0);
                float3 n = GetDist(p) - float3(
                    GetDist(p-e.xyy),
                    GetDist(p-e.yxy),
                    GetDist(p-e.yyx)
                );
                return normalize(n);
            }

            fixed4 SampleSky(in float3 rd){
                float4 wRd = mul(unity_ObjectToWorld,float4(rd,0));
                float3 sc = texCUBE(_SkyCube, wRd.xyz).rgb;
                return fixed4(sc,1);
            }
            
            bool InHole(float3 p){
                return length(p.xz)<_HoleRadian;
            }

            bool InDisk(float3 p){
                return length(p.xz)<_DiskRadian;
            }

            fixed4 SampleDisk(in float3 p){
                if (InHole(p))
                    return fixed4(0,0,0,1);
                // Rotate the texture sampling to fake motion.
                float d = length(p.xz);
                float brightness = (cos(d*0.8*2*PI)+1)*0.5+1;
                if (d*0.8>1)
                    brightness = 1;

                float2 uv = p.xz+0.5;
                fixed4 col = tex2D(_MainTex, uv);
                col *= brightness;
                return col;
            }

        
            float PointOnLine(float3 ro, float3 rd, float3 p){
                float3 v = p - ro;
                float d = dot(v, normalize(rd));
                return d;
            }

            float PointOnDisk(float3 ro, float3 rd){
                if (rd.y==0)
                    return MAX_DIST;

                float d = -ro.y/rd.y;
                return d;
            }

            float3 BezierPointOnDisk(float3 p1, float3 p2, float3 p3){
                float l = 0.0;
                float r = 1.0;
                float3 p = p1;
                float m;
                int i = 0;
                while (abs(p.y)>0.01 || i>2000){
                    m = lerp(l,r,0.5);
                    p = lerp(lerp(p1,p2,m),lerp(p2,p3,m),m);
                    if (p.y*p1.y<0){
                        r = m;
                    }else{
                        l = m;
                    }
                    i++;
                }
                return p;
                
            }

            fixed4 BlendColor(fixed4 f, fixed4 b){
                fixed3 rgb = lerp(b,f,f.a);
                fixed alpha = 1-(1-f.a)*(1-b.a);
                return fixed4(rgb,alpha);
            }

            float GetDistortion(float d){
                float dt = pow(_DistortionH,_DistortionP)/pow(d,_DistortionP);
                float v = (cos(d*0.8*2*PI)+1)/2;
                if (d*0.8>1)
                    v = 0;
                return dt*v;
            }

            fixed4 RayMarch(float3 ro, float3 rd){
                fixed4 color = fixed4(0,0,0,0);
                float3 core = float3(0,0,0);


                //first step
                float d = length(core-ro) - EFFECT_RADIAN;
                float3 p = ro + d*rd;
                float3 newP;
                
                for (int i=0;i<MAX_STEP;i++){
                    d = length(p-core);
                    float distortion = GetDistortion(d);
                    rd = normalize(rd + distortion * normalize(core-p));
                    newP = p + MIN_DIST*rd;
                    if(p.y*newP.y<=0){
                        float3 interP = lerp(p,newP,newP.y/(newP.y-p.y));
                        if (InDisk(interP)){
                            color = BlendColor(color,SampleDisk(interP));
                        }
                        
                    }
                    p = newP;
                    
                }
 
                color = BlendColor(color,SampleSky(rd));
                return color;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //object coord to camera view coord
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.ro =  mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1.0));  //mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1));
                o.hitPos = v.vertex;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                float3 ro = i.ro;
                float3 rd = normalize(i.hitPos - ro);
                fixed4 col = RayMarch(ro,rd);

                return col;
            }
            ENDCG
        }
    }
}
