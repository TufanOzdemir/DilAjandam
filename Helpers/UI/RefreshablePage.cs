using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Helpers.UI
{
    public abstract class RefreshablePage : ContentPage, IRefreshablePage
    {
        public abstract void Refresh();
    }
}
