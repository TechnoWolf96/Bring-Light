using UnityEngine;
using FunkyCode.LightingSettings;

namespace FunkyCode
{
	[System.Serializable]
	public class LightmapMaterials
	{
		public Material[] materials = new Material[1];

		public static void ClearMaterial(Material material)
		{
			Vector4 zero = new Vector4(0, 0, 0, 0);

			// game
			material.SetTexture("_GameTexture1", null);
			material.SetVector("_GameRect1", zero);
			material.SetVector("_GameColor1", zero);
			material.SetFloat("_GameRotation1", 0);

			material.SetTexture("_GameTexture2", null);
			material.SetVector("_GameRect2", zero);
			material.SetVector("_GameColor2", zero);
			material.SetFloat("_GameRotation2", 0);

			material.SetTexture("_GameTexture3", null);
			material.SetVector("_GameRect3", zero);
			material.SetVector("_GameColor3", zero);
			material.SetFloat("_GameRotation3", 0);

			material.SetTexture("_GameTexture4", null);
			material.SetVector("_GameRect4", zero);
			material.SetVector("_GameColor4", zero);
			material.SetFloat("_GameRotation4", 0);

			material.SetTexture("_GameTexture5", null);
			material.SetVector("_GameRect5", zero);
			material.SetVector("_GameColor5", zero);
			material.SetFloat("_GameRotation5", 0);

			material.SetTexture("_GameTexture6", null);
			material.SetVector("_GameRect6", zero);
			material.SetVector("_GameColor6", zero);
			material.SetFloat("_GameRotation6", 0);

			material.SetTexture("_GameTexture7", null);
			material.SetVector("_GameRect7", zero);
			material.SetVector("_GameColor7", zero);
			material.SetFloat("_GameRotation7", 0);

			material.SetTexture("_GameTexture8", null);
			material.SetVector("_GameRect8", zero);
			material.SetVector("_GameColor8", zero);
			material.SetFloat("_GameRotation8", 0);


			// scene
			material.SetTexture("_SceneTexture1", null);
			material.SetVector("_SceneRect1", zero);
			material.SetVector("_SceneColor1", zero);
			material.SetFloat("_SceneRotation1", 0);

			material.SetTexture("_SceneTexture2", null);
			material.SetVector("_SceneRect2", zero);
			material.SetVector("_SceneColor2", zero);
			material.SetFloat("_SceneRotation2", 0);

			material.SetTexture("_SceneTexture3", null);
			material.SetVector("_SceneRect3", zero);
			material.SetVector("_SceneColor3", zero);
			material.SetFloat("_SceneRotation3", 0);

			material.SetTexture("_SceneTexture4", null);
			material.SetVector("_SceneRect4", zero);
			material.SetVector("_SceneColor4", zero);
			material.SetFloat("_SceneRotation4", 0);

			material.SetTexture("_SceneTexture5", null);
			material.SetVector("_SceneRect5", zero);
			material.SetVector("_SceneColor5", zero);
			material.SetFloat("_SceneRotation5", 0);

			material.SetTexture("_SceneTexture6", null);
			material.SetVector("_SceneRect6", zero);
			material.SetVector("_SceneColor6", zero);
			material.SetFloat("_SceneRotation6", 0);

			material.SetTexture("_SceneTexture7", null);
			material.SetVector("_SceneRect7", zero);
			material.SetVector("_SceneColor7", zero);
			material.SetFloat("_SceneRotation7", 0);

			material.SetTexture("_SceneTexture8", null);
			material.SetVector("_SceneRect8", zero);
			material.SetVector("_SceneColor8", zero);
			material.SetFloat("_SceneRotation8", 0);
		}

