Shader "Hidden/Heathen/OcclusionShader" {
	Properties {
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BumpMap("Normal Map", 2D) = "bump" {}
		_CullColor("Cull Color", Color) = (0,0,0,0)
		_CullTex("Cull Albedo", 2D) = "white" {}
		_CullStrength("Cull Strength", Range(0.0, 1.0)) = 1.0
		_CullRim("Cull Rim", Range(0.0, 0.99)) = 0.2
		
		[HideInInspector] _ZTest ("__zt", Float) = 1.0
	}
	SubShader {
		//AlphaTest-100
		Tags {  "RenderType"="Transparent" }
		LOD 150
		
		ZWrite Off
		ZTest [_ZTest]
		Blend SrcAlpha OneMinusSrcAlpha
		//Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard decal:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _CullTex;
		sampler2D _BumpMap;

		struct Input {
			float2 uv_BumpMap;
			float2 uv_CullTex;
			float3 viewDir;
		};

		fixed4 _CullColor;
		float _ZTest;
		float _CullStrength;
		float _CullRim;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_CullTex, IN.uv_CullTex);
			
			
			float alphaMult = 1;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			if(_ZTest > 0.0 || _CullStrength == 0.0)
			{
				alphaMult = 0;
				c = (0,0,0,0);
			}
			float effectPower = pow (rim, _CullRim);
			
			o.Albedo = c.rgb * _CullColor.rgb;
			o.Emission = (_CullColor.rgb * (1-effectPower))* _CullColor.a;		
			
			//if(_CullRim <= 0.0)
			//	rim = 1;
			
			float finalAlpha = (c.a * _CullStrength * rim) * alphaMult;
			
			if(_CullRim <= 0.0)
				finalAlpha = _CullStrength;
				
			o.Alpha = finalAlpha;			
		}
		ENDCG
	} 
	FallBack "VertexLit"
}
