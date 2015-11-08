float opacity : register(C0);
float timer : register(C1);
sampler2D implicitInputSampler : register(S0);

//
// Description : Array and textureless GLSL 2D/3D/4D simplex
// noise functions.
// Author : Ian McEwan, Ashima Arts.
// Maintainer : ijm
// Lastfmod : 20110822 (ijm)
// License : Copyright (C) 2011 Ashima Arts. All rights reserved.
// Distributed under the MIT License.
// https://github.com/ashima/webgl-noise
//

const float3 fmod289(float3 x) {
	return x - floor(x * (1.0 / 289.0)) * 289.0;
}

const float4 fmod289(float4 x) {
	return x - floor(x * (1.0 / 289.0)) * 289.0;
}

const float3 permute(float3 x) {
	return fmod289(((x*34.0) + 1.0)*x);
}

const float4 taylorInvSqrt(float4 r)
{
	return 1.79284291400159 - 0.85373472095314 * r;
}


// ' threshold ' is constant , ' value ' is smoothly varying
const float aastep(float threshold, float value) {
	float2 lengthVar = float2(ddx(value), ddy(value));
	float temp = length(lengthVar);
	float afwidth = 0.7 * temp;
	// GLSL ' s fwidth ( value ) is abs ( dFdx ( value ) ) + abs ( dFdy ( value ) )
	return smoothstep(threshold - afwidth, threshold + afwidth, value);
}


// Cellular noise, returning F1 and F2 in a vec2.
// 3x3x3 search region for good F2 everywhere, but a lot
// slower than the 2x2x2 version.
// The code below is a bit scary even to its author,
// but it has at least half decent performance on a
// fmodern GPU. In any case, it beats any software
// implementation of Worley noise hands down.

float2 hash2( float2 p )
{
	// texture based white noise
	//return texture2D( iChannel0, (p+0.5)/256.0, -100.0 ).xy;
	
    // procedural white noise	
	return frac(sin(float2(dot(p,float2(127.1,311.7)),dot(p,float2(269.5,183.3))))*43758.5453);
}

float3 voronoi(in float2 x )
{
    float2 n = floor(x);
    float2 f = frac(x);

    //----------------------------------
    // first pass: regular voronoi
    //----------------------------------
	float2 mg, mr;

    float md = 8.0;
    for( int j=-1; j<=1; j++ )
    for( int i=-1; i<=1; i++ )
    {
        float2 g = float2(float(i),float(j));
		float2 o = hash2( n + g );
		#ifdef ANIMATE
        o = 0.5 + 0.5*sin( timer + 6.2831*o );
        #endif	
        float2 r = g + o - f;
        float d = dot(r,r);

        if( d<md )
        {
            md = d;
            mr = r;
            mg = g;
        }
    }

    //----------------------------------
    // second pass: distance to borders
    //----------------------------------
    md = 8.0;
    for( int j=-2; j<=2; j++ )
    for( int i=-2; i<=2; i++ )
    {
        float2 g = mg + float2(float(i),float(j));
		float2 o = hash2( n + g );
		#ifdef ANIMATE
        o = 0.5 + 0.5*sin( timer+ 6.2831*o );
        #endif	
        float2 r = g + o - f;

        if( dot(mr-r,mr-r)>0.00001 )
        md = min( md, dot( 0.5*(mr+r), normalize(r-mr) ) );
    }

    return float3( md, mr );
}



float4 main(float2 uv : TEXCOORD) : COLOR
{
	  //float2 F = cellular(float3(uv.x * 10.0, uv.y * 10.0, timer));
   // float n = (F.y - F.x);
    //return float4(n, n, n, 1.0);
    
    float2 F = cellular(float3(uv.x * 0.1, uv.y * opacity, timer));
    float rings = 1.0 - aastep(0.45, F.x ) + aastep(0.55, F.x);
   return float4(0.0 , 0.0, rings, 1.0);
}


