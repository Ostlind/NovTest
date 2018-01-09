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

    public class execute_test_manually : BaseWebAiiTest
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
        public void execute_test_manually_CodedStep()
        {
                var bobbins = Helper.GetBobbinsByBobbinOrderName(Data["Name"].ToString());
                foreach( var bobbin in bobbins )
                {
                    
                    var lots = Helper.GetLotsByBobbinName(bobbin.Name);
                    
                foreach(var lot in lots)
                {
                    var weldings = Helper.GetWeldings(lot.Name);
                    
                    foreach(var welding in weldings)
                    {
                        SetExtractedValue("currentWelding", welding);    
                    }
                    
                }
                    
                    
                    
                    
                    SetExtractedValue("currentBobbin", bobbin);
                    SetExtractedValue("lots",);
                    SetExtractedValue("", );
                    SetExtractedValue("")
                    
                    Log.WriteLine("Name is now : " + bobbin.Name);
                    
                    this.ExecuteTest("only-for-testing\\register-source-multiple-times.tstest");
                }
                
                
                var weldings = Helper.GetWeldings()
                
                foreach()
        }
    }
}
