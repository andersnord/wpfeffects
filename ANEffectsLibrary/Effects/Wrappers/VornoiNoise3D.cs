using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ANEffectsLibrary
{
    public class VoronoiNoise3D : ShaderEffect
    {
		public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(VoronoiNoise3D), 0);
		public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(double), typeof(VoronoiNoise3D), new UIPropertyMetadata(((double)(10D)), PixelShaderConstantCallback(0)));
		public static readonly DependencyProperty IntensityProperty = DependencyProperty.Register("Intensity", typeof(double), typeof(VoronoiNoise3D), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(1)));
		public static readonly DependencyProperty TimerProperty = DependencyProperty.Register("Timer", typeof(double), typeof(VoronoiNoise3D), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(2)));
		public static readonly DependencyProperty VoronoiversionProperty = DependencyProperty.Register("Voronoiversion", typeof(double), typeof(VoronoiNoise3D), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty AnimationspeedProperty = DependencyProperty.Register("Animationspeed", typeof(double), typeof(VoronoiNoise3D), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(4)));
        public VoronoiNoise3D()
        {
			PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = Global.MakePackUri("Effects/Shaders/VoronoiNoise3D.ps");
			this.PixelShader = pixelShader;

			this.UpdateShaderValue(InputProperty);
			this.UpdateShaderValue(ZoomProperty);
			this.UpdateShaderValue(IntensityProperty);
			this.UpdateShaderValue(TimerProperty);
			this.UpdateShaderValue(VoronoiversionProperty);
		}
		public Brush Input {
			get {
				return ((Brush)(this.GetValue(InputProperty)));
			}
			set {
				this.SetValue(InputProperty, value);
			}
		}
		public double Zoom {
			get {
				return ((double)(this.GetValue(ZoomProperty)));
			}
			set {
				this.SetValue(ZoomProperty, value);
			}
		}
		public double Intensity {
			get {
				return ((double)(this.GetValue(IntensityProperty)));
			}
			set {
				this.SetValue(IntensityProperty, value);
			}
		}
		public double Timer {
			get {
				return ((double)(this.GetValue(TimerProperty)));
			}
			set {
				this.SetValue(TimerProperty, value);
			}
		}
		public double Voronoiversion {
			get {
				return ((double)(this.GetValue(VoronoiversionProperty)));
			}
			set {
				this.SetValue(VoronoiversionProperty, value);
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
