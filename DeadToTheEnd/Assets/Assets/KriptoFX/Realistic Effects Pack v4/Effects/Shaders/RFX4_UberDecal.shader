
Shader "KriptoFX/RFX4/Decal"
{
	Properties
	{
		[Header(Main Settings)]
	[Space]
	[HDR]_TintColor("Tint Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
	[Toggle(USE_ALPHA_POW)] _UseAlphaPow("Use Alpha Pow", Int) = 0
		_AlphaPow("Alpha pow", Float) = 1
		[Space]
	[Header(Light)]

	[Space]
	[Header(Noise Distortion)]
	[Toggle(USE_NOISE_DISTORTION)] _UseNoiseDistortion("Use Noise Distortion", Int) = 0
		_NoiseTex("Noise Texture (RG)", 2D) = "gray" {}
	_DistortSpeed("Distort Speed", Float) = 1
		_DistortScale("Distort Scale", Float) = .1

		[Space]
	[Header(Cutout)]
	[Toggle(USE_CUTOUT)] _UseCutout("Use Cutout", Int) = 0
	_Cutout("Cutout", Range(0, 1)) = 1
		_CutoutAlphaMul("Alpha multiplier", Float) = 1

		[Toggle(USE_CUTOUT_TEX)] _UseCutoutTex("Use Cutout Texture", Int) = 0
		_CutoutTex("Cutout Tex", 2D) = "white" {}

	[Toggle(USE_CUTOUT_THRESHOLD)] _UseCutoutThreshold("Use Cutout Threshold", Int) = 0
		[HDR]_CutoutColor("Cutout Color", Color) = (1,1,1,1)

		[Space]
	[Header(Rendering)]
	[Toggle(USE_WORLD_SPACE_UV)] _UseWorldSpaceUV("Use World Space UV", Int) = 0
		[Toggle(USE_FRAME_BLENDING)] _UseFrameBlending("Use Frame Blending", Int) = 0

		[KeywordEnum(Add, Blend, Mul)] _BlendMode("Blend Mode", Float) = 1
		_SrcMode("SrcMode", int) = 5
		_DstMode("DstMode", int) = 10
		_ZTest1("_ZTest1", int) = 5
	}

		Category{
		Tags{ "Queue" = "Transparent-1"  "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend[_SrcMode][_DstMode]
		Cull Front
		ZTest [_ZTest1]
		ZWrite Off

		SubShader{

		Pass{

		HLSLPROGRAM
#pragma vertex vert
#pragma fragment frag
		//#pragma target 3.0

#pragma multi_compile_fog
#pragma multi_compile_instancing

#pragma shader_feature USE_QUAD_DECAL

#pragma shader_feature USE_ALPHA_POW
#pragma shader_feature USE_NOISE_DISTORTION

#pragma shader_feature USE_CUTOUT
#pragma shader_feature USE_CUTOUT_TEX
#pragma shader_feature USE_CUTOUT_THRESHOLD

#pragma shader_feature USE_WORLD_SPACE_UV
#pragma shader_feature USE_FRAME_BLENDING

#pragma multi_compile _BLENDMODE_ADD _BLENDMODE_BLEND _BLENDMODE_MUL

#include "UnityCG.cginc"

		sampler2D _MainTex;
	sampler2D _NoiseTex;
	sampler2D _CutoutTex;
	sampler2D _CutoutRamp;
	UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);


	float4 _MainTex_ST;
	float4 _MainTex_NextFrame;
	float4 _NoiseTex_ST;
	float4 _CutoutTex_ST;

	UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(float4x4, _InverseTransformMatrix)
#define _InverseTransformMatrix_arr Props
		UNITY_DEFINE_INSTANCED_PROP(half4, _TintColor)
#define _TintColor_arr Props
		UNITY_DEFINE_INSTANCED_PROP(half, _Cutout)
#define _Cutout_arr Props
	UNITY_INSTANCING_BUFFER_END(Props)


		half4 _CutoutColor;
	half4 _FresnelColor;
	half4 _DistortionSpeedScale;

	half _InvFade;
	half _FresnelFadeFactor;
	half _FresnelPow;
	half _FresnelR0;
	half InterpolationValue;
	half _CutoutAlphaMul;
	half _AlphaPow;
	half _DistortSpeed;
	half _DistortScale;


	struct appdata_t {
		float4 vertex : POSITION;
		float4 normal : NORMAL;
		half4 color : COLOR;
		float4 randomID : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half4 color : COLOR;

		float4 screenUV : TEXCOORD0;
		float4x4 inverseVP : TEXCOORD1;

#if USE_QUAD_DECAL
		float2 uv : TEXCOORD6;
#endif

		UNITY_FOG_COORDS(7)

		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};

	float4x4 inverse(float4x4 m) {
		float
			a00 = m[0][0], a01 = m[0][1], a02 = m[0][2], a03 = m[0][3],
			a10 = m[1][0], a11 = m[1][1], a12 = m[1][2], a13 = m[1][3],
			a20 = m[2][0], a21 = m[2][1], a22 = m[2][2], a23 = m[2][3],
			a30 = m[3][0], a31 = m[3][1], a32 = m[3][2], a33 = m[3][3],

			b00 = a00 * a11 - a01 * a10,
			b01 = a00 * a12 - a02 * a10,
			b02 = a00 * a13 - a03 * a10,
			b03 = a01 * a12 - a02 * a11,
			b04 = a01 * a13 - a03 * a11,
			b05 = a02 * a13 - a03 * a12,
			b06 = a20 * a31 - a21 * a30,
			b07 = a20 * a32 - a22 * a30,
			b08 = a20 * a33 - a23 * a30,
			b09 = a21 * a32 - a22 * a31,
			b10 = a21 * a33 - a23 * a31,
			b11 = a22 * a33 - a23 * a32,

			det = b00 * b11 - b01 * b10 + b02 * b09 + b03 * b08 - b04 * b07 + b05 * b06;

		return float4x4(
			a11 * b11 - a12 * b10 + a13 * b09,
			a02 * b10 - a01 * b11 - a03 * b09,
			a31 * b05 - a32 * b04 + a33 * b03,
			a22 * b04 - a21 * b05 - a23 * b03,
			a12 * b08 - a10 * b11 - a13 * b07,
			a00 * b11 - a02 * b08 + a03 * b07,
			a32 * b02 - a30 * b05 - a33 * b01,
			a20 * b05 - a22 * b02 + a23 * b01,
			a10 * b10 - a11 * b08 + a13 * b06,
			a01 * b08 - a00 * b10 - a03 * b06,
			a30 * b04 - a31 * b02 + a33 * b00,
			a21 * b02 - a20 * b04 - a23 * b00,
			a11 * b07 - a10 * b09 - a12 * b06,
			a00 * b09 - a01 * b07 + a02 * b06,
			a31 * b01 - a30 * b03 - a32 * b00,
			a20 * b03 - a21 * b01 + a22 * b00) / det;
	}

	float3 WorldSpacePositionFromDepth(float2 positionNDC, float deviceDepth, float4x4 inverseVP)
	{
		float4 positionCS = float4(positionNDC * 2.0 - 1.0, deviceDepth, 1.0);
#if UNITY_UV_STARTS_AT_TOP
		positionCS.y = -positionCS.y;
#endif
		float4 hpositionWS = mul(inverseVP, positionCS);
		return hpositionWS.xyz / hpositionWS.w;
	}


	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_TRANSFER_INSTANCE_ID(v, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

		o.vertex = UnityObjectToClipPos(v.vertex);
		o.color = v.color;

		o.inverseVP = inverse(UNITY_MATRIX_VP);
		o.screenUV = ComputeScreenPos(o.vertex);

#if USE_QUAD_DECAL
	#ifdef USE_WORLD_SPACE_UV
			o.uv = mul(unity_ObjectToWorld, v.vertex).xz;
	#else
			o.uv = v.vertex.xz + 0.5;
	#endif
#endif
		UNITY_TRANSFER_FOG(o,o.vertex);

		return o;
	}


	half4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_INSTANCE_ID(i);
#if USE_QUAD_DECAL
		float2 uv = i.uv;
		float3 opos = uv.xyy + 0.5;
		float projClipFade = 1;
#else

		float2 screenUV = i.screenUV.xy / i.screenUV.w;
		float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenUV);

		//float3 wpos = mul(unity_CameraToWorld, float4(i.ray * depth, 1)).xyz;

		float3 wpos = WorldSpacePositionFromDepth(screenUV, depth, i.inverseVP);
		//return float4(frac(wpos.xz), 0, 1);
		//float3 opos = mul(unity_WorldToObject, float4(wpos, 1)).xyz;
		float3 opos = mul(unity_WorldToObject, float4(wpos, 1));
		//float3 opos = mul(_InverseTransformMatrix, float4(wpos, 1)).xyz;
		float3 stepVal = saturate((0.5 - abs(opos.xyz)) * 10000);

		float projClipFade = stepVal.x * stepVal.y * stepVal.z * (1 - abs(opos.y * 2));
		projClipFade = pow(projClipFade, 0.2);

#ifdef USE_WORLD_SPACE_UV
		float2 uv = wpos.xz;
#else
		float2 uv = opos.xz + 0.5;
#endif

#endif


		float2 uvMain = uv * _MainTex_ST.xy + _MainTex_ST.zw;
	#ifdef USE_FRAME_BLENDING
		float2 uvNextFrame = uv * _MainTex_NextFrame.xy + _MainTex_NextFrame.zw;
	#endif


	#if defined (USE_NOISE_DISTORTION)
		float2 uvNoise = uv * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
	#endif
	#if defined (USE_CUTOUT_TEX)
		float2 uvCutout = (opos.xz + 0.5) * _CutoutTex_ST.xy + _CutoutTex_ST.zw;
	#endif


	#ifdef USE_NOISE_DISTORTION

		half2 distortMask = tex2D(_NoiseTex, uvNoise) * 2 - 1;

		half4 tex = tex2D(_MainTex, uvMain + distortMask * _DistortScale + _DistortSpeed * _Time.xx);
		half4 tex2 = tex2D(_MainTex, uvMain - distortMask * _DistortScale - _DistortSpeed * _Time.xx * 1.4);
		tex *= tex2;
	#ifdef USE_FRAME_BLENDING
		half4 tex3 = tex2D(_MainTex, uvNextFrame);
		tex = lerp(tex, tex3, InterpolationValue);
	#endif

	#else
		half4 tex = tex2D(_MainTex, uvMain);

	#ifdef USE_FRAME_BLENDING
		half4 tex2 = tex2D(_MainTex, uvNextFrame);
		tex = lerp(tex, tex2, InterpolationValue);
	#endif
	#endif



		half4 tintColor = UNITY_ACCESS_INSTANCED_PROP(_TintColor_arr, _TintColor);
		tintColor.rgb = tintColor.rgb * tintColor.rgb* 2;
		half4 res = tex * tintColor;
		res.rgb *= 2;
	#ifdef USE_CUTOUT

	#ifdef USE_CUTOUT_TEX
		half mask = tex2D(_CutoutTex, uvCutout).a;
	#else
		half mask = tex.a;
	#endif

		half cutout = 1 - UNITY_ACCESS_INSTANCED_PROP(_Cutout_arr, _Cutout) * i.color.a;
		half alphaMask = saturate((mask - (cutout * 2 - 1)) * _CutoutAlphaMul) * res.a;
		res.a = alphaMask;

	#endif



		res.a = saturate(res.a * projClipFade);
	#ifdef USE_ALPHA_POW
		res.rgb *= pow(res.a, _AlphaPow);
	#endif

	#ifdef _BLENDMODE_ADD
		UNITY_APPLY_FOG_COLOR(i.fogCoord, res, half4(0,0,0,0));
	#endif
	#ifdef _BLENDMODE_BLEND
		UNITY_APPLY_FOG(i.fogCoord, res);
	#endif
	#ifdef _BLENDMODE_MUL
		res = lerp(1, res, saturate(res.a * 2));
		UNITY_APPLY_FOG_COLOR(i.fogCoord, res, half4(1,1,1,1)); // fog towards white due to our blend mode
	#endif

		return res;
	}
		ENDHLSL
	}
	}
	}


		CustomEditor "RFX4_UberDecalGUI"
}
