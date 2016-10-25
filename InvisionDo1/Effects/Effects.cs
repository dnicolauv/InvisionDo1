using System;
using Xamarin.Forms;
             
namespace InvisionDo1.Effects
{
	//Removes borders of the entry control on iOS
	public class EntryEffect : RoutingEffect
	{
		public EntryEffect() : base("dnvXamarin.EntryEffect")
		{
		}
	}

	public class CircleEffect : RoutingEffect
	{
		public CircleEffect() : base("dnvXamarin.CircleEffect")
		{
		}
	}

	public class CornerRadiusEffect : RoutingEffect
	{
		public float CornerRadius { get; set; }

		public CornerRadiusEffect() : base("dnvXamarin.CornerRadiusEffect")
		{
		}
	}

	public class ShadowEffect : RoutingEffect
	{
		public Color ShadowColor { get; set; }

		public float ShadowSize { get; set; }

		public ShadowEffect() : base("dnvXamarin.ShadowEffect")
		{
		}
	}

}

