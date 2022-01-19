using UnityEngine;
using FunkyCode.LightingSettings;

namespace FunkyCode.Rendering.Manager
{
    public static class Main
	{
        public static void InternalUpdate()
        {
			UpdateCameras();

			CameraTransform.Update();
			
            UpdateMaterials();

			UpdateMainBuffers();
        }

        public static void Render()
		{
			if (Lighting2D.disable)
			{
				return;
			}

            LightingCameras cameras = LightingManager2D.Get().cameras;

			if (cameras.Length < 1)
			{
				return;
			}

			UpdateLoop();
			
			if (LightBuffer2D.List.Count > 0)
			{
				foreach(LightBuffer2D buffer in LightBuffer2D.List)
				{
					buffer.Render();
				}
			}
			
			if (LightMainBuffer2D.List.Count > 0)
			{
				foreach(LightMainBuffer2D buffer in LightMainBuffer2D.List)
				{
					buffer.Render();
				}
			}
		}

        private static void UpdateLoop()
		{
			// colliders

			if (DayLightCollider2D.List.Count > 0)
			{
				for(int id = 0; id < DayLightCollider2D.List.Count; id++)
				{
					DayLightCollider2D.List[id].UpdateLoop();
				}
			}
			
			if (LightCollider2D.List.Count > 0)
			{
				for(int id = 0; id < LightCollider2D.List.Count; id++)
				{
					LightCollider2D.List[id].UpdateLoop();
				}
			}

			// lights

			if (LightSprite2D.List.Count > 0)
			{
				for(int id = 0; id < LightSprite2D.List.Count; id++)
				{
					LightSprite2D.List[id].UpdateLoop();
				}
			}
			
			if (Light2D.List.Count > 0)
			{
				for(int id = 0; id < Light2D.List.Count; id++)
				{
					Light2D.List[id].UpdateLoop();
				}
			}

			// mesh renderers

			if (OnRenderMode.List.Count > 0)
			{
				for(int id = 0; id < OnRenderMode.List.Count; id++)
				{
					OnRenderMode.List[id].UpdateLoop();
				}
			}
		}

        public static void UpdateCameras()
		{
			// should reset materials
			
			LightmapMaterials.ResetShaders();

			LightmapMaterials.SetDayLight();

            LightingCameras cameras = LightingManager2D.Get().cameras;

			for(int i = 0; i < cameras.Length; i++)
			{
				CameraSettings cameraSetting = cameras.Get(i);

				for(int b = 0; b < cameraSetting.Lightmaps.Length; b++)
				{
					CameraLightmap cameraLightmap = cameraSetting.GetLightmap(b);

					if (cameraLightmap.presetId >= Lighting2D.LightmapPresets.Length)
					{
						continue;
					}

					LightmapPreset lightmapPreset = Lighting2D.LightmapPresets[cameraLightmap.presetId];
	
					LightMainBuffer2D buffer = LightMainBuffer2D.Get(false, cameraSetting, cameraLightmap, lightmapPreset);

					if (buffer != null)
					{
						Camera camera = cameraSetting.GetCamera();
						CameraTransform.GetCamera(camera);

						PassLightmap(false, buffer, cameraSetting, cameraLightmap, lightmapPreset);
					}
	
					if (cameraLightmap.sceneView == CameraLightmap.SceneView.Enabled)
					{
						CameraSettings sceneSettings = cameraSetting;
						sceneSettings.cameraType = CameraSettings.CameraType.SceneView;

						buffer = LightMainBuffer2D.Get(true, sceneSettings, cameraLightmap, lightmapPreset);

						if (buffer != null)
						{
							Camera camera = sceneSettings.GetCamera();
							CameraTransform.GetCamera(camera);
						
							PassLightmap(true, buffer, sceneSettings, cameraLightmap, lightmapPreset);
						}
					}
				}
			}
		}

		public static void PassLightmap(bool isSceneView, LightMainBuffer2D buffer, CameraSettings cameraSetting, CameraLightmap cameraLightmap, LightmapPreset lightmapPreset)
		{
			// bool isSceneView = false; // bool isSceneView = cameraSetting.cameraType == CameraSettings.CameraType.SceneView;

			buffer.cameraLightmap.rendering = cameraLightmap.rendering;

			buffer.cameraLightmap.overlay = cameraLightmap.overlay;

			buffer.cameraLightmap.renderLayerId = cameraLightmap.renderLayerId;

			if (buffer.cameraLightmap.customMaterial != cameraLightmap.customMaterial)
			{
				buffer.cameraLightmap.customMaterial = cameraLightmap.customMaterial;

				buffer.ClearMaterial();
			}

			if (buffer.cameraLightmap.overlayMaterial != cameraLightmap.overlayMaterial)
			{
				buffer.cameraLightmap.overlayMaterial = cameraLightmap.overlayMaterial;

				buffer.ClearMaterial();
			}

			if (cameraLightmap.rendering == CameraLightmap.Rendering.Disabled)
			{
				return;
			}

			Camera camera = cameraSetting.GetCamera();

			switch(cameraLightmap.output)
			{
				case CameraLightmap.Output.Materials:

					foreach(Material material in cameraLightmap.GetMaterials().materials)
					{
						if (material == null)
						{
							continue;
						}

						// adding up // Get Free LightMap

						// Get Free ID from material

						// + is scene view

						// incremental ID
					
						LightmapMaterials.SetMaterial(isSceneView, 1, material, camera, buffer.renderTexture, lightmapPreset);			
					}

				break;

				case CameraLightmap.Output.Shaders:

					// incremental ID

					LightmapMaterials.SetShaders(isSceneView, 1, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass1:

					LightmapMaterials.SetShaders(isSceneView, 1, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass2:

					LightmapMaterials.SetShaders(isSceneView, 2, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass3:

					LightmapMaterials.SetShaders(isSceneView, 3, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass4:

					LightmapMaterials.SetShaders(isSceneView, 4, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass5:

					LightmapMaterials.SetShaders(isSceneView, 5, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass6:

					LightmapMaterials.SetShaders(isSceneView, 6, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass7:

					LightmapMaterials.SetShaders(isSceneView, 7, camera, buffer.renderTexture, lightmapPreset);

				break;

				case CameraLightmap.Output.Pass8:

					LightmapMaterials.SetShaders(isSceneView, 8, camera, buffer.renderTexture, lightmapPreset);

				break;
			}
		}
		
		public static void UpdateMaterials()
		{
			if (Lighting2D.materials.Initialize())
			{
				LightMainBuffer2D.Clear();
				LightBuffer2D.Clear();

				Light2D.ForceUpdateAll();
			}
		}

		public static void UpdateMainBuffers()
		{
			if (LightMainBuffer2D.List.Count <= 0)
			{
				return;
			}

			for(int i = 0; i < LightMainBuffer2D.List.Count; i++)
			{
				LightMainBuffer2D.List[i]?.Update();
			}

			for(int i = 0; i < LightMainBuffer2D.List.Count; i++)
			{
				LightMainBuffer2D buffer = LightMainBuffer2D.List[i];

				if (Lighting2D.disable)
				{
					buffer.updateNeeded = false;	

					continue;
				}

				CameraSettings cameraSettings = buffer.cameraSettings;
				CameraLightmap cameraLightmap = buffer.cameraLightmap;
				
				bool render = cameraLightmap.rendering != CameraLightmap.Rendering.Disabled;
			
				buffer.updateNeeded = (render && cameraSettings.GetCamera() != null);
			}
		}
    }
}