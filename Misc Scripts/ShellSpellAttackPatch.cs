using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ACTAP
{
    [HarmonyPatch(typeof(Player),"Attack")]
    class ShellSpellAttackPatch
    {
		static MethodInfo atkSpeed = AccessTools.Method(typeof(Player), "AttackSpeed");
		static MethodInfo hmrSpeed = AccessTools.Method(typeof(Player), "HammerSpeed");
		static MethodInfo resetAtk = AccessTools.Method(typeof(Player), "ResetAttackBehavior");
		static MethodInfo climbAtk = AccessTools.Method(typeof(Player), "ClimbAttack");
		static MethodInfo airAtk = AccessTools.Method(typeof(Player), "AirAttack");
		static MethodInfo sendAudio = AccessTools.Method(typeof(Player), "SendAudioAggroEventAttack");
		static MethodInfo tryAutoAim = AccessTools.Method(typeof(Player), "TryMeleeAutoAim");
		static MethodInfo autoAim = AccessTools.Method(typeof(Player), "AutoAim");
		static MethodInfo buffer = AccessTools.Method(typeof(Player), "BufferAction");

		static FieldInfo eafTarget = AccessTools.Field(typeof(Player), "ebbAndFlowTarget");
		static FieldInfo eafTime = AccessTools.Field(typeof(Player), "ebbAndFlowTime");
		static FieldInfo canRip = AccessTools.Field(typeof(Player), "canRiposte");
		static PropertyInfo parryInv = AccessTools.Property(typeof(Player), "parryInvincibility");
		static FieldInfo molt = AccessTools.Field(typeof(Player), "molted");
		static FieldInfo lerpedRun = AccessTools.Field(typeof(Player), "lerpedRunSpeedParam");

		delegate bool TryMeleeAutoAim(bool isDashAttack, out Enemy bestTarget);
		static TryMeleeAutoAim tryMeleeAuto;

		[HarmonyPrefix]
        static bool Prefix(Player __instance, ref bool __result, string forceAnimation = "", int umamiCost = 0, Player.BufferedAction.ACTIONTYPE actionType = Player.BufferedAction.ACTIONTYPE.ATTACK)
        {
			if (forceAnimation == "")
            {
				return true;
            }

			Debug.Log("Try Attack " + forceAnimation);
			/*if (!CrabFile.current.unlocks[SkillWorldUnlocks.BasicFork].unlocked)
			{
				__result = false;
			}*/
			if (!__instance.visuallyGrounded && __instance.airActioned)
			{
				__result = false;
				return false;
			}
			float num = (float)atkSpeed.Invoke(__instance, new object[] { });
			__instance.anim.SetFloat("AttackSpeed", num);
			if (__instance.equippedShellHammer)
			{
				__instance.anim.SetFloat("HammerSpeed", num * (float)hmrSpeed.Invoke(__instance, new object[] { }));
			}
			bool flag = false;
			if (actionType == Player.BufferedAction.ACTIONTYPE.UMAMIATTACK)
			{
				GameManager.events.TriggerUmamiTechniqueUsed();
			}
			if (actionType == Player.BufferedAction.ACTIONTYPE.EBBANDFLOW)
			{
				GameManager.instance.EndSlowMo();
				Vector3 vector = ((Entity)eafTarget.GetValue(__instance)).GetCenter() - __instance.GetCenter();
				__instance.SnapLook(global::Util.ZeroY(vector));
			}
			if (!string.IsNullOrEmpty(forceAnimation))
			{
				if (forceAnimation == "MantisPunchTeleport")
				{
					bool flag2 = false;
					if (__instance.focusedEnemy && Vector3.Distance(__instance.transform.position, __instance.focusedEnemy.transform.position) < 30f)
					{
						flag2 = true;
					}
					else if (CameraController.instance.potentialFocusTarget && Vector3.Distance(__instance.transform.position, CameraController.instance.potentialFocusTarget.transform.position) < 30f)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						forceAnimation = "MantisPunch";
					}
				}
				__instance.nextAttackChain = forceAnimation;
			}
			if (__instance.isStepBackDodge && (float)eafTime.GetValue(__instance) > Time.time - __instance.stats.stepbackDodgeStats.ebbAndFlowProcWindow && CrabFile.current.unlocks[SkillTreeUnlocks.EbbAndFlow].unlocked)
			{
				resetAtk.Invoke(__instance, new object[] { });
				__instance.anim.SetTrigger("EbbAndFlowPre");
				__instance.isStepBackDodge = false;
				__instance.dodging = false;
				GameManager.instance.SlowMo(0.2f, 0.25f);
				__instance.MakeInvincible(1.5f);
				__result = true;
				return false;
			}
			if (actionType == Player.BufferedAction.ACTIONTYPE.ATTACK && __instance.groundDetector.touchingGround && __instance.ChargeInteract())
			{
				__result = false;
			}
			if ((bool)canRip.GetValue(__instance) && (bool)parryInv.GetValue(__instance) && CrabFile.current.unlocks[SkillTreeUnlocks.Riposte].unlocked && actionType == Player.BufferedAction.ACTIONTYPE.ATTACK)
			{
				canRip.SetValue(__instance,false);
				__instance.anim.SetTrigger("Riposte");
				__result = true;
				return false;
			}
			if (__instance.nextAttackChain == "")
			{
				if (__instance.climbing)
				{
					bool flag3 = __instance.debugPrintAttack;
					climbAtk.Invoke(__instance,new object[] { });
					flag = true;
				}
				else if ((bool)molt.GetValue(__instance))
				{
					__instance.nextAttackChain = "MoltAttack";
				}
				else if (!__instance.visuallyGrounded)
				{
					bool flag4 = __instance.debugPrintAttack;
					airAtk.Invoke(__instance, new object[] { });
				}
				else if (__instance.sprinting && (float)lerpedRun.GetValue(__instance) > 1.6f && CrabFile.current.unlocks[SkillTreeUnlocks.Skewer].unlocked)
				{
					bool flag5 = __instance.debugPrintAttack;
					__instance.nextAttackChain = "AttackSprinting";
				}
				else
				{
					bool flag6 = __instance.debugPrintAttack;
					__instance.nextAttackChain = "Attack1";
				}
			}
			if (!__instance.drinking && (!__instance.attacking || __instance.canAttackCancel) && (!__instance.dodging || actionType == Player.BufferedAction.ACTIONTYPE.EBBANDFLOW) && !__instance.grabbingItem)
			{
				Debug.Log("In Deep");

				Enemy enemy;

				tryMeleeAuto = (TryMeleeAutoAim)Delegate.CreateDelegate(typeof(TryMeleeAutoAim),__instance, tryAutoAim);
				
				//object[] args = new object[] { __instance.nextAttackChain == "AttackSprinting",  out enemy };

				if (tryMeleeAuto.Invoke(__instance.nextAttackChain == "AttackSprinting",out enemy))
				{
					
					autoAim.Invoke(__instance, new object[] { enemy });
				}
				else
				{
					__instance.ForceLookInControllerDirection();
				}
				__instance.currentAttackUmamiCost = (float)umamiCost;
				bool flag7 = __instance.debugPrintAttack;
				sendAudio.Invoke(__instance, new object[]{ });
				resetAtk.Invoke(__instance, new object[] { });
				__instance.anim.SetTrigger(__instance.nextAttackChain);
				flag = true;
			}
			else if (__instance.canBufferAction)
			{
				bool flag8 = __instance.debugPrintAttack;
				buffer.Invoke(__instance, new object[] { actionType });
			}
			__result = flag;
			return false;
		}
    }
}