		public static void ResetShaders()
		{
			Vector4 zero = new Vector4(0, 0, 0, 0);

			// game
			Shader.SetGlobalTexture("_GameTexture1", null);
			Shader.SetGlobalVector("_GameRect1", zero);
			Shader.SetGlobalVector("_GameColor1", zero);
			Shader.SetGlobalFloat("_GameRotation1", 0);

			Shader.SetGlobalTexture("_GameTexture2", null);
			Shader.SetGlobalVector("_GameRect2", zero);
			Shader.SetGlobalVector("_GameColor2", zero);
			Shader.SetGlobalFloat("_GameRotation2", 0);

			Shader.SetGlobalTexture("_GameTexture3", null);
			Shader.SetGlobalVector("_GameRect3", zero);
			Shader.SetGlobalVector("_GameColor3", zero);
			Shader.SetGlobalFloat("_GameRotation3", 0);

			Shader.SetGlobalTexture("_GameTexture4", null);
			Shader.SetGlobalVector("_GameRect4", zero);
			Shader.SetGlobalVector("_GameColor4", zero);
			Shader.SetGlobalFloat("_GameRotation4", 0);

			Shader.SetGlobalTexture("_GameTexture5", null);
			Shader.SetGlobalVector("_GameRect5", zero);
			Shader.SetGlobalVector("_GameColor5", zero);
			Shader.SetGlobalFloat("_GameRotation5", 0);

			Shader.SetGlobalTexture("_GameTexture6", null);
			Shader.SetGlobalVector("_GameRect6", zero);
			Shader.SetGlobalVector("_GameColor6", zero);
			Shader.SetGlobalFloat("_GameRotation6", 0);

			Shader.SetGlobalTexture("_GameTexture7", null);
			Shader.SetGlobalVector("_GameRect7", zero);
			Shader.SetGlobalVector("_GameColor7", zero);
			Shader.SetGlobalFloat("_GameRotation7", 0);

			Shader.SetGlobalTexture("_GameTexture8", null);
			Shader.SetGlobalVector("_GameRect8", zero);
			Shader.SetGlobalVector("_GameColor8", zero);
			Shader.SetGlobalFloat("_GameRotation8", 0);


			// scene
			Shader.SetGlobalTexture("_SceneTexture1", null);
			Shader.SetGlobalVector("_SceneRect1", zero);
			Shader.SetGlobalVector("_SceneColor1", zero);
			Shader.SetGlobalFloat("_SceneRotation1", 0);

			Shader.SetGlobalTexture("_SceneTexture2", null);
			Shader.SetGlobalVector("_SceneRect2", zero);
			Shader.SetGlobalVector("_SceneColor2", zero);
			Shader.SetGlobalFloat("_SceneRotation2", 0);

			Shader.SetGlobalTexture("_SceneTexture3", null);
			Shader.SetGlobalVector("_SceneRect3", zero);
			Shader.SetGlobalVector("_SceneColor3", zero);
			Shader.SetGlobalFloat("_SceneRotation3", 0);

			Shader.SetGlobalTexture("_SceneTexture4", null);
			Shader.SetGlobalVector("_SceneRect4", zero);
			Shader.SetGlobalVector("_SceneColor4", zero);
			Shader.SetGlobalFloat("_SceneRotation4", 0);

			Shader.SetGlobalTexture("_SceneTexture5", null);
			Shader.SetGlobalVector("_SceneRect5", zero);
			Shader.SetGlobalVector("_SceneColor5", zero);
			Shader.SetGlobalFloat("_SceneRotation5", 0);

			Shader.SetGlobalTexture("_SceneTexture6", null);
			Shader.SetGlobalVector("_SceneRect6", zero);
			Shader.SetGlobalVector("_SceneColor6", zero);
			Shader.SetGlobalFloat("_SceneRotation6", 0);

			Shader.SetGlobalTexture("_SceneTexture7", null);
			Shader.SetGlobalVector("_SceneRect7", zero);
			Shader.SetGlobalVector("_SceneColor7", zero);
			Shader.SetGlobalFloat("_SceneRotation7", 0);

			Shader.SetGlobalTexture("_SceneTexture8", null);
			Shader.SetGlobalVector("_SceneRect8", zero);
			Shader.SetGlobalVector("_SceneColor8", zero);
			Shader.SetGlobalFloat("_SceneRotation8", 0);
		}

		public static Texture previewTexture;

		public static Texture GetPreviewTexture()
		{
			if (previewTexture == null) {
				previewTexture = Resources.Load<Texture>("Sprites/gfx_light2");
			}

			return(previewTexture);
		}

