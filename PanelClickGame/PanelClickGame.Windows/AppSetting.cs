using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PanelClickGame
{
    /// <summary>
    /// store application setting
    /// </summary>
    /// <remarks>
    /// this class is singletone.
    /// </remarks>
    class AppSetting
    {
        private static AppSetting _instance;

        private AppSetting()
        {
            this.SettingChanged += new EventHandler((s, e) => { });
            //default grid width
            if (ApplicationData.Current.LocalSettings.Values["GridWidth"] == null)
                ApplicationData.Current.LocalSettings.Values["GridWidth"] = 4;
        }

        /// <summary>
        /// return instance
        /// </summary>
        public static AppSetting Current
        {
            get
            {
                if (_instance == null)
                    _instance = new AppSetting();
                return _instance;
            }
        }

        public event EventHandler SettingChanged;

        /// <summary>
        /// return grid width or set grid width.
        /// </summary>
        /// <remarks>
        /// equals grid height and width.
        /// </remarks>
        public int GridWidth
        {
            get
            {
                return (int)ApplicationData.Current.LocalSettings.Values["GridWidth"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["GridWidth"] = value;
                this.SettingChanged(this, null);
            }
        }

        /// <summary>
        /// return grid height
        /// </summary>
        public int GridHeight
        {
            get
            {
                return (int)ApplicationData.Current.LocalSettings.Values["GridWidth"];
            }
        }
    }
}
