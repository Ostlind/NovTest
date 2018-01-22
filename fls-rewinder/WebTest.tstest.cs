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

    public class WebTest : BaseWebAiiTest
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
    
        [CodedStep(@"New Coded Step")]
        public void WebTest_CodedStep()
        {
            var bobbins = Helper.GetBobbinsByBobbinOrderName(Data["Name"].ToString());
        }
    
        [CodedStep(@"Verify 'TextContent' 'Contains' '076' on 'x076Div'")]
        public void WebTest_CodedStep1()
        {
            
         // var test =  Manager.ActiveBrowser.Find.ByContent(ArtOfTest.Common.StringCompareType.Contains, "076");
            // Verify 'TextContent' 'Contains' '076' on 'x076Div'
            //Pages.PressureRewindingOverviev.x076Div.AssertContent().TextContent(ArtOfTest.Common.StringCompareType.Contains, "076");
            
        }
    }
}
