Shader "Custom/BlackHole"
{
   	Properties
	{
		_MainTex("Texture",2D) = "white"{}
		_SkyCube("Skycube", Cube) = "defaulttexture" {}//天空盒
		_HoleRadian("Hole Radidan",Float) = 0.1
		_DiskRadian("Disk Radian",Float) = 0.8
		_DistortionH("Distortion H",Float) = 0.1
		_DistortionP("Distortion P",Float) = 3
		_ROTATION_SPEED("Rotation Speed",Float) = 3

	}
	SubShader
	{
		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#define MAX_DIST 100.0
			#define MAX_STEP 2000
			#define MIN_DIST 0.001
			#define EFFECT_RADIAN 1//效果的弧度
			#define  PI 3.1415926

			struct appdata
			{
				float4 vertex:POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv:TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 ro : TEXCOORD1;
				float3 hitPos : TEXCOORD2;//返回光束位置
			};

			sampler2D _MainTex;
			samplerCUBE _SkyCube;
			float4 _MainTex_ST;//偏移
			float	_HoleRadian,_DiskRadian,_DistortionP,_DistortionH,_ROTATION_SPEED;
			float3 worldSpace;


			fixed4 SampleSky(in float3 rd)
			{
				float4 wRd = mul(unity_ObjectToWorld,float4(rd,1));//物体坐标系转到世界坐标系
				float3 sc = texCUBE(_SkyCube,wRd.xyz).rgb;//采样天空盒
				return fixed4(sc,1);//返回颜色
			}
			
			//判断是否在黑洞范围，如果在半径内
			bool InHole(float3 p)
			{
				return length(worldSpace+p.xz) < _HoleRadian;
			}
			//判断是否在星盘上
			bool InDisk(float3 p)
			{
				return length(worldSpace+p.xz)<_DiskRadian;
			}

			//星盘采样
			fixed4 SampleDisk(in float3 p)
			{
				if(InHole(p))//在黑洞中心就被吞噬了，直接黑
						return fixed4(0,0,0,1);
				//旋转贴图采样到一个假的运动上
				float d = length(p.xz);//距离星盘中心的距离
				////亮度增强
				float brightness = (cos(d*0.8*2*PI)+1)*0.5+1;
				if(d*0.8>1)
					brightness = 1;

				//把位置中心转换为uv坐标
				float2 uv = p.xz+0.5;
				fixed4 col = tex2D(_MainTex,uv);//到mainTex采样
				col *= brightness;//乘以一个亮度
				return col;
			}
			
			//经典颜色混合
			fixed4 BlendColor(fixed4 f, fixed4 b)
			{
				fixed3 rgb = lerp(b,f,f.a);
				fixed alpha = 1-(1-f.a)*(1-b.a);
				return fixed4(rgb,alpha);
			}
			
			//一个近似的方法，离黑洞距离越大，扭曲越大
			float GetDistortion(float d)
			{
				float dt = pow(_DistortionH,_DistortionP)/pow(d,_DistortionP);//距离越大，分母越大，扰动越小，d=h时，扭曲为1
				//---平滑边界，不写边界会有比较明显的分割感----
				float v = (cos(d * 0.8 * 2 * PI)+1)/2;
				if(d+0.8 > 1)
					v = 0;
				//-----------
				return  dt*v;
			}

			
			fixed4 RayMarch(float3 ro, float3 rd)
			{
				fixed4 color = fixed4(0,0,0,0);//默认透明度0，全为黑
				float3 core = worldSpace;//默认物体--黑洞中心

				//first step
				float d = length(core-ro) - EFFECT_RADIAN;//当前相机到黑洞距离 - 球的半径距离 = 相机到球表面距离
				//可以一步走到黑洞的表面附近，再一步步走到中心。不然raymaching步数有限不一定能走到中心。
				float3 p = ro + d*rd;//当前光点位置 == 位置+距离*方向
				float3 newP;

				//最大迭代步数
				for(int i =0 ;i<MAX_STEP;i++)
				{
					d = length(p-core);//先求当前点到黑洞距离
					float distortion = GetDistortion(d);//获取扭曲的量
					rd = normalize(rd + distortion*normalize(core - p));//矢量运算：方向=方向+扭曲值*（黑洞中心-当前光点位置），归一化得到新的方向
					newP = p + MIN_DIST* rd;//下一步新的点的位置
					if(p.y + newP.y <=0)//判断老的点和新的点有没有跨越星盘的平面，y=0；跨越了就代表和盘有交点，需要返回颜色
					{
						float3 interP = lerp(p,newP,newP.y/(newP.y-newP.y));//获得插值（交点值）
						if(InDisk(interP))
						{
							color = BlendColor(color,SampleDisk(interP));
						}
						
					}
					p = newP;//然后把位置更新为新的位置
				}
					if(!InHole(p))//在黑洞中心就被吞噬了，直接黑
				color = BlendColor(color,SampleSky(rd));//如果没有碰到星盘，还需要跟背景颜色混合
				return color;
				
			}
			
			
			v2f vert(appdata v)
			{
				worldSpace =    unity_ObjectToWorld._14_24_34;//默认物体--黑洞中心;
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);//裁剪顶点
				o.ro = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1.8));//世界坐标到局部坐标X世界相机坐标
				o.hitPos = v.vertex;//相机和物体的碰撞点（顶点）
				return o;
			}
		
		fixed4 frag(v2f i) : SV_Target
		{
			float3 ro = i.ro;//相机的位置
			float3 rd = normalize(i.hitPos - ro);//距离为碰撞点减去相机的位置
			fixed4 col = RayMarch(ro,rd);
			
			return col;
		}
		ENDCG
		}

	}
}
