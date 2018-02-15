using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
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

    public class run_rewinder_no_weldings : BaseWebAiiTest
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
    
        [CodedStep(@"Process Lots")]
        public void ProcessLots()
        {
      
            var bobbinOrderName = Data["Name"].ToString();

            SetExtractedValue("bobbinOrderName", bobbinOrderName);

            var currentBobbin = Helper.GetBobbinsByBobbinOrderName(bobbinOrderName).FirstOrDefault();
            
            SetExtractedValue("currentBobbin", currentBobbin);
            
            var lots = Helper.GetLotsByBobbinName(currentBobbin.Name).OrderBy( lot => lot.Id).ToList();
            
            Log.WriteLine("lots count: " + lots.Count().ToString());
            
            foreach( var lot in lots)
            {
                
                SetExtractedValue("currentLot", lot);
                
                this.ExecuteTest("fls-rewinder\\register-source.tstest");
                
                System.Threading.Thread.Sleep(1000);
                
                this.ExecuteTest("fls-rewinder\\unregister-source-no-weldings.tstest");
                
            }
        }
    

    
        [CodedStep(@"Set BobbinOrder Complete Status")]
        public void SetBobbinOrderCompleteStatus()
        {
            var currentBobbinName = Data["Name"].ToString();
            Helper.SetBobbinOrderCompletedStatus(currentBobbinName, false);
        }
    
        [CodedStep(@"Set Environment variables")]
        public void SetEnvironmentVariables()
        {
            var currentBobbin = Helper.GetBobbinsByBobbinOrderName(Data["Name"].ToString()).FirstOrDefault();
            
            SetExtractedValue("bobbinName", currentBobbin.Name );
        }
    
        [CodedStep(@"Set Bobbin Name Environment Variable")]
        public void SetBobbinNameEnvironmentVariable()
        {
                        
            var bobbinOrderName = Data["Name"].ToString();
           var bobbin = Helper.GetBobbinsByBobbinOrderName(bobbinOrderName).FirstOrDefault();
            
            SetExtractedValue("bobbinOrderName", bobbinOrderName);
            SetExtractedValue("currentBobbin", bobbin);
            SetExtractedValue("bobbinName", bobbin.Name);
            
        }
 
    }
}