		public static void SetShaders(bool isSceneView, int id, Camera camera, LightTexture lightTexture, LightmapPreset lightmapPreset)
		{
			float ratio = (float)camera.pixelRect.width / camera.pixelRect.height;

			float x = camera.transform.position.x;
			float y = camera.transform.position.y;

			// z = width ; w = height
			float w = camera.orthographicSize * 2;
			float z = w * ratio;

			float rotation = camera.transform.eulerAngles.z * Mathf.Deg2Rad;

			Vector4 rect = new Vector4(x, y, z, w);

			Color c = lightmapPreset.darknessColor;

			Vector4 color = new Vector4(c.r, c.g, c.b, c.a);

			if (lightTexture == null)
			{
				Debug.Log("light texture null");
				return;
			}

			Texture texture = lightTexture.renderTexture;

			if (Lighting2D.ProjectSettings.shaderPreview == ShaderPreview.Enabled)
			{
				texture = GetPreviewTexture();
				color = Color.black;
				rect = new Vector4(0, 0, 1, 1);
			}

			bool gameView = !isSceneView;

			if (gameView)
			{
				switch(id)
				{
					case 1:
						Shader.SetGlobalTexture("_GameTexture1", texture);
						Shader.SetGlobalVector("_GameRect1", rect);
						Shader.SetGlobalVector("_GameColor1", color);
						Shader.SetGlobalFloat("_GameRotation1", rotation);
					break;

					case 2:
						Shader.SetGlobalTexture("_GameTexture2", texture);
						Shader.SetGlobalVector("_GameRect2", rect);
						Shader.SetGlobalVector("_GameColor2", color);
						Shader.SetGlobalFloat("_GameRotation2", rotation);
					break;

					case 3:
						Shader.SetGlobalTexture("_GameTexture3", texture);
						Shader.SetGlobalVector("_GameRect3", rect);
						Shader.SetGlobalVector("_GameColor3", color);
						Shader.SetGlobalFloat("_GameRotation3", rotation);
					break;

					case 4:
						Shader.SetGlobalTexture("_GameTexture4", texture);
						Shader.SetGlobalVector("_GameRect4", rect);
						Shader.SetGlobalVector("_GameColor4", color);
						Shader.SetGlobalFloat("_GameRotation4", rotation);
					break;

					case 5:
						Shader.SetGlobalTexture("_GameTexture5", texture);
						Shader.SetGlobalVector("_GameRect5", rect);
						Shader.SetGlobalVector("_GameColor5", color);
						Shader.SetGlobalFloat("_GameRotation5", rotation);
					break;

					case 6:
						Shader.SetGlobalTexture("_GameTexture6", texture);
						Shader.SetGlobalVector("_GameRect6", rect);
						Shader.SetGlobalVector("_GameColor6", color);
						Shader.SetGlobalFloat("_GameRotation6", rotation);
					break;

					case 7:
						Shader.SetGlobalTexture("_GameTexture7", texture);
						Shader.SetGlobalVector("_GameRect7", rect);
						Shader.SetGlobalVector("_GameColor7", color);
						Shader.SetGlobalFloat("_GameRotation7", rotation);
					break;

					case 8:
						Shader.SetGlobalTexture("_GameTexture8", texture);
						Shader.SetGlobalVector("_GameRect8", rect);
						Shader.SetGlobalVector("_GameColor8", color);
						Shader.SetGlobalFloat("_GameRotation8", rotation);
					break;
				}
			}
				else
			{
				switch(id)
				{
					case 1:
						Shader.SetGlobalTexture("_SceneTexture1", texture);
						Shader.SetGlobalVector("_SceneRect1", rect);
						Shader.SetGlobalVector("_SceneColor1", color);
						Shader.SetGlobalFloat("_SceneRotation1", rotation);
					break;

					case 2:
						Shader.SetGlobalTexture("_SceneTexture2", texture);
						Shader.SetGlobalVector("_SceneRect2", rect);
						Shader.SetGlobalVector("_SceneColor2", color);
						Shader.SetGlobalFloat("_SceneRotation2", rotation);
					break;

					case 3:
						Shader.SetGlobalTexture("_SceneTexture3", texture);
						Shader.SetGlobalVector("_SceneRect3", rect);
						Shader.SetGlobalVector("_SceneColor4", color);
						Shader.SetGlobalFloat("_SceneRotation3", rotation);
					break;

					case 4:
						Shader.SetGlobalTexture("_SceneTexture4", texture);
						Shader.SetGlobalVector("_SceneRect4", rect);
						Shader.SetGlobalVector("_SceneColor4", color);
						Shader.SetGlobalFloat("_SceneRotation4", rotation);
					break;

					case 5:
						Shader.SetGlobalTexture("_SceneTexture5", texture);
						Shader.SetGlobalVector("_SceneRect5", rect);
						Shader.SetGlobalVector("_SceneColor5", color);
						Shader.SetGlobalFloat("_SceneRotation5", rotation);
					break;

					case 6:
						Shader.SetGlobalTexture("_SceneTexture6", texture);
						Shader.SetGlobalVector("_SceneRect6", rect);
						Shader.SetGlobalVector("_SceneColor6", color);
						Shader.SetGlobalFloat("_SceneRotation6", rotation);
					break;

					case 7:
						Shader.SetGlobalTexture("_SceneTexture7", texture);
						Shader.SetGlobalVector("_SceneRect7", rect);
						Shader.SetGlobalVector("_SceneColor7", color);
						Shader.SetGlobalFloat("_SceneRotation7", rotation);
					break;

					case 8:
						Shader.SetGlobalTexture("_SceneTexture8", texture);
						Shader.SetGlobalVector("_SceneRect8", rect);
						Shader.SetGlobalVector("_SceneColor8", color);
						Shader.SetGlobalFloat("_SceneRotation8", rotation);
					break;
				}
			}
		}

