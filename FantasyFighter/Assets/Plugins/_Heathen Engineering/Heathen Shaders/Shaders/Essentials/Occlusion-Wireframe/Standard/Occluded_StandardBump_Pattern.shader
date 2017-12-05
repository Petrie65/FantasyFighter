Shader "Heathen/Essentials/Occlusion-Wireframe/Pattern/Standard Bump" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_BumpScale("Scale", Float) = 1.0
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
		_OcclusionPattern ("Pattern (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/FORWARD"   
		//UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/PREPASS"
		UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/DEFERRED"
		
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _BumpScale;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			fixed3 tempNorm = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			tempNorm.z = tempNorm.z/_BumpScale;
			o.Normal = normalize(tempNorm);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
