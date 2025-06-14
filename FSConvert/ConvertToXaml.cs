#if NET40_OR_GREATER || NETCOREAPP

// based on  http://robrelyea.wordpress.com/2007/02/10/winforms-xaml/
// converted to an Extension Method by @CADbloke
//  a list: http://msdn.microsoft.com/en-us/library/ms750559(v=vs.110).aspx
// here's moar code:http://wf2wpf.codeplex.com/SourceControl/latest but it converts source files, not actual controls.
// Here's a site that does code too http://www.win2wpf.com/
// http://www.codeproject.com/Articles/25795/Creating-the-Same-Program-in-Windows-Forms-and-WPF


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;

namespace FSConvert
{
    /// <summary> Converts <seealso cref="System.Windows.Forms.Control"/>s to WPF XAML. </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl"/>
    ///  <seealso cref="T:System.Windows.Forms.Form"/>
    public static class ConvertToXaml
    {
        /// <summary> Generates the WPF XAML equivalent for a <see cref="System.Windows.Forms.Control"/>
        ///           and its children. The XAML won't be perfect - you will need to edit it. A lot.
        ///           Some <see cref="System.Windows.Forms.Control"/>s may not convert and will be called the wrong type.
        ///           You will see a lot more Events than you probably want. They are easier to delete than to create so quit yer whinging.
        ///           You should be able to use most of your existing code-behind. In theory
        ///           The XAML will also (optionally) be on the Windows Clipboard, paste it into your text editor. </summary>
        /// <param name="windowsControl"> The <see cref="System.Windows.Forms.Form"/> to be converted to XAML. </param>
        /// <param name="toolTipProvider">Optional. Your <seealso cref="ToolTip"/> component. Provides the tooltips.</param>
        /// <param name="includeAllFromParentFormOrUserControl"> XAML includes Everything in the Top parent container. 
        ///                                          Default is true</param>
        /// <param name="doColorsAndFontsForEveryIndividualControl">True if you want all the <seealso cref="Font"/> and 
        ///             <seealso cref="Color"/> information for each and every control, 
        ///                                     rather than going with the overall defaults. Default is False. </param>
        /// <param name="copyXamlToClipboard">True if you want the XAML in the Windows Clipboard to paste into an editor. 
        ///                               Default is true</param>
        /// <param name="doEvents">True if you want the <see cref="System.Windows.Forms"/> Events translated into XAML. Default is true.</param>
        /// <param name="eventsToExclude">List any <see cref="System.Windows.Forms"/> or WPF Events you don't want to see in XAML.</param>
        /// <param name="eventsToInclude">List any <see cref="System.Windows.Forms"/> or WPF Events you DO want to see in XAML. 
        ///                               All others are ignored.</param>
        /// <param name="regexOfEventsToInclude">Now you have two problems. Trumps <see cref="eventsToInclude"/></param>
        /// <returns> The generated WPF XAML as a string. </returns>
        public static string Convert(this Control windowsControl,
                                                               ToolTip toolTipProvider = null,
                                                                  bool includeAllFromParentFormOrUserControl = true,
                                                                  bool doColorsAndFontsForEveryIndividualControl = false,
                                                                  bool copyXamlToClipboard = true,
                                                                  bool doEvents = true,
                                                              string[] eventsToExclude = null,
                                                              string[] eventsToInclude = null,
                                                                string regexOfEventsToInclude = null)
        {
            if (windowsControl == null) return string.Empty;

            _toolTipProvider = toolTipProvider;
            _includeAllFromParentFormOrUserControl = includeAllFromParentFormOrUserControl;
            _doColorsAndFontsForEveryIndividualControl = doColorsAndFontsForEveryIndividualControl;
            _copyXamlToClipboard = copyXamlToClipboard;
            _doEvents = doEvents;
            _eventsToExclude = eventsToExclude;
            _eventsToInclude = eventsToInclude;
            _regexOfEventsToInclude = regexOfEventsToInclude;

            wpfBuilder.Clear();

            Control topParent = windowsControl;
            while ((topParent.Parent != null) && !(topParent is UserControl || topParent is Form)) topParent = topParent.Parent;

            if (_includeAllFromParentFormOrUserControl) windowsControl = topParent;

            wpfBuilder.AppendLine("<Page xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            wpfBuilder.AppendLine("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            wpfBuilder.AppendLine("xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" ");
            wpfBuilder.AppendLine("xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"");
            wpfBuilder.AppendLine("d:DesignHeight=\"" + windowsControl.Size.Width +
                               "\" d:DesignWidth=\"" + windowsControl.Size.Height + "\"");

            WriteBrushAttribute("Background", windowsControl.BackColor, "0");
            WriteBrushAttribute("Foreground", windowsControl.ForeColor, "ControlText");
            WriteAttribute("Width", windowsControl.Size.Width.ToString(), "0");
            WriteAttribute("Height", windowsControl.Size.Height.ToString(), "0");

            WriteEvents<Control>(windowsControl);

            wpfBuilder.AppendLine(">");
            wpfBuilder.AppendLine("  <StackPanel>");
            WalkControls(windowsControl);
            wpfBuilder.AppendLine("  </StackPanel>");
            wpfBuilder.AppendLine("</Page>");

            string xaml = wpfBuilder.ToString();

            if (_copyXamlToClipboard) Clipboard.SetData(DataFormats.Text, xaml);

            return xaml;
        }


        // ===============================    done. Now start deleting stuff   =================


        /// <summary> Iterates through a Control and its children to populate a your XAMLs. </summary>
        /// <param name="parentControl">   The <see cref="Control"/> to convert to XAML, along with its children. </param>
        private static void WalkControls(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control.HasChildren) wpfBuilder.AppendLine("  </StackPanel>"); // ungrouped controls on a WinForm need a container

                wpfBuilder.Append("  <" + WpfControlName(control));

                WriteAttribute("Name", control.Name, "");

                if (_doColorsAndFontsForEveryIndividualControl)
                {
                    WriteBrushAttribute("Background", control.BackColor, "0");
                    WriteBrushAttribute("Foreground", control.ForeColor, "ControlText");
                    wpfBuilder.Append(" FontSize=\"" + control.Font.SizeInPoints + "pt\"");
                    wpfBuilder.Append(" FontFamily=\"" + control.Font.FontFamily.Name + "\"");
                }

                WriteAttribute("Width", control.Size.Width.ToString(), "0");
                WriteAttribute("Height", control.Size.Height.ToString(), "0");
                WriteAttribute("TabIndex", control.TabIndex.ToString(), "0");

                WriteAttribute("VerticalAlignment", (control.Anchor & AnchorStyles.Bottom) == control.Anchor ? "Bottom" : "Top", "x");
                WriteAttribute("HorizontalAlignment", (control.Anchor & AnchorStyles.Right) == control.Anchor ? "Right" : "Left", "x");

                WriteAttribute((control is GroupBox) ? "Header" : "Content", control.Text.Replace(@"&", "_"), ""); // & _ == shortcut key


                string tag = string.Empty;

                try
                {
                    if (control.Tag != null)
                        tag = control.Tag.ToString();
                }
                catch (Exception)
                {
                }

                if (tag.Length > 0) WriteAttribute("Tag", tag, "");

                if (_toolTipProvider != null)
                    WriteAttribute("ToolTip", _toolTipProvider.GetToolTip(control).Replace("\"", ""), "");

                WriteEvents<Control>(control);

                if (control.HasChildren)
                {
                    wpfBuilder.AppendLine(">");
                    wpfBuilder.AppendLine("  <StackPanel>");

                    WalkControls(control);

                    wpfBuilder.AppendLine("  </StackPanel>");

                    wpfBuilder.AppendLine("  </" + controlsTranslator[control.GetType().Name].ToList().FirstOrDefault() + ">");
                    wpfBuilder.AppendLine("  <StackPanel>"); // ungrouped controls on a WinForm need a container
                }
                else wpfBuilder.AppendLine("  />");
            }
        }

        private static string WpfControlName(Control control)
        {
            List<string> list = controlsTranslator[control.GetType().Name].ToList();
            if (list.Count > 0)
            {
                return list.FirstOrDefault();
            }
            else return control.GetType().Name;
        }

        /// <summary> Writes a XAML attribute. </summary>
        /// <param name="attributeName">  The name of the Attribute. </param>
        /// <param name="attributeValue"> The value of the Attribute. </param>
        /// <param name="defaultValue"> The default value. Attribute is not written if it has the default value</param>
        private static void WriteAttribute(string attributeName, string attributeValue, string defaultValue)
        {
            if (attributeValue != defaultValue)
                wpfBuilder.AppendLine("     " + attributeName + "=\"" + attributeValue + "\"");
        }


        /// <summary> Writes a XAML brush attribute. </summary>
        /// <param name="brushName">         The name of the Attribute. </param>
        /// <param name="brushValue">        The value of the Attribute. </param>
        /// <param name="defaultValue"> The default value. Attribute is not written if it has the default value. </param>
        private static void WriteBrushAttribute(string brushName, Color brushValue, string defaultValue)
        {
            string finalValue = brushValue.ToKnownColor().ToString();

            if (brushValue.IsNamedColor)
            {
                if (finalValue == "Window") finalValue = "{x:Static SystemColors.WindowBrush}";
                else if (finalValue == "ControlText") finalValue = defaultValue;
                else if (finalValue == "ActiveCaption") finalValue = "{x:Static SystemColors.ActiveCaptionBrush}";
                else if (finalValue == "WindowText") finalValue = "{x:Static SystemColors.WindowTextBrush}";

                WriteAttribute(brushName, finalValue, defaultValue);
            }

            else
            {
                StringBuilder colorBuilder = new StringBuilder();
                colorBuilder.Append("#");
                colorBuilder.AppendFormat("{0:X2}", brushValue.R);
                colorBuilder.AppendFormat("{0:X2}", brushValue.G);
                colorBuilder.AppendFormat("{0:X2}", brushValue.B);
                WriteAttribute(brushName, colorBuilder.ToString(), defaultValue);
            }
        }





        /// <summary> Writes the events of the control, translating the names from 
        ///           <seealso cref="System.Windows.Forms"/> to XAML. 
        ///           Also - you know abou WPF Triggers, right?</summary>
        /// <param name="windowsControl"> The <see cref="System.Windows.Forms.Control"/> whose Events get converted to XAML. </param>
        private static void WriteEvents<T>(Control windowsControl) // where T : Control
        { // http://stackoverflow.com/questions/660480/determine-list-of-event-handlers-bound-to-event/660489#660489
            if (!_doEvents) return;

            EventHandlerList eventsHandlersList = (EventHandlerList)typeof(T)
              .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
              .GetValue(windowsControl, null);

            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy);



            if (windowsControl is CheckBox)
            {
                FieldInfo[] moarFields = {
                                   typeof(CheckBox).GetField("EVENT_CHECKEDCHANGED",BindingFlags.NonPublic | BindingFlags.Static| BindingFlags.Instance | BindingFlags.FlattenHierarchy),
                                   typeof(CheckBox).GetField("EVENT_CHECKSTATECHANGED", BindingFlags.NonPublic | BindingFlags.Static| BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                                 };
                Array.Resize(ref fields, fields.Length + moarFields.Length);
                moarFields.CopyTo(fields, fields.Length - moarFields.Length);
            }

            foreach (var fieldInfo in fields)
            {
                string fieldName = fieldInfo.Name;
                if (fieldInfo.Name.ToLower().Contains("event"))
                {
                    FieldInfo field;  // Yes, for reals - how very special. http://stackoverflow.com/a/27413251/492
                    if (windowsControl is CheckBox && (fieldName.Contains("EVENT_CHECKEDCHANGED") || fieldName.Contains("EVENT_CHECKSTATECHANGED")))
                        field = typeof(CheckBox).GetField(fieldInfo.Name, BindingFlags.NonPublic | BindingFlags.Static |
                                                                          BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    else
                        field = typeof(T).GetField(fieldInfo.Name, BindingFlags.NonPublic | BindingFlags.Static |
                                                                   BindingFlags.Instance | BindingFlags.FlattenHierarchy);


                    if (field != null)
                    {
                        // if (field.IsStatic)
                        object key = field.IsStatic ? fieldInfo.GetValue(null) : field.GetValue(windowsControl);

                        if (key != null)
                        {
                            Delegate eventHandlers = eventsHandlersList[key];

                            if (eventHandlers != null)
                            {

                                var eventsList = new HashSet<Tuple<string, string>>();

                                foreach (Delegate eventHandler in eventHandlers.GetInvocationList())
                                {
                                    MethodInfo method = eventHandler.Method;
                                    if (method != null) // && method.DeclaringType != null)
                                    {
                                        string winFormsEventName = fieldInfo.Name;
                                        string methodName = method.Name;

                                        // internal name is not consistent. eg {"EventEnabled", "EnabledChanged"}, ("EventMarginChanged", "MarginChanged"}
                                        string winFormsEventExternalName = winFormsEventName.Replace("Event", "").Replace("EVENT", "").Replace("_", "");
                                        List<string> eventTranslations = eventsTranslator[winFormsEventExternalName].ToList();
                                        eventTranslations.AddRange(
                                          eventsTranslator[winFormsEventExternalName.Replace("Changed", "").Replace("CHANGED", "")].ToList());
                                        eventTranslations.AddRange(eventsTranslator[winFormsEventExternalName + "Changed"].ToList());
                                        eventTranslations.AddRange(eventsTranslator[winFormsEventExternalName + "CHANGED"].ToList());

                                        if (!eventTranslations.Any()) { eventTranslations.Add("WINDOWS.FORMS___" + winFormsEventName); }

                                        foreach (string translatedWpfEventName in eventTranslations)
                                        {
                                            string cleanWpfEventName = translatedWpfEventName.Replace("WINDOWS.FORMS___", "");

                                            if (_regexOfEventsToInclude != null && Regex.IsMatch(cleanWpfEventName, _regexOfEventsToInclude))
                                            {
                                                eventsList.Add(new Tuple<string, string>(translatedWpfEventName, methodName));
                                                continue;
                                            }

                                            if (_eventsToExclude != null
                                                 && (_eventsToExclude.Contains(cleanWpfEventName) || _eventsToExclude.Contains(winFormsEventExternalName))) continue;

                                            if (_eventsToInclude != null
                                                 && !(_eventsToInclude.Contains(cleanWpfEventName) || _eventsToInclude.Contains(winFormsEventExternalName))) continue;

                                            eventsList.Add(new Tuple<string, string>(translatedWpfEventName, methodName));
                                        }
                                    }
                                }
                                // write events
                                foreach (var eventDescription in eventsList) WriteAttribute(eventDescription.Item1, eventDescription.Item2, "");
                            }
                        }
                    }
                }
            }
        }


        // =================== My Privates ===========================


        /// This is what we build the XAML with..
        private static StringBuilder wpfBuilder = new StringBuilder();



        /// Optional. Your <seealso cref="ToolTip"/> component. Provides the tooltips.
        private static ToolTip _toolTipProvider;

        /// XAML includes Everything in the Top parent container. Default is true
        private static bool _includeAllFromParentFormOrUserControl;

        /// True if you want all the <seealso cref="Font"/> and  <seealso cref="Color"/> 
        /// information for each and every control, rather than going with the overall defaults. Default is False.
        private static bool _doColorsAndFontsForEveryIndividualControl;

        /// True if you want the XAML in the Windows Clipboard to paste into an editor. Default is true
        private static bool _copyXamlToClipboard;

        /// True if you want the <see cref="System.Windows.Forms"/> Events translated into XAML. Default is true.
        private static bool _doEvents;

        /// List any <see cref="System.Windows.Forms"/> or WPF Events you don't want to see in XAML.
        private static string[] _eventsToExclude;

        /// List any <see cref="System.Windows.Forms"/> or WPF Events you DO want to see in XAML. All others are ignored.
        private static string[] _eventsToInclude;

        /// Now you have two problems. Trumps <see cref="_eventsToInclude"/>
        private static string _regexOfEventsToInclude;


        private struct WinFormsToWpfTranslator
        {
            internal WinFormsToWpfTranslator(string winName, string wpfName) : this()
            {
                winformsName = winName;
                this.wpfName = wpfName;
            }
            internal string winformsName { get; private set; }
            internal string wpfName { get; private set; }
        }


        private static Lookup<string, string> eventsTranslator
        {
            get {
                Lookup<string, string> conver = (Lookup<string, string>)eventsTxList.ToLookup(p => p.winformsName, p => p.wpfName);
                return conver;
            }
        }
        #region EventsTranslations
        private static List<WinFormsToWpfTranslator> eventsTxList = new List<WinFormsToWpfTranslator>
         {
           new WinFormsToWpfTranslator("Click"                       , "Click"                           ),
           new WinFormsToWpfTranslator("BindingContextChanged"       , "DataContextChanged"              ),
           new WinFormsToWpfTranslator("ContextMenuChanged"          , "ContextMenuClosing"              ),
           new WinFormsToWpfTranslator("ContextMenuChanged"          , "ContextMenuOpening"              ),
           new WinFormsToWpfTranslator("ContextMenuStripChanged"     , "ContextMenuClosing"              ),
           new WinFormsToWpfTranslator("ContextMenuStripChanged"     , "ContextMenuOpening"              ),
           new WinFormsToWpfTranslator("ControlRemoved"              , "Unloaded"                        ),
           new WinFormsToWpfTranslator("CursorChanged"               , "QueryCursor"                     ),
           new WinFormsToWpfTranslator("Disposed"                    , "Unloaded"                        ),
           new WinFormsToWpfTranslator("DockChanged"                 , "ManipulationCompleted"           ),
           new WinFormsToWpfTranslator("DoubleClick"                 , "MouseDoubleClick"                ),
           new WinFormsToWpfTranslator("DoubleClick"                 , "PreviewMouseDoubleClick"         ),
           new WinFormsToWpfTranslator("DragDrop"                    , "Drop"                            ),
           new WinFormsToWpfTranslator("DragDrop"                    , "PreviewDrop"                     ),
           new WinFormsToWpfTranslator("DragEnter"                   , "DragEnter"                       ),
           new WinFormsToWpfTranslator("DragEnter"                   , "PreviewDragEnter"                ),
           new WinFormsToWpfTranslator("DragLeave"                   , "DragLeave"                       ),
           new WinFormsToWpfTranslator("DragLeave"                   , "PreviewDragLeave"                ),
           new WinFormsToWpfTranslator("DragOver"                    , "DragOver"                        ),
           new WinFormsToWpfTranslator("DragOver"                    , "PreviewDragOver"                 ),
           new WinFormsToWpfTranslator("EnabledChanged"              , "IsEnabledChanged"                ),
           new WinFormsToWpfTranslator("Enter"                       , "PreviewStylusInRange"            ),
           new WinFormsToWpfTranslator("Enter"                       , "StylusEnter"                     ),
           new WinFormsToWpfTranslator("Enter"                       , "StylusInRange"                   ),
           new WinFormsToWpfTranslator("Enter"                       , "TouchEnter"                      ),
           new WinFormsToWpfTranslator("GiveFeedback"                , "GiveFeedback"                    ),
           new WinFormsToWpfTranslator("GiveFeedback"                , "PreviewGiveFeedback"             ),
           new WinFormsToWpfTranslator("GotFocus"                    , "GotFocus"                        ),
           new WinFormsToWpfTranslator("GotFocus"                    , "GotKeyboardFocus"                ),
           new WinFormsToWpfTranslator("GotFocus"                    , "GotMouseCapture"                 ),
           new WinFormsToWpfTranslator("GotFocus"                    , "GotStylusCapture"                ),
           new WinFormsToWpfTranslator("GotFocus"                    , "GotTouchCapture"                 ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsKeyboardFocusedChanged"        ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsKeyboardFocusWithinChanged"    ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsMouseCapturedChanged"          ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsMouseCaptureWithinChanged"     ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsStylusCapturedChanged"         ),
           new WinFormsToWpfTranslator("GotFocus"                    , "IsStylusCaptureWithinChanged"    ),
           new WinFormsToWpfTranslator("GotFocus"                    , "PreviewGotKeyboardFocus"         ),
           new WinFormsToWpfTranslator("KeyDown"                     , "KeyDown"                         ),
           new WinFormsToWpfTranslator("KeyPress"                    , "KeyDown"                         ),
           new WinFormsToWpfTranslator("KeyUp"                       , "KeyUp"                           ),
           new WinFormsToWpfTranslator("KeyUp"                       , "PreviewKeyUp"                    ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsKeyboardFocusedChanged"        ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsKeyboardFocusWithinChanged"    ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsMouseCapturedChanged"          ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsMouseCaptureWithinChanged"     ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsStylusCapturedChanged"         ),
           new WinFormsToWpfTranslator("LostFocus"                   , "IsStylusCaptureWithinChanged"    ),
           new WinFormsToWpfTranslator("LostFocus"                   , "LostFocus"                       ),
           new WinFormsToWpfTranslator("LostFocus"                   , "LostKeyboardFocus"               ),
           new WinFormsToWpfTranslator("LostFocus"                   , "LostMouseCapture"                ),
           new WinFormsToWpfTranslator("LostFocus"                   , "LostStylusCapture"               ),
           new WinFormsToWpfTranslator("LostFocus"                   , "LostTouchCapture"                ),
           new WinFormsToWpfTranslator("LostFocus"                   , "PreviewLostKeyboardFocus"        ),
           new WinFormsToWpfTranslator("MarginChanged"               , "ManipulationCompleted"           ),
           new WinFormsToWpfTranslator("MouseCaptureChanged"         , "StylusOutOfRange"                ),
           new WinFormsToWpfTranslator("MouseClick"                  , "Click"                           ),
           new WinFormsToWpfTranslator("MouseClick"                  , "MouseDown"                       ),
           new WinFormsToWpfTranslator("MouseDoubleClick"            , "MouseDoubleClick"                ),
           new WinFormsToWpfTranslator("MouseDoubleClick"            , "PreviewMouseDoubleClick"         ),
           new WinFormsToWpfTranslator("MouseDown"                   , "MouseDown"                       ),
           new WinFormsToWpfTranslator("MouseDown"                   , "MouseLeftButtonDown"             ),
           new WinFormsToWpfTranslator("MouseDown"                   , "MouseRightButtonDown"            ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewMouseDown"                ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewMouseLeftButtonDown"      ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewMouseRightButtonDown"     ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewStylusButtonDown"         ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewStylusDown"               ),
           new WinFormsToWpfTranslator("MouseDown"                   , "PreviewTouchDown"                ),
           new WinFormsToWpfTranslator("MouseDown"                   , "StylusButtonDown"                ),
           new WinFormsToWpfTranslator("MouseDown"                   , "StylusDown"                      ),
           new WinFormsToWpfTranslator("MouseDown"                   , "TouchDown"                       ),
           new WinFormsToWpfTranslator("MouseEnter"                  , "MouseEnter"                      ),
           new WinFormsToWpfTranslator("MouseEnter"                  , "PreviewStylusInRange"            ),
           new WinFormsToWpfTranslator("MouseEnter"                  , "StylusEnter"                     ),
           new WinFormsToWpfTranslator("MouseEnter"                  , "StylusInRange"                   ),
           new WinFormsToWpfTranslator("MouseEnter"                  , "TouchEnter"                      ),
           new WinFormsToWpfTranslator("MouseHover"                  , "IsMouseDirectlyOverChanged"      ),
           new WinFormsToWpfTranslator("MouseHover"                  , "IsStylusDirectlyOverChanged"     ),
           new WinFormsToWpfTranslator("MouseHover"                  , "PreviewStylusInAirMove"          ),
           new WinFormsToWpfTranslator("MouseLeave"                  , "MouseLeave"                      ),
           new WinFormsToWpfTranslator("MouseLeave"                  , "PreviewStylusOutOfRange"         ),
           new WinFormsToWpfTranslator("MouseLeave"                  , "StylusLeave"                     ),
           new WinFormsToWpfTranslator("MouseLeave"                  , "TouchLeave"                      ),
           new WinFormsToWpfTranslator("MouseMove"                   , "MouseMove"                       ),
           new WinFormsToWpfTranslator("MouseMove"                   , "PreviewMouseMove"                ),
           new WinFormsToWpfTranslator("MouseMove"                   , "PreviewStylusMove"               ),
           new WinFormsToWpfTranslator("MouseMove"                   , "PreviewTouchMove"                ),
           new WinFormsToWpfTranslator("MouseMove"                   , "StylusInAirMove"                 ),
           new WinFormsToWpfTranslator("MouseMove"                   , "StylusMove"                      ),
           new WinFormsToWpfTranslator("MouseMove"                   , "TouchMove"                       ),
           new WinFormsToWpfTranslator("MouseUp"                     , "MouseLeftButtonUp"               ),
           new WinFormsToWpfTranslator("MouseUp"                     , "MouseRightButtonUp"              ),
           new WinFormsToWpfTranslator("MouseUp"                     , "MouseUp"                         ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewMouseLeftButtonUp"        ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewMouseRightButtonUp"       ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewMouseUp"                  ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewStylusButtonUp"           ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewStylusUp"                 ),
           new WinFormsToWpfTranslator("MouseUp"                     , "PreviewTouchUp"                  ),
           new WinFormsToWpfTranslator("MouseUp"                     , "StylusButtonUp"                  ),
           new WinFormsToWpfTranslator("MouseUp"                     , "StylusUp"                        ),
           new WinFormsToWpfTranslator("MouseUp"                     , "TouchUp"                         ),
           new WinFormsToWpfTranslator("MouseWheel"                  , "MouseWheel"                      ),
           new WinFormsToWpfTranslator("MouseWheel"                  , "PreviewMouseWheel"               ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationBoundaryFeedback"    ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationCompleted"           ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationDelta"               ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationInertiaStarting"     ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationStarted"             ),
           new WinFormsToWpfTranslator("Move"                        , "ManipulationStarting"            ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationBoundaryFeedback"    ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationCompleted"           ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationDelta"               ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationInertiaStarting"     ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationStarted"             ),
           new WinFormsToWpfTranslator("PaddingChanged"              , "ManipulationStarting"            ),
           new WinFormsToWpfTranslator("ParentChanged"               , "ManipulationCompleted"           ),
           new WinFormsToWpfTranslator("PreviewKeyDown"              , "PreviewKeyDown"                  ),
           new WinFormsToWpfTranslator("QueryContinueDrag"           , "PreviewQueryContinueDrag"        ),
           new WinFormsToWpfTranslator("QueryContinueDrag"           , "QueryContinueDrag"               ),
           new WinFormsToWpfTranslator("Resize"                      , "SizeChanged"                     ),
           new WinFormsToWpfTranslator("SizeChanged"                 , "SizeChanged"                     ),
           new WinFormsToWpfTranslator("TextChanged"                 , "PreviewTextInput"                ),
           new WinFormsToWpfTranslator("TextChanged"                 , "SourceUpdated"                   ),
           new WinFormsToWpfTranslator("TextChanged"                 , "TargetUpdated"                   ),
           new WinFormsToWpfTranslator("TextChanged"                 , "TextInput"                       ),
           new WinFormsToWpfTranslator("VisibleChanged"              , "IsVisibleChanged"                ),
           new WinFormsToWpfTranslator("BackColorChanged"            , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("BackgroundImageChanged"      , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("BackgroundImageLayoutChanged", "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("ClientSizeChanged"           , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("FontChanged"                 , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("ForeColorChanged"            , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("Invalidated"                 , "Layout"                          ),
           new WinFormsToWpfTranslator("Leave"                       , "MouseLeave"                      ),
           new WinFormsToWpfTranslator("Leave"                       , "PreviewStylusOutOfRange"         ),
           new WinFormsToWpfTranslator("Leave"                       , "StylusLeave"                     ),
           new WinFormsToWpfTranslator("Leave"                       , "TouchLeave"                      ),
           new WinFormsToWpfTranslator("LocationChanged"             , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("Paint"                       , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("RightToLeftChanged"          , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("StyleChanged"                , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("SystemColorsChanged"         , "LayoutUpdated"                   ),
           new WinFormsToWpfTranslator("HandleCreated"               , "RequestBringIntoView"            ),
           new WinFormsToWpfTranslator("HandleDestroyed"             , "Unloaded"                        ),
           new WinFormsToWpfTranslator("AutoSizeChanged"             , "NotInWpf_AutoSizeChanged"        ),
           new WinFormsToWpfTranslator("CausesValidationChanged"     , "NotInWpf_CausesValidationChanged"),
           new WinFormsToWpfTranslator("ChangeUICues"                , "NotInWpf_ChangeUICues"           ),
           new WinFormsToWpfTranslator("HelpRequested"               , "NotInWpf_HelpRequested"          ),
           new WinFormsToWpfTranslator("ImeModeChanged"              , "NotInWpf_ImeModeChanged"         ),
           new WinFormsToWpfTranslator("QueryAccessibilityHelp"      , "NotInWpf_QueryAccessibilityHelp" ),
           new WinFormsToWpfTranslator("RegionChanged"               , "NotInWpf_RegionChanged"          ),
           new WinFormsToWpfTranslator("TabIndexChanged"             , "NotInWpf_TabIndexChanged"        ),
           new WinFormsToWpfTranslator("TabStopChanged"              , "NotInWpf_TabStopChanged"         ),
           new WinFormsToWpfTranslator("Validated"                   , "NotInWpf_Validated"              ),
           new WinFormsToWpfTranslator("Validating"                  , "NotInWpf_Validating"             ),
           new WinFormsToWpfTranslator("ControlAdded"                , "NotInWpf_ControlAdded"           ),
           new WinFormsToWpfTranslator("CheckStateChanged"           , "Checked"                         ),
           new WinFormsToWpfTranslator("CheckStateChanged"           , "Unchecked"                       ),
           new WinFormsToWpfTranslator("CheckedChanged"              , "Checked"                         ),
           new WinFormsToWpfTranslator("CheckedChanged"              , "Unchecked"                       ),
           new WinFormsToWpfTranslator("CHECKSTATECHANGED"           , "Checked"                         ),
           new WinFormsToWpfTranslator("CHECKSTATECHANGED"           , "Unchecked"                       ),
           new WinFormsToWpfTranslator("CHECKEDCHANGED"              , "Checked"                         ),
           new WinFormsToWpfTranslator("CHECKEDCHANGED"              , "Unchecked"                       ),
           new WinFormsToWpfTranslator("AppearanceChanged"           , "NotInWpf_AppearanceChanged"      ),
           new WinFormsToWpfTranslator("MouseDoubleClick"            , "MouseDoubleClick"                ),
         };
        #endregion



        private static Lookup<string, string> controlsTranslator
        {
            get
            {
                Lookup<string, string> conver = (Lookup<string, string>)controlsTxList.ToLookup(p => p.winformsName, p => p.wpfName);
                return conver;
            }
        }

        #region ControlsTypeTranslations
        private static List<WinFormsToWpfTranslator> controlsTxList = new List<WinFormsToWpfTranslator>
         {
           new WinFormsToWpfTranslator("Button"                      , "Button"                               ),
           new WinFormsToWpfTranslator("MonthCalendar"               , "Calendar"                             ),
           new WinFormsToWpfTranslator("Panel"                       , "StackPanel"                           ),
           new WinFormsToWpfTranslator("CheckBox"                    , "CheckBox"                             ),
           new WinFormsToWpfTranslator("BindingSource"               , "CollectionViewSource"                 ),
           new WinFormsToWpfTranslator("ComboBox"                    , "ComboBox"                             ),
           new WinFormsToWpfTranslator("ContextMenuStrip"            , "ContextMenu"                          ),
           new WinFormsToWpfTranslator("DataGridView"                , "DataGrid"                             ),
           new WinFormsToWpfTranslator("DateTimePicker"              , "DatePicker"                           ),
           new WinFormsToWpfTranslator("Timer"                       , "DispatcherTimer"                      ),
           new WinFormsToWpfTranslator("PrintPreviewControl"         , "DocumentViewer"                       ),
           new WinFormsToWpfTranslator("WebBrowser"                  , "WebBrowser"                           ),
           new WinFormsToWpfTranslator("TableLayoutPanel"            , "Grid"                                 ),
           new WinFormsToWpfTranslator("SplitContainer"              , "GridSplitter"                         ),
           new WinFormsToWpfTranslator("GroupBox"                    , "GroupBox"                             ),
           new WinFormsToWpfTranslator("PictureBox"                  , "Image"                                ),
           new WinFormsToWpfTranslator("Label"                       , "Label"                                ),
           new WinFormsToWpfTranslator("ListBox"                     , "ListBox"                              ),
           new WinFormsToWpfTranslator("CheckedListBox"              , "ListBox with composition"             ),
           new WinFormsToWpfTranslator("ListView"                    , "ListView"                             ),
           new WinFormsToWpfTranslator("SoundPlayer"                 , "MediaPlayer"                          ),
           new WinFormsToWpfTranslator("MenuStrip"                   , "Menu"                                 ),
           new WinFormsToWpfTranslator("BindingNavigator"            , "Winforms__BindingNavigator"           ),
           new WinFormsToWpfTranslator("ColorDialog"                 , "Winforms__ColorDialog"                ),
           new WinFormsToWpfTranslator("ErrorProvider"               , "Winforms__ErrorProvider"              ),
           new WinFormsToWpfTranslator("FolderBrowserDialog"         , "Winforms__FolderBrowserDialog"        ),
           new WinFormsToWpfTranslator("FontDialog"                  , "Winforms__FontDialog"                 ),
           new WinFormsToWpfTranslator("HelpProvider"                , "ToolTip"                              ),
           new WinFormsToWpfTranslator("ImageList"                   , "Winforms__ImageList"                  ),
           new WinFormsToWpfTranslator("LinkLabel"                   , "Hyperlink"                            ),
           new WinFormsToWpfTranslator("MaskedTextBox"               , "Winforms__MaskedTextBox"              ),
           new WinFormsToWpfTranslator("NotifyIcon"                  , "Winforms__NotifyIcon"                 ),
           new WinFormsToWpfTranslator("PageSetupDialog"             , "Winforms__PageSetupDialog"            ),
           new WinFormsToWpfTranslator("PrintDocument"               , "Winforms__PrintDocument"              ),
           new WinFormsToWpfTranslator("PrintPreviewDialog"          , "Winforms__PrintPreviewDialog"         ),
           new WinFormsToWpfTranslator("PropertyGrid"                , "Winforms__PropertyGrid"               ),
           new WinFormsToWpfTranslator("OpenFileDialog"              , "OpenFileDialog"                       ),
           new WinFormsToWpfTranslator("PrintDialog"                 , "PrintDialog"                          ),
           new WinFormsToWpfTranslator("ProgressBar"                 , "ProgressBar"                          ),
           new WinFormsToWpfTranslator("RadioButton"                 , "RadioButton"                          ),
           new WinFormsToWpfTranslator("RichTextBox"                 , "RichTextBox"                          ),
           new WinFormsToWpfTranslator("SaveFileDialog"              , "SaveFileDialog"                       ),
           new WinFormsToWpfTranslator("HScrollBar"                  , "ScrollBar"                            ),
           new WinFormsToWpfTranslator("VScrollBar"                  , "ScrollBar"                            ),
           new WinFormsToWpfTranslator("ScrollableControl"           , "ScrollViewer"                         ),
           new WinFormsToWpfTranslator("TrackBar"                    , "Slider"                               ),
           new WinFormsToWpfTranslator("StatusStrip"                 , "StatusBar"                            ),
           new WinFormsToWpfTranslator("TabControl"                  , "TabControl"                           ),
           new WinFormsToWpfTranslator("TextBox"                     , "TextBox"                              ),
           new WinFormsToWpfTranslator("DomainUpDown"                , "TextBox and two RepeatButton controls"),
           new WinFormsToWpfTranslator("NumericUpDown"               , "TextBox and two RepeatButton controls"),
           new WinFormsToWpfTranslator("ToolStrip"                   , "ToolBar"                              ),
           new WinFormsToWpfTranslator("ToolStripContainer"          , "ToolBar with composition"             ),
           new WinFormsToWpfTranslator("ToolStripDropDown"           , "ToolBar with composition"             ),
           new WinFormsToWpfTranslator("ToolStripDropDownMenu"       , "ToolBar with composition"             ),
           new WinFormsToWpfTranslator("ToolStripPanel"              , "ToolBar with composition"             ),
           new WinFormsToWpfTranslator("ToolTip"                     , "ToolTip"                              ),
           new WinFormsToWpfTranslator("TreeView"                    , "TreeView"                             ),
           new WinFormsToWpfTranslator("UserControl"                 , "UserControl"                          ),
           new WinFormsToWpfTranslator("Form"                        , "Window"                               ),
           new WinFormsToWpfTranslator("FlowLayoutPanel"             , "WrapPanel or StackPanel"              ),
           new WinFormsToWpfTranslator("DBLabel"                     , "Label"                                ),
           new WinFormsToWpfTranslator("DBControl"                   , "Label"                                ),
           new WinFormsToWpfTranslator("DBCombo"                     , "ComboBox"                             ),
           new WinFormsToWpfTranslator("DBButton"                    , "Button"                               ),
           new WinFormsToWpfTranslator("DBGrid"                      , "DataGrid"                             ),
           new WinFormsToWpfTranslator("DBGridView"                  , "DataGrid"                             ),
           new WinFormsToWpfTranslator("DBTextBox"                   , "TextBox"                              ),
           new WinFormsToWpfTranslator("DBPicture"                   , "Image"                                ),
           new WinFormsToWpfTranslator("DBStatusBar"                 , "StatusBar"                            ),
           new WinFormsToWpfTranslator("DBToolBar"                   , "ToolBar"                              ),
           new WinFormsToWpfTranslator("DBToolBarEx"                 , "ToolBar"                              )
         };
        #endregion


    }
}

#endif