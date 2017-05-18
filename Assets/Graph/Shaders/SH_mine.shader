// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SH_mine"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color0 = float4(1,0,0,0);
			float temp_output_26_0 = ( _Time.y * _Speed );
			float cos53 = cos( temp_output_26_0 );
			float sin53 = sin( temp_output_26_0 );
			float2 rotator53 = mul(i.texcoord_0 - float2( 0.5,0.5 ), float2x2(cos53,-sin53,sin53,cos53)) + float2( 0.5,0.5 );
			float4 tex2DNode51 = tex2D( _TextureSample0,rotator53);
			float4 temp_cast_0 = (0).xxxx;
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float temp_output_23_0 = sin( temp_output_26_0 );
			o.Emission = lerp( lerp( lerp( _Color0 , float4( 0,0,0,0 ) , tex2DNode51.r ) , temp_cast_0 , tex2D( _TextureSample1,uv_TextureSample1).b ) , ( (0.0 + (temp_output_23_0 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) * _Color0 ) , tex2DNode51.g ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
788;334;1531;746;954.4159;128.7854;2.025807;True;True
Node;AmplifyShaderEditor.RangedFloatNode;27;-471.119,-71.94938;Float;False;Property;_Speed;Speed;3;0;5;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;22;-491.9191,-163.1495;Float;False;0;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;56;-616.9222,382.5564;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-251.9191,-105.5493;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RotatorNode;53;-357.4223,382.5567;Float;False;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;51;-39.82236,538.7559;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Assets/Graph/Textures/Gameplay/Mine_mask.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SinOpNode;23;-67.5199,-104.9503;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ColorNode;50;213.5738,87.94245;Float;False;Constant;_Color0;Color 0;1;0;1,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;59;-36.7255,784.8538;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Assets/Graph/Textures/Gameplay/Mine_mask.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;57;455.6967,323.205;Float;False;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.TFHCRemap;25;145.2798,-143.3505;Float;True;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;58;516.1458,581.576;Float;True;0;COLOR;0.0;False;1;FLOAT;0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;495.6796,3.849669;Float;False;0;FLOAT;0.0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.LerpOp;52;726.1776,311.8562;Float;True;0;COLOR;0,0;False;1;COLOR;1.0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;140.4767,-232.2485;Float;False;Time;-1;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1216.3,13.6;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;Custom/SH_mine;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;26;0;22;0
WireConnection;26;1;27;0
WireConnection;53;0;56;0
WireConnection;53;2;26;0
WireConnection;51;1;53;0
WireConnection;23;0;26;0
WireConnection;57;0;50;0
WireConnection;57;2;51;1
WireConnection;25;0;23;0
WireConnection;58;0;57;0
WireConnection;58;2;59;3
WireConnection;28;0;25;0
WireConnection;28;1;50;0
WireConnection;52;0;58;0
WireConnection;52;1;28;0
WireConnection;52;2;51;2
WireConnection;39;0;23;0
WireConnection;0;2;52;0
ASEEND*/
//CHKSM=DE232211AF90D00A7A9140F1C78E4EFB9E17F36E