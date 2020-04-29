using Medicines_Company_Information.BaseFramework;
using System;


namespace Medicines_Company_Information.BaseFramework
{
    public abstract class BasePage
    {
        protected BasePage(ICustomDriver customDriver)
        {
            CustomDriver = customDriver;
        }

        public void NavigateTo()
        {
            CustomDriver.Navigate(RelativeUrl);
        }
        public abstract Uri RelativeUrl { get; }
        protected ICustomDriver CustomDriver { get; }

    }
}
