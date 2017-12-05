Shader "Heathen/Essentials/Occlusion-Wireframe/Basic/Occluded Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
	}
 
    SubShader 
	{
		Tags { "Queue" = "AlphaTest-1" "RenderType" = "Opaque" }

		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/FORWARD"     
		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/PREPASS"
		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/DEFERRED"    

		ZWrite On
		ZTest LEqual
		Blend Off

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG 
	}
 
 	Fallback "Diffuse", 0
}
