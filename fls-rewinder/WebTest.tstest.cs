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
            //Iterate bobbins

              
            
        
            
        // 1. donwload order (get bobbinId)

        // 2. login fls
        // 3. start order
        // 4. open mom
        // 5. add destination bobbinId
            // Iterate lots
            
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
                
                var weldings = Helper.GetWeldings(lot.Name).Where(w => w.LotId == lot.Id).OrderBy(w => w.WeldingSequenceNumber).ToList();   
                
                foreach(var currentWelding in weldings)
                {
                    Log.WriteLine("Processing welding: " + currentWelding.Name);
                    
                    SetExtractedValue("currentWelding", currentWelding);

                    SetExtractedValue("weldingName", currentWelding.Name);

                  
                
                    if(currentWelding.IsFirstWelding.Value && currentWelding.WeldingType == "WA")
                    {
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\register-wa-flash-welding.tstest");
                        
                        continue;
                        
                    }
                             
                   if(currentWelding.WeldingType == "CS")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-CS-welding-test.tstest");
                                 
                        
                        this.ExecuteTest("fls-rewinder\\unregister-source-1");
                        
                        break;
                    }
                } 
                
                System.Threading.Thread.Sleep(1000);
                
            }
            
                //this.ExecuteStep("fls-rewinder\\unregister-source");
            
                
                // 6. add current source (lot) from list
                // 7. check bobbind reference, does this lot belong to right bobbin?
                // 8. load source, enter values
                // 9. iterate weldings 
                    // 10.check if welding has right lot id
                    // 11. execute current welding
                    // 12. finsih weldings
                    // 13. Repeat step 10 if there's more weldings
                
            // 11. unload lot (unload lot)
         
            // 10. repeat step 6 until lots are finished
         // 11. unload bobbin (destination)
         // 12. repeat step 1. until bobbins is finished 
         
            
            
            
            
        
            
        
            
          // 7. get all weldings
          // 8. 
        }
    
        [CodedStep(@"Verify 'TextContent' 'Contains' '076' on 'x076Div'")]
        public void WebTest_CodedStep1()
        {
            
         // var test =  Manager.ActiveBrowser.Find.ByContent(ArtOfTest.Common.StringCompareType.Contains, "076");
            // Verify 'TextContent' 'Contains' '076' on 'x076Div'
            //Pages.PressureRewindingOverviev.x076Div.AssertContent().TextContent(ArtOfTest.Common.StringCompareType.Contains, "076");
            
        }
    
        [CodedStep(@"Execute test 'start-bobbin-order'")]
        public void WebTest_CodedStep2()
        {
            // Execute test 'start-bobbin-order'
            this.ExecuteTest("fls-rewinder\\start-bobbin-order.tstest");
            
        }
    
        [CodedStep(@"Set BobbinOrder Complete Status")]
        public void SetBobbinOrderCompleteStatus()
        {
            var currentBobbinName = Data["Name"].ToString();
            Helper.SetBobbinOrderCompletedStatus(currentBobbinName, true);
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