		public static void SetMaterial(bool isSceneView, int id, Material material, Camera camera, LightTexture lightTexture, LightmapPreset lightmapPreset)
		{
			float ratio = (float)camera.pixelRect.width / camera.pixelRect.height;

			float x = camera.transform.position.x;
			float y = camera.transform.position.y;

			// z = size x ; w = size y
			float w = camera.orthographicSize * 2;
			float z = w * ratio;

			float rotation = camera.transform.eulerAngles.z * Mathf.Deg2Rad;

			Color c = lightmapPreset.darknessColor;

			Vector4 color = new Vector4(c.r, c.g, c.b, c.a);

			Vector4 rect = new Vector4(x, y, z, w);

			bool isGameView = !isSceneView;

			// scene?

			if (isGameView)
			{
				switch(id)
				{
					case 1:
						material.SetTexture("_GameTexture1", lightTexture.renderTexture);
						material.SetVector("_GameRect1", rect);
						material.SetVector("_GameColor1", color);
						material.SetFloat("_GameRotation1", rotation);
					break;

					case 2:
						material.SetTexture("_GameTexture2", lightTexture.renderTexture);
						material.SetVector("_GameRect2", rect);
						material.SetVector("_GameColor2", rect);
						material.SetFloat("_GameRotation2", rotation);
					break;

					case 3:
						material.SetTexture("_GameTexture3", lightTexture.renderTexture);
						material.SetVector("_GameRect3", rect);
						material.SetVector("_GameColor3", color);
						material.SetFloat("_GameRotation3", rotation);
					break;

					case 4:
						material.SetTexture("_GameTexture4", lightTexture.renderTexture);
						material.SetVector("_GameRect4", rect);
						material.SetVector("_GameColor4", rect);
						material.SetFloat("_GameRotation4", rotation);
					break;

					case 5:
						material.SetTexture("_GameTexture5", lightTexture.renderTexture);
						material.SetVector("_GameRect5", rect);
						material.SetVector("_GameColor5", color);
						material.SetFloat("_GameRotation5", rotation);
					break;

					case 6:
						material.SetTexture("_GameTexture6", lightTexture.renderTexture);
						material.SetVector("_GameRect6", rect);
						material.SetVector("_GameColor6", rect);
						material.SetFloat("_GameRotation6", rotation);
					break;

					case 7:
						material.SetTexture("_GameTexture7", lightTexture.renderTexture);
						material.SetVector("_GameRect7", rect);
						material.SetVector("_GameColor7", color);
						material.SetFloat("_GameRotation7", rotation);
					break;

					case 8:
						material.SetTexture("_GameTexture8", lightTexture.renderTexture);
						material.SetVector("_GameRect8", rect);
						material.SetVector("_GameColor8", rect);
						material.SetFloat("_GameRotation8", rotation);
					break;
				}
			}
				else
			{
				switch(id)
				{
					case 1:
						material.SetTexture("_SceneTexture1", lightTexture.renderTexture);
						material.SetVector("_SceneRect1", rect);
						material.SetVector("_SceneColor1", color);
						material.SetFloat("_SceneRotation1", rotation);
					break;

					case 2:
						material.SetTexture("_SceneTexture2", lightTexture.renderTexture);
						material.SetVector("_SceneRect2", rect);
						material.SetVector("_SceneColor2", rect);
						material.SetFloat("_SceneRotation2", rotation);
					break;

					case 3:
						material.SetTexture("_SceneTexture3", lightTexture.renderTexture);
						material.SetVector("_SceneRect3", rect);
						material.SetVector("_SceneColor3", color);
						material.SetFloat("_SceneRotation3", rotation);
					break;

					case 4:
						material.SetTexture("_SceneTexture4", lightTexture.renderTexture);
						material.SetVector("_SceneRect4", rect);
						material.SetVector("_SceneColor4", rect);
						material.SetFloat("_SceneRotation4", rotation);
					break;

					case 5:
						material.SetTexture("_SceneTexture5", lightTexture.renderTexture);
						material.SetVector("_SceneRect5", rect);
						material.SetVector("_SceneColor5", color);
						material.SetFloat("_SceneRotation5", rotation);
					break;

					case 6:
						material.SetTexture("_SceneTexture6", lightTexture.renderTexture);
						material.SetVector("_SceneRect6", rect);
						material.SetVector("_SceneColor6", rect);
						material.SetFloat("_SceneRotation6", rotation);
					break;

					case 7:
						material.SetTexture("_SceneTexture7", lightTexture.renderTexture);
						material.SetVector("_SceneRect7", rect);
						material.SetVector("_SceneColor7", color);
						material.SetFloat("_SceneRotation7", rotation);
					break;

					case 8:
						material.SetTexture("_SceneTexture8", lightTexture.renderTexture);
						material.SetVector("_SceneRect8", rect);
						material.SetVector("_SceneColor8", rect);
						material.SetFloat("_SceneRotation8", rotation);
					break;
				}
			}
		}

