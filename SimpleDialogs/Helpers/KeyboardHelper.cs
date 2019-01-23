using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SimpleDialogs.Helpers
{
    public sealed class KeyboardHelper
    {
        private static KeyboardHelper _Instance;

        private readonly PropertyInfo _AlwaysShowFocusVisual;
        private readonly MethodInfo _ShowFocusVisual;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static KeyboardHelper()
        {
        }

        private KeyboardHelper()
        {
            var type = typeof(KeyboardNavigation);

            _AlwaysShowFocusVisual = type.GetProperty("AlwaysShowFocusVisual", BindingFlags.NonPublic | BindingFlags.Static);
            _ShowFocusVisual = type.GetMethod("ShowFocusVisual", BindingFlags.NonPublic | BindingFlags.Static);
        }

        /// <summary>
        /// Gets the KeyboardNavigationEx singleton instance.
        /// </summary>
        internal static KeyboardHelper Instance => _Instance ?? (_Instance = new KeyboardHelper());

        /// <summary>
        /// Shows the focus visual of the current focused UI element.
        /// Works only together with AlwaysShowFocusVisual property.
        /// </summary>
        internal void ShowFocusVisualInternal()
        {
            _ShowFocusVisual.Invoke(null, null);
        }

        internal bool AlwaysShowFocusVisualInternal
        {
            get { return (bool)_AlwaysShowFocusVisual.GetValue(null, null); }
            set { _AlwaysShowFocusVisual.SetValue(null, value, null); }
        }

        /// <summary>
        /// Focuses the specified element and shows the focus visual style.
        /// </summary>
        /// <param name="element">The element which will be focused.</param>
        public static void Focus(UIElement element)
        {
            element?.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var keybHack = KeyboardHelper.Instance;
                var oldValue = keybHack.AlwaysShowFocusVisualInternal;

                keybHack.AlwaysShowFocusVisualInternal = true;

                try
                {
                    Keyboard.Focus(element);
                    keybHack.ShowFocusVisualInternal();
                }
                finally
                {
                   keybHack.AlwaysShowFocusVisualInternal = oldValue;
                }
            }));
        }
    }
}