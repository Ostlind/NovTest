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
using System.Runtime.Serialization;


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
      
            var currentBobbin = GetExtractedValue("currentBobbin") as Bobbin;
            
            var lots = Helper.GetLotsByBobbinId(currentBobbin.Id).OrderBy( lot => lot.CoilNumber).ToList();
            
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
           
            var bobbinOrderId = int.Parse(Data["Id"].ToString());
            
            var bobbin = Helper.GetBobbinByBobbinOrderId(bobbinOrderId);

            if(bobbin == null)
            {
                throw new ArgumentNullException("could not find bobbin");
            }
            
            SetExtractedValue("bobbinOrderName", bobbinOrderName);
            
            SetExtractedValue("currentBobbin", bobbin);
            
            SetExtractedValue("bobbinName", bobbin.Name);
            
        }
 
    
        [CodedStep(@"Wait For Download")]
        public void WaitForDownload()
        {
            // Wait for '120000' msec.
            var currentIteration = this.Data.IterationIndex;
            var numberOfRows = this.ExecutionContext.DataSource.Rows.Count - 1;

            if(currentIteration != numberOfRows)
            {
                System.Threading.Thread.Sleep(120000);
            }
            
        }
    }
}
