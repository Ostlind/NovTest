using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
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

    public class login_rewinder : BaseWebAiiTest
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
    
        [CodedStep(@"Click 'x7102FS416PID416Span'")]
        public void loginrewinder_CodedStep()
        {
            // Click 'x7102FS416PID416Span'
            Pages.PressureRewindingOverview.x7102FS416PID416Span.Click(false);
            
        }
    
        [CodedStep(@"Wait for Exists 'Div'")]
        public void loginrewinder_CodedStep1()
        {
            // Wait for Exists 'Div'
            Pages.PressureRewindingOverview.Div.Wait.ForExists(30000);
            
        }
    
        [CodedStep(@"Verify element 'Div' 'is not' visible.")]
        public void loginrewinder_CodedStep2()
        {
            // Verify element 'Div' 'is not' visible.
            Pages.PressureRewindingOverview.Div0.Wait.ForExists(30000);
            Assert.AreEqual(false, Pages.PressureRewindingOverview.Div0.IsVisible());
            
        }
    
        [CodedStep(@"Wait for element 'Div' 'is not' visible.")]
        public void loginrewinder_CodedStep3()
        {
            // Wait for element 'Div2' 'is not' visible.
            bool expextedValue = false;
            Pages.PressureRewindingOverview.Div2.Wait.ForCondition((a_0, a_1) => (((ArtOfTest.WebAii.Controls.HtmlControls.HtmlDiv)(a_0)).IsVisible() == expextedValue), false, null, 30000);
            
        }
    
        [CodedStep(@"Click 'x7102FS416PID416Span'")]
        public void loginrewinder_CodedStep4()
        {
            // Click 'x7102FS416PID416Span'
            Pages.PressureRewindingOverview.x7102FS416PID416Span.Click(false);
            
        }
    
        [CodedStep(@"Wait for element 'backdrop modal' 'is not' visible.")]
        public void loginrewinder_CodedStep5()
        {
            // Wait for element 'backdrop modal' 'is not' visible.
            bool expextedValue = false;
            

         // HtmlDiv e = Find.ByAttributes("class=modal-backdrop fade in");
            
          //  e.Wait.ForCondition((a_0, a_1) => (((ArtOfTest.WebAii.Controls.HtmlControls.HtmlDiv)(a_0)).IsVisible() == expextedValue), false, null, 30000);
            
        }
    }
}
