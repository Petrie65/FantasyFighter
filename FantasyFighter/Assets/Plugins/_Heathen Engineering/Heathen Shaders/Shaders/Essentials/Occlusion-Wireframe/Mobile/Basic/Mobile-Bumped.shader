// Simplified Bumped shader. Differences from regular Bumped one:
// - no Main Color
// - Normalmap uses Tiling/Offset of the Base texture
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Heathen/Mobile/Occlusion-Wireframe/Basic/Bumped Diffuse" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
}

SubShader {
	Tags {"Queue" = "AlphaTest-1" "RenderType"="Opaque" }
	LOD 250

	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/FORWARD" 
	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/PREPASS"
	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/DEFERRED"

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;
sampler2D _BumpMap;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG  
}

FallBack "Mobile/Diffuse"
}
