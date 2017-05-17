// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SH_test"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.04
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 1)) = 0.6
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ "GrabScreen0" }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D GrabScreen0;
		uniform float _Float0;
		uniform float _MaskClipValue = 0.04;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color1 = float4(0,0,0,0);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode12 = tex2D( _TextureSample0,uv_TextureSample0);
			float temp_output_27_0 = clamp( ( pow( tex2DNode12.r , 2.0 ) * 3.0 ) , 0.0 , 1.0 );
			o.Albedo = lerp( _Color1 , float4(0.03067948,0.8897059,0,0) , temp_output_27_0 ).rgb;
			o.Emission = lerp( ( ( tex2Dproj( GrabScreen0, UNITY_PROJ_COORD( i.screenPos ) ) * unity_FogColor ) * _Float0 ) , _Color1 , temp_output_27_0 ).rgb;
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			o.Alpha = 1;
			clip( tex2DNode12.r - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
11;651;1531;746;1164.183;552.3644;1.732552;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;7;-963.0602,-71.52788;Float;False;1;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;12;-469.5089,305.2081;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ScreenColorNode;11;-789.0602,-44.52787;Float;False;Global;GrabScreen0;Grab Screen 0;0;0;Object;-1;0;FLOAT4;0,0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.FogAndAmbientColorsNode;19;-883.1597,-169.1278;Float;False;unity_FogColor;COLOR
Node;AmplifyShaderEditor.PowerNode;26;-399.8404,77.38653;Float;False;0;FLOAT;0.0;False;1;FLOAT;2.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;15;-1006.36,-249.4278;Float;False;Property;_Float0;Float 0;1;0;0.6;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-243.2016,77.88944;Float;False;0;FLOAT;0.0;False;1;FLOAT;3.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-520.66,-85.82786;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.ColorNode;24;-417.8579,-332.9783;Float;False;Constant;_Color0;Color 0;2;0;0.03067948,0.8897059,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;30;-419.1859,-495.1898;Float;False;Constant;_Color1;Color 1;2;0;0,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;27;47.81947,-1.502618;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-356.0598,-82.62784;Float;False;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.LerpOp;23;38.24242,-344.6424;Float;False;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.LerpOp;29;38.20752,-224.5861;Float;False;0;COLOR;0.0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;17;-95.09966,184.6002;Float;False;Constant;_Float2;Float 2;2;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;16;-102.6997,97.2;Float;False;Constant;_Float1;Float 1;2;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;282.0999,-1.5;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;SH_test;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Masked;0.04;True;True;0;False;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;11;0;7;0
WireConnection;26;0;12;1
WireConnection;28;0;26;0
WireConnection;14;0;11;0
WireConnection;14;1;19;0
WireConnection;27;0;28;0
WireConnection;22;0;14;0
WireConnection;22;1;15;0
WireConnection;23;0;30;0
WireConnection;23;1;24;0
WireConnection;23;2;27;0
WireConnection;29;0;22;0
WireConnection;29;1;30;0
WireConnection;29;2;27;0
WireConnection;0;0;23;0
WireConnection;0;2;29;0
WireConnection;0;3;16;0
WireConnection;0;4;17;0
WireConnection;0;10;12;1
ASEEND*/
//CHKSM=CB56E0EB5B06029EE0178666A823C4BCB63F72A9