		public static void SetDayLight()
		{
			float direction = -(Lighting2D.DayLightingSettings.direction - 180) * Mathf.Deg2Rad;
			float height = Lighting2D.DayLightingSettings.bumpMap.height;
			float alpha = Lighting2D.DayLightingSettings.ShadowColor.a;

			Shader.SetGlobalFloat("_Day_Direction", direction);
			Shader.SetGlobalFloat("_Day_Height", height);
			Shader.SetGlobalFloat("_Day_Alpha", alpha);
		}

		public void Add(Material material)
		{
			foreach(Material m in  materials)
			{
				if (m == material)
				{
					Debug.Log("Lighting Manager 2D: Failed to add material (material already added!");
					return;
				}
			}

			for(int i = 0 ; i < materials.Length; i++)
			{
				if (materials[i] != null)
				{
					continue;
				}

				materials[i] = material;

				return;
			}

			System.Array.Resize(ref materials, materials.Length + 1);

			materials[materials.Length - 1] = material;
		}

		public void Remove(Material material)
		{
			for(int i = 0 ; i < materials.Length; i++)
			{
				if (materials[i] != material)
				{
					continue;
				}
				
				materials[i] = null;

				return;
			}

			Debug.LogWarning("Lighting Manager 2D: Removing material that does not exist");
		}
	}
}

/*
public static int GetFreeId() {
	Vector4 rect;

	rect = Shader.GetGlobalVector("_GameRect1");

	if (rect.z <= 0) {
		return(1);
	}

	rect = Shader.GetGlobalVector("_GameRect2");

	if (rect.z <= 0) {
		return(2);
	}

	rect = Shader.GetGlobalVector("_GameRect3");

	if (rect.z <= 0) {
		return(3);
	}

	rect = Shader.GetGlobalVector("_GameRect4");

	if (rect.z <= 0) {
		return(4);
	}

	Debug.Log("cant find");

	return(0);
}*/
