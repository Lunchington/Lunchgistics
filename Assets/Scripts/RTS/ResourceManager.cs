using UnityEngine;
using System.Collections;

namespace RTS {

	public static class ResourceManager  {
		public static float ScrollSpeed { get { return 2.5f; } }
		public static int ScrollWidth { get { return 15; } }

		public static float zLevelDefault { get { return -15f; } }

		public static int ZoomMin { get { return 1; } }
		public static int ZoomMax { get { return 5; } }
	}
}
