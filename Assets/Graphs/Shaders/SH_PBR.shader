// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SH_PBR"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Base_color("Base_color", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_RMAo("RMAo", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Base_color;
		uniform float4 _Base_color_ST;
		uniform sampler2D _RMAo;
		uniform float4 _RMAo_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = clamp( ( UnpackNormal( tex2D( _Normal,uv_Normal) ) * float3( 2.5,2.5,2.5 ) ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
			float2 uv_Base_color = i.uv_texcoord * _Base_color_ST.xy + _Base_color_ST.zw;
			float4 tex2DNode1 = tex2D( _Base_color,uv_Base_color);
			o.Albedo = tex2DNode1.rgb;
			o.Emission = ( tex2DNode1 * float4( 0.2132353,0.2132353,0.2132353,0 ) ).rgb;
			float2 uv_RMAo = i.uv_texcoord * _RMAo_ST.xy + _RMAo_ST.zw;
			float4 tex2DNode4 = tex2D( _RMAo,uv_RMAo);
			o.Metallic = tex2DNode4.g;
			o.Smoothness = clamp( ( ( 1.0 - tex2DNode4.r ) + 0.0 ) , 0.0 , 1.0 );
			o.Occlusion = tex2DNode4.b;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
712;721;1531;746;671.5015;133.3;1;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-540.5,295;Float;True;Property;_RMAo;RMAo;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;5;-533.5,97;Float;True;Property;_Normal;Normal;1;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;6;-175.4999,431.6002;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-65.20001,169.9999;Float;False;0;FLOAT3;0.0;False;1;FLOAT3;2.5,2.5,2.5;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;7;15.10001,426.2001;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-533.5,-99;Float;True;Property;_Base_color;Base_color;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;11;181.8002,353.2;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;10;134.9,139.1999;Float;False;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;70.49854,-31.30003;Float;False;0;COLOR;0.0;False;1;COLOR;0.2132353,0.2132353,0.2132353,0;False;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;458,108;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;Custom/SH_PBR;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;6;0;4;1
WireConnection;8;0;5;0
WireConnection;7;0;6;0
WireConnection;11;0;7;0
WireConnection;10;0;8;0
WireConnection;12;0;1;0
WireConnection;0;0;1;0
WireConnection;0;1;10;0
WireConnection;0;2;12;0
WireConnection;0;3;4;2
WireConnection;0;4;11;0
WireConnection;0;5;4;3
ASEEND*/
//CHKSM=71A81C0B973234D471C6CA8481DB9BB56A377539