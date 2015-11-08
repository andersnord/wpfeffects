using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ANEffectsLibrary
{
    public class SimplexNoise : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(SimplexNoise), 0);
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(double), typeof(SimplexNoise), new UIPropertyMetadata(((double)(10D)), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty IntensityProperty = DependencyProperty.Register("Intensity", typeof(double), typeof(SimplexNoise), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty TimerProperty = DependencyProperty.Register("Timer", typeof(double), typeof(SimplexNoise), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty AnimationspeedProperty = DependencyProperty.Register("Animationspeed", typeof(double), typeof(SimplexNoise), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(3)));
        public SimplexNoise()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = Global.MakePackUri("Effects/Shaders/SimplexNoise.ps");
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(SizeProperty);
            this.UpdateShaderValue(IntensityProperty);
            this.UpdateShaderValue(TimerProperty);
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
        public double Size
        {
            get
            {
                return ((double)(this.GetValue(SizeProperty)));
            }
            set
            {
                this.SetValue(SizeProperty, value);
            }
        }
        public double Intensity
        {
            get
            {
                return ((double)(this.GetValue(IntensityProperty)));
            }
            set
            {
                this.SetValue(IntensityProperty, value);
            }
        }
        public double Timer
        {
            get
            {
                return ((double)(this.GetValue(TimerProperty)));
            }
            set
            {
                this.SetValue(TimerProperty, value);
            }
        }
        public double Animationspeed
        {
            get
            {
                return ((double)(this.GetValue(AnimationspeedProperty)));
            }
            set
            {
                this.SetValue(AnimationspeedProperty, value);
            }
        }
    }


}

