using UnityEngine;
using System.Collections;

namespace RTS {

	public static class ResourceManager  {
		public static float ScrollSpeed { get { return 25f; } }
		public static int ScrollWidth { get { return 15; } }

		public static int ZoomMin { get { return 4; } }
		public static int ZoomMax { get { return 10; } }
	}
}
