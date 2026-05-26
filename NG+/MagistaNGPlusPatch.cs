using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    class MagistaNGPlusPatch
    {

        [HarmonyPatch(typeof(Duchess),"Awake")]
        [HarmonyPostfix]
        static void AwakePatch(Duchess __instance)
        {
			if (Plugin.debugMode || (Plugin.connection.connected && CrabFile.current.GetBool("ngplusBosses")))
			{
				StaticHitbox[] hitboxes = Traverse.Create(__instance).Field("hitboxes").GetValue<StaticHitbox[]>();
				ParticleSystem[] replaceVFXNGPlus = Traverse.Create(__instance).Field("replaceVFXNGPlus").GetValue<ParticleSystem[]>();

				for (int i = 0; i < hitboxes.Length; i++)
				{
					if (hitboxes[i] != null && hitboxes[i].afflictionType == HitEvent.AFFLICTIONTYPE.GUNK)
					{
						hitboxes[i].afflictionType = HitEvent.AFFLICTIONTYPE.SCOUR;
					}
				}
				replaceVFXNGPlus[0].gameObject.SetActive(true);
				replaceVFXNGPlus[1].gameObject.SetActive(true);
				replaceVFXNGPlus[2].gameObject.SetActive(true);
				__instance.view.vfx[0].gameObject.SetActive(false);
				__instance.view.vfx[4].gameObject.SetActive(false);
				__instance.view.vfx[11].gameObject.SetActive(false);
				__instance.view.vfx[0] = replaceVFXNGPlus[0];
				__instance.view.vfx[4] = replaceVFXNGPlus[1];
				__instance.view.vfx[11] = replaceVFXNGPlus[2];
			}
		}

		[HarmonyPatch(typeof(Duchess), "SpawnGunkPuddle")]
		[HarmonyPrefix]
		static bool SpawnPuddlePrefix(ref Transform tf, Duchess __instance)
		{
			if (Plugin.debugMode || (Plugin.connection.connected && CrabFile.current.GetBool("ngplusBosses")))
			{
				Vector3 gunkZfightOffset = Traverse.Create(__instance).Field("gunkZfightOffset").GetValue<Vector3>();
				List<GunkPuddle> gunkPuddles = Traverse.Create(__instance).Field("gunkPuddles").GetValue<List<GunkPuddle>>();

				GunkPuddle gunkPuddle = UnityEngine.Object.Instantiate<GunkPuddle>(CrabFile.current.progressData.isNewGamePlus ? __instance.gunkPuddleNewGamePlus : __instance.gunkPuddle, __instance.transform);
				gunkPuddle.transform.SetParent(null, true);
				RaycastHit raycastHit;
				Physics.Raycast(tf.position + Vector3.up, Vector3.down, out raycastHit, 20f, GlobalSettings.settings.groundLayers.value, QueryTriggerInteraction.Ignore);
				if (raycastHit.collider)
				{
					gunkPuddle.transform.position = raycastHit.point;
				}
				else
				{
					gunkPuddle.transform.position = tf.position;
				}
				gunkPuddle.transform.position += gunkZfightOffset;
				gunkPuddle.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f);
				GameManager.instance.StartCoroutine(Traverse.Create(__instance).Method("gunkZfightOffset", new object[] { gunkPuddle }).GetValue<IEnumerator>());
				gunkPuddles.Add(gunkPuddle);
				gunkZfightOffset.y = gunkZfightOffset.y + 0.001f;
				return false;
			}

			return true;
		}
    }
}
