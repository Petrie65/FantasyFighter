// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Heathen/Mobile/Occlusion-Wireframe/Pattern/Diffuse" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
		_OcclusionPattern ("Pattern (A)", 2D) = "white" {}
}
SubShader {
	Tags {"Queue" = "AlphaTest-1" "RenderType"="Opaque" }
	LOD 150

	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/FORWARD"
	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/PREPASS"
	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/DEFERRED"

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
