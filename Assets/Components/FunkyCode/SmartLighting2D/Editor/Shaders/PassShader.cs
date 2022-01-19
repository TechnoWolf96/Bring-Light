using UnityEngine;
using UnityEditor;

namespace FunkyCode
{
    public class PassShader : ShaderGUI
    {
        public enum EnumPass {None, Pass1, Pass2, Pass3, Pass4, Pass5, Pass6, Pass7, Pass8}

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material material = materialEditor.target as Material;

            int passId = material.GetInt("_PassId");

            EnumPass pass = (EnumPass)EditorGUILayout.EnumPopup("Pass", (EnumPass)passId);

            int newPassId = (int)pass;

            if (newPassId != passId)
            {
                material.SetInt("_PassId", newPassId);
                    
                switch(newPassId)
                {
                    case 0:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");
                    break;

                    case 1:
                        material.EnableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");
                    break;

                    case 2:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.EnableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");
                    break;

                    case 3:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.EnableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");
                    break;

                    case 4:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.EnableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");    
                    break;

                    case 5:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.EnableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");    
                    break;

                    case 6:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.EnableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");    
                    break;

                    case 7:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.EnableKeyword("SL2D_PASS_7");
                        material.DisableKeyword("SL2D_PASS_8");    
                    break;

                    case 8:
                        material.DisableKeyword("SL2D_PASS_1");
                        material.DisableKeyword("SL2D_PASS_2");
                        material.DisableKeyword("SL2D_PASS_3");
                        material.DisableKeyword("SL2D_PASS_4");
                        material.DisableKeyword("SL2D_PASS_5");
                        material.DisableKeyword("SL2D_PASS_6");
                        material.DisableKeyword("SL2D_PASS_7");
                        material.EnableKeyword("SL2D_PASS_8");    
                    break;
                }
            }

            base.OnGUI(materialEditor, properties);
        }
    }
}