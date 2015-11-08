float circlesize : register(C0);
float zoom : register(C1);
sampler2D implicitInputSampler : register(S0);

//
// Description : Array and textureless GLSL 2D/3D/4D simplex
// noise functions.
// Author : Ian McEwan, Ashima Arts.
// Maintainer : ijm
// Lastmod : 20110822 (ijm)
// License : Copyright (C) 2011 Ashima Arts. All rights reserved.
// Distributed under the MIT License.
// https://github.com/ashima/webgl-noise
//

const float3 mod289(float3 x) {
	return x - floor(x * (1.0 / 289.0)) * 289.0;
}

const float4 mod289(float4 x) {
	return x - floor(x * (1.0 / 289.0)) * 289.0;
}

const float4 permute(float4 x) {
	return mod289(((x*34.0) + 1.0)*x);
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


// Cellular noise (" Worley noise ") in 2 D in GLSL , simplified version .
// Copyright ( c ) Stefan Gustavson 2011-04-19. All rights reserved .
// This code is released under the conditions of the MIT license .
// See LICENSE file for details .

const float2 cellular2x2(float2 P) {
	const float K = 1.0 / 7.0;
	const float K2 = 0.5 / 7.0;
	const float jitter = 0.8; // jitter 1.0 makes F1 wrong more often
	float2 Pi = fmod(floor(P), 289.0);
	float2 Pf = frac(P);
	float4 Pfx = Pf.x + float4(-0.5, -1.5, -0.5, -1.5);
	float4 Pfy = Pf.y + float4(-0.5, -0.5, -1.5, -1.5);
	float4 p = permute(Pi.x + float4(0.0, 1.0, 0.0, 1.0));
	p = permute(p + Pi.y + float4(0.0, 0.0, 1.0, 1.0));
	float4 ox = fmod(p, 7.0)*K + K2;
	float4 oy = fmod(floor(p*K), 7.0)*K + K2;
	float4 dx = Pfx + jitter*ox;
	float4 dy = Pfy + jitter*oy;
	float4 d = dx * dx + dy * dy; // distances squared
	// Cheat and pick only F1 for the return value
	d.xy = min(d.xy, d.zw);
	d.x = min(d.x, d.y);
	return d.xx; // F1 duplicated , F2 not computed
}



float4 main(float2 uv : TEXCOORD) : COLOR
{
	float2 F = cellular2x2(uv * zoom);
	float rings = 1.0 - aastep(0.45, F.x  * circlesize) + aastep(0.55, F.x * circlesize);
	float4 cellularValue = float4(rings, rings, rings, 1.0);

   // Return new color
	return cellularValue;
}


