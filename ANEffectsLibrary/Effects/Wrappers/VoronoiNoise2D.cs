using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ANEffectsLibrary
{
    public class VoronoiNoise2D : ShaderEffect
        {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(VoronoiNoise2D), 0);
        public static readonly DependencyProperty CirclesizeProperty = DependencyProperty.Register("Circlesize", typeof(double), typeof(VoronoiNoise2D), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(double), typeof(VoronoiNoise2D), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));
            public VoronoiNoise2D()
            {
                PixelShader pixelShader = new PixelShader();
                pixelShader.UriSource = Global.MakePackUri("Effects/Shaders/VoronoiNoise2D.ps");
                this.PixelShader = pixelShader;

                this.UpdateShaderValue(InputProperty);
                this.UpdateShaderValue(CirclesizeProperty);
                this.UpdateShaderValue(ZoomProperty);
            }
            public Brush Input
            {
                get
                {
                    return ((Brush)(this.GetValue(InputProperty)));
                }
                set
                {
                    this.SetValue(InputProperty, value);
                }
            }
            public double Circlesize
            {
                get
                {
                    return ((double)(this.GetValue(CirclesizeProperty)));
                }
                set
                {
                    this.SetValue(CirclesizeProperty, value);
                }
            }
            public double Zoom
            {
                get
                {
                    return ((double)(this.GetValue(ZoomProperty)));
                }
                set
                {
                    this.SetValue(ZoomProperty, value);
                }
            }
    }
}
