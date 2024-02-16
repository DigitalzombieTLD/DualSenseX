using UnityEngine;
using ModSettings;
using MelonLoader;
using DualSenseX.Utils.Filesystem;
using System.Reflection;
using DualSenseX.Utils.Ini.Model;
using Newtonsoft.Json.Linq;

namespace DualSenseX
{
    public class DualSenseXSettings : JsonModSettings
    {     
		[Section("General")]

        [Name("Enable")]
        [Description("Enable/Disable the connection to the DualSenseX application")]
        public bool enabled = true;

        [Name("Controller")]
        [Description("Assigned controller (0-3)")]
        [Slider(0, 3)]
        public int controllerIndex = 0;

        [Name("Force Settings Reload")]
        [Description("Assign button to hot reload the effect settings and reconnect to the DualSenseX application")]      
        public KeyCode hotReload = KeyCode.None;

        [Section("Aurora")]

        [Name("LED effects")]
        [Description("Enable/Disable aurora LED effects")]
        public bool enableAuroraLED = false;
            

        [Section("Debug")]
        [Name("Enable debug mode")]
        [Description("Enable")]
        public bool enableDebug = false;


		protected override void OnConfirm()
        {
            base.OnConfirm();     
            
            if(!enableAuroraLED)
            {
                UDPManager.SetRGB(new Color(1, 1, 1));
            }         

            if (enabled)
            {
                UDPManager.Disconnect();
                Settings.GetActionModeSettings();
                UDPManager.TryConnect();
            }
            else
            {
                UDPManager.Disconnect();
            }
        }
    }

    public static class Settings
    {
        public static DualSenseXSettings options;
        public static int udpPort = 6969;
        public static DualSenseX.Utils.Ini.IniFile settingsFile;

        public enum ActionMode { None, Default, Stone, Flashlight, Lantern, Flare, FlareGun, Bow, Revolver, Rifle, Torch, Matches };
        public static Dictionary<ActionMode, ActionSetting> actionSettings;

