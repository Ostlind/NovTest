using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

namespace Nov_Test
{

    public class register_flash_welding : BaseWebAiiTest
    {
        #region [ Dynamic Pages Reference ]

        private Pages _pages;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
        public Pages Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = new Pages(Manager.Current);
                }
                return _pages;
            }
        }

        #endregion

        // Add your test methods here...

        [CodedStep(@"Set Environment Variables")]
        public void SetEnvironmentVariables()
        {
            var currentWelding = GetExtractedValue("currenWelding") as Welding;

            SetExtractedValue("weldingType", currentWelding.WeldingType);
            SetExtractedValue("equipmentId", currentWelding.EquipmentId);

        }

        [CodedStep(@"Set Environment Variables Test")]
        public void SetEnvironmentVariablesTest()
        {


            string text = "WA - Fastgøring på spole";
            string equipmentId = "12345";

            SetExtractedValue("weldingName", text);
            SetExtractedValue("equipmentId", equipmentId);

        }

        [CodedStep("Select a listbox item by text")]
        public void SelectListItemByTextCodedStep()
        {
            //string itemToFind = GetExtractedValue("weldingName").ToString();
            
            string itemToFind = "CS - Coilskift";
            

            SilverlightApp app = ActiveBrowser.SilverlightApps()[0];
            RadComboBox myLB = app.Find.ByAutomationId<RadComboBox>("36366929");

            Log.WriteLine(myLB.Name);
            RadComboBoxItem item = FindListboxItemByText(itemToFind, myLB);
            Assert.IsNotNull(item, "\"" + itemToFind + "\" not found in the listbox.");
            item.User.Click();
        }


        private static RadComboBoxItem FindListboxItemByText(string itemToFind, RadComboBox myLB)
        {
            // We may have to scroll the list box to find the item we want, so get the scroll viewer attached to the listbox
            ScrollViewer scroller = myLB.Find.ByType<ScrollViewer>();
            IList<RadComboBoxItem> items;
            double scrollPos = 0;

            do
            {
                // Get all list box items currently contained in the visual tree
                items = myLB.Find.AllByType<RadComboBoxItem>();

                // Iterate through each list box item looking for the one we want
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Text.Equals(itemToFind))
                    {
                        // We found the one we want. Scroll it to the top (in case it's outside the listbox's viewport) then return it to the caller.
                        scroller.InvokeMethod("ScrollToVerticalOffset", i);
                        return items[i];
                    }
                }

                // We didn't find it, scroll down by a page worth of data to populate the VisualTree with the next page of data and try again.
                scrollPos += scroller.ViewportHeight;
                scroller.InvokeMethod("ScrollToVerticalOffset", scrollPos);
                myLB.Refresh(); // Refresh our cached copy of the listbox.
            }
            while (scrollPos <= scroller.ExtentHeight);

            // We scanned the entire list and didn't find the right item to select.
            return null;
        }

    }
}
