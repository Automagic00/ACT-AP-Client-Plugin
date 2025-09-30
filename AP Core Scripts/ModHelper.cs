using ACTAP.Extensions;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ACTAP
{
    public static class ModHelper
    {
        internal static readonly Dictionary<string, Sprite> Sprites = new();

        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(Plugin));
        }

        public static Sprite GetSprite(string fileName)
        {
            Assembly mod = GetAssembly();
            var embededRes = mod.GetManifestResourceNames();

            if (Sprites.ContainsKey(fileName))
            {
                return Sprites[fileName];
            }
            else
            {
                byte[] resource = null;
                Texture2D texture = new(2, 2) { filterMode = FilterMode.Bilinear };
                foreach (string e in embededRes)
                {
                    string[] resourcePath = e.Split('.');
                    string resourceName = resourcePath[2];

                    if (resourceName == fileName)
                    {
                        resource = mod.GetManifestResourceStream(e).GetByteArray();
                        break;
                    }
                }
                if (resource != null)
                {
                    ImageConversion.LoadImage(texture, resource);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 10.8f);
                    Sprites[fileName] = sprite;
                    return sprite;
                }
            }
            
            return null;
        }
        /*
        public static void Msg(string msg)
        {
            ModMain.logSource.LogInfo(msg);
        }*/
    }
}