        public static void GetActionModeSettings()
        {
            actionSettings = new Dictionary<ActionMode, ActionSetting>();
           
            foreach (SectionData section in settingsFile._thisIniData.Sections)
            {
                if(section.SectionName == "General")
                {
                    string key = settingsFile.GetString("Port", "General");
                    key = key.Replace(" ", "");

                    if (key != "" && int.TryParse(key, System.Globalization.NumberStyles.Integer, null, out int value))
                    {
                        udpPort = value;
                    }
                }
                else
                {
                    ActionMode actionMode;

                    if (Enum.TryParse(section.SectionName, out actionMode))
                    {
                        bool error = false;

                        TriggerMode LeftTriggerMode = TriggerMode.Normal;
                        int[] LeftTriggerParameter = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
                        int LeftTriggerArgumentCount = 0;
                        int LeftTriggerThreshold = 0;
                        CustomTriggerValueMode LeftTriggerCustomValueMode = CustomTriggerValueMode.OFF; ;

                        TriggerMode RightTriggerMode = TriggerMode.Normal;
                        int[] RightTriggerParameter = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
                        int RightTriggerArgumentCount = 0;
                        int RightTriggerThreshold = 0;
                        CustomTriggerValueMode RightTriggerCustomValueMode = CustomTriggerValueMode.OFF;

                        TriggerMode modeValue;
                        CustomTriggerValueMode triggerValueMode;

                        foreach (KeyData key in section.Keys)
                        {
                            if(key.KeyName == "LeftTriggerMode")
                            {
                                if (Enum.TryParse(key.Value, out modeValue))
                                {
                                    LeftTriggerMode = modeValue;
                                }
                            }
                            else if (key.KeyName == "LeftTriggerParameter")
                            {
                                string tempString = key.Value.Replace(" ", "");
                                string[] stringParts = tempString.Split(',');                              

                                foreach (string part in stringParts)
                                {
                                    if (LeftTriggerArgumentCount < 7)
                                    {
                                        if (int.TryParse(part, System.Globalization.NumberStyles.Integer, null, out int value))
                                        {
                                            LeftTriggerParameter[LeftTriggerArgumentCount] = value;
                                            LeftTriggerArgumentCount++;
                                        }
                                    }
                                }
                            }
                            else if (key.KeyName == "LeftTriggerThreshold")
                            {
                                if (int.TryParse(key.Value, System.Globalization.NumberStyles.Integer, null, out int value))
                                {
                                    LeftTriggerThreshold = value;
                                }
                            }
                            else if (key.KeyName == "LeftTriggerCustomValueMode")
                            {
                                if (Enum.TryParse(key.Value, out triggerValueMode))
                                {
                                    LeftTriggerCustomValueMode = triggerValueMode;
                                }
                            }
                            else if (key.KeyName == "RightTriggerMode")
                            {
                                if (Enum.TryParse(key.Value, out modeValue))
                                {
                                    RightTriggerMode = modeValue;
                                }
                            }
                            else if (key.KeyName == "RightTriggerParameter")
                            {
                                string tempString = key.Value.Replace(" ", "");
                                string[] stringParts = tempString.Split(',');

                                foreach (string part in stringParts)
                                {
                                    if (RightTriggerArgumentCount < 7)
                                    {
                                        if (int.TryParse(part, System.Globalization.NumberStyles.Integer, null, out int value))
                                        {
                                            RightTriggerParameter[RightTriggerArgumentCount] = value;
                                            RightTriggerArgumentCount++;
                                        }
                                    }                                      
                                }
                            }
                            else if (key.KeyName == "RightTriggerThreshold")
                            {
                                if (int.TryParse(key.Value, System.Globalization.NumberStyles.Integer, null, out int value))
                                {
                                    RightTriggerThreshold = value;
                                }
                            }
                            else if (key.KeyName == "RightTriggerCustomValueMode")
                            {
                                if (Enum.TryParse(key.Value, out triggerValueMode))
                                {
                                    RightTriggerCustomValueMode = triggerValueMode;
                                }
                            }
                        }

                        actionSettings.Add(actionMode, new ActionSetting());
                        actionSettings[actionMode].actionMode = actionMode;
                        actionSettings[actionMode].LeftTriggerMode = LeftTriggerMode;
                        actionSettings[actionMode].LeftTriggerArgumentCount = LeftTriggerArgumentCount;
                        actionSettings[actionMode].LeftTriggerParameter= LeftTriggerParameter;
                        actionSettings[actionMode].LeftTriggerThreshold = LeftTriggerThreshold;
                        actionSettings[actionMode].LeftTriggerCustomValueMode = LeftTriggerCustomValueMode;

                        actionSettings[actionMode].RightTriggerMode = RightTriggerMode;
                        actionSettings[actionMode].RightTriggerArgumentCount = RightTriggerArgumentCount;
                        actionSettings[actionMode].RightTriggerParameter = RightTriggerParameter;
                        actionSettings[actionMode].RightTriggerThreshold = RightTriggerThreshold;
                        actionSettings[actionMode].RightTriggerCustomValueMode = RightTriggerCustomValueMode;
                        actionSettings[actionMode].UpdatePaket();

                        if(Settings.options.enableDebug)
                        {
                            actionSettings[actionMode].PrintInfoToLog();
                        }                        
                    }                        
                }                
            }
        }

        public static void OnLoad()
        {
            options = new DualSenseXSettings();
            options.AddToModSettings("DualSenseX");

            if (!Files.FileExist("DualSenseXSettings.ini"))
            {
                MemoryStream memoryStream;
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DualSenseX.Resources.DualSenseXSettings.ini");
                memoryStream = new MemoryStream((int)stream.Length);
                stream.CopyTo(memoryStream);

                File.WriteAllBytes(Path.Combine(Files.rootPath, "DualSenseXSettings.ini"), memoryStream.ToArray());
            }

            settingsFile = new DualSenseX.Utils.Ini.IniFile("DualSenseXSettings.ini");

            GetActionModeSettings();

            if (Settings.options.enabled)
            {
                UDPManager.TryConnect();
            }
            else
            {
                UDPManager.Disconnect();
            }
        }
    }
}
