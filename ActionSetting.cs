using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using static DualSenseX.Settings;

namespace DualSenseX
{
    public class ActionSetting
    {
        public ActionMode actionMode;

        public TriggerMode LeftTriggerMode = TriggerMode.Normal;
        public int[] LeftTriggerParameter = new int[7];
        public int LeftTriggerArgumentCount = 0;
        public int LeftTriggerThreshold = 0;
        public CustomTriggerValueMode LeftTriggerCustomValueMode;
        public object[] LeftTriggerPacket = null;


        public TriggerMode RightTriggerMode = TriggerMode.Normal;
        public int[] RightTriggerParameter = new int[7];
        public int RightTriggerArgumentCount = 0;
        public int RightTriggerThreshold = 0;
        public CustomTriggerValueMode RightTriggerCustomValueMode;
        public object[] RightTriggerPacket = null;

        public void PrintInfoToLog()
        {
            MelonLogger.Msg("Loaded ActionSetting: " + actionMode);

            MelonLogger.Msg("[Left]");
            MelonLogger.Msg("TriggerMode: " + LeftTriggerMode);
            MelonLogger.Msg("ParameterCount: " + LeftTriggerArgumentCount);
            MelonLogger.Msg("Parameter: " + LeftTriggerParameter[0] + ", " + LeftTriggerParameter[1] + ", " + LeftTriggerParameter[2] + ", " + LeftTriggerParameter[3] + ", " + LeftTriggerParameter[4] + ", " + LeftTriggerParameter[5] + ", " + LeftTriggerParameter[6]);
            MelonLogger.Msg("TriggerThreshold: " + LeftTriggerThreshold);
            MelonLogger.Msg("CustomTriggerValueMode: " + LeftTriggerCustomValueMode);

            MelonLogger.Msg("[Right]");
            MelonLogger.Msg("TriggerMode: " + RightTriggerMode);
            MelonLogger.Msg("ParameterCount: " + RightTriggerArgumentCount);
            MelonLogger.Msg("Parameter: " + RightTriggerParameter[0] + ", " + RightTriggerParameter[1] + ", " + RightTriggerParameter[2] + ", " + RightTriggerParameter[3] + ", " + RightTriggerParameter[4] + ", " + RightTriggerParameter[5] + ", " + RightTriggerParameter[6]);
            MelonLogger.Msg("TriggerThreshold: " + RightTriggerThreshold);
            MelonLogger.Msg("CustomTriggerValueMode: " + RightTriggerCustomValueMode);
        }


        public void UpdatePaket()
        {
            if (LeftTriggerMode == TriggerMode.Normal ||
                LeftTriggerMode == TriggerMode.GameCube ||
                LeftTriggerMode == TriggerMode.VerySoft ||
                LeftTriggerMode == TriggerMode.Soft ||
                LeftTriggerMode == TriggerMode.Hard ||
                LeftTriggerMode == TriggerMode.VeryHard ||
                LeftTriggerMode == TriggerMode.Hardest ||
                LeftTriggerMode == TriggerMode.Rigid ||
                LeftTriggerMode == TriggerMode.Choppy ||
                LeftTriggerMode == TriggerMode.Medium)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, LeftTriggerMode };
            }
            else if (LeftTriggerMode == TriggerMode.CustomTriggerValue)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.CustomTriggerValue, LeftTriggerCustomValueMode, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2], LeftTriggerParameter[3], LeftTriggerParameter[4], LeftTriggerParameter[5], LeftTriggerParameter[6] };
            }
            else if (LeftTriggerMode == TriggerMode.Resistance)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.Resistance, LeftTriggerParameter[0], LeftTriggerParameter[1] };
            }
            else if (LeftTriggerMode == TriggerMode.Bow)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.Bow, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2], LeftTriggerParameter[3] };
            }
            else if (LeftTriggerMode == TriggerMode.Galloping)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.Galloping, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2], LeftTriggerParameter[3], LeftTriggerParameter[4] };
            }
            else if (LeftTriggerMode == TriggerMode.SemiAutomaticGun)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.SemiAutomaticGun, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2] };
            }
            else if (LeftTriggerMode == TriggerMode.AutomaticGun)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.AutomaticGun, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2] };
            }
            else if (LeftTriggerMode == TriggerMode.Machine)
            {
                LeftTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Left, TriggerMode.Machine, LeftTriggerParameter[0], LeftTriggerParameter[1], LeftTriggerParameter[2], LeftTriggerParameter[3], LeftTriggerParameter[4], LeftTriggerParameter[5] };
            }

            if (RightTriggerMode == TriggerMode.Normal ||
               RightTriggerMode == TriggerMode.GameCube ||
               RightTriggerMode == TriggerMode.VerySoft ||
               RightTriggerMode == TriggerMode.Soft ||
               RightTriggerMode == TriggerMode.Hard ||
               RightTriggerMode == TriggerMode.VeryHard ||
               RightTriggerMode == TriggerMode.Hardest ||
               RightTriggerMode == TriggerMode.Rigid ||
               RightTriggerMode == TriggerMode.Choppy ||
               RightTriggerMode == TriggerMode.Medium)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, RightTriggerMode };
            }
            else if (RightTriggerMode == TriggerMode.CustomTriggerValue)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.CustomTriggerValue, RightTriggerCustomValueMode, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2], RightTriggerParameter[3], RightTriggerParameter[4], RightTriggerParameter[5], RightTriggerParameter[6] };
            }
            else if (RightTriggerMode == TriggerMode.Resistance)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.Resistance, RightTriggerParameter[0], RightTriggerParameter[1] };
            }
            else if (RightTriggerMode == TriggerMode.Bow)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.Bow, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2], RightTriggerParameter[3] };
            }
            else if (RightTriggerMode == TriggerMode.Galloping)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.Galloping, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2], RightTriggerParameter[3], RightTriggerParameter[4] };
            }
            else if (RightTriggerMode == TriggerMode.SemiAutomaticGun)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.SemiAutomaticGun, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2] };
            }
            else if (RightTriggerMode == TriggerMode.AutomaticGun)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.AutomaticGun, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2] };
            }
            else if (RightTriggerMode == TriggerMode.Machine)
            {
                RightTriggerPacket = new object[] { Settings.options.controllerIndex, Trigger.Right, TriggerMode.Machine, RightTriggerParameter[0], RightTriggerParameter[1], RightTriggerParameter[2], RightTriggerParameter[3], RightTriggerParameter[4], RightTriggerParameter[5] };
            }
        }
    }
}