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

    public class register_destination : BaseWebAiiTest
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
    
        [CodedStep(@"Set Environment Variables Test")]
        public void SetEnvironmentVariablesTest()
        {
           var bobbin = Helper.GetBobbinsByBobbinOrderName("1731006-142").FirstOrDefault();
            Log.WriteLine(bobbin.Name);
          SetExtractedValue("bobbinName", bobbin.Name);
            
             // SetExtractedValue("bobbinLength", bobbin.len)
        }
    
        

    
        [CodedStep(@"Set Environment Variables")]
        public void SetEnvironmentVariables()
        {
            
            var bobbin = GetExtractedValue("currentBobbin") as Bobbin;
            
            SetExtractedValue("bobbinName", bobbin.Name);
        }
    }
}
