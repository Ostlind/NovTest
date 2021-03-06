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

    public class run_rewinder : BaseWebAiiTest
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
                
                foreach(var welding in weldings)
                {
                    var currentWelding = welding;
                    
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
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-CS-welding.tstest");
                                 
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "WDG")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-ECG-welding.tstest");
                                 
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "WD")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-WD-welding.tstest");
                                 
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "ECG")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-ECG-welding.tstest");
                                 
                        continue;
                    }

                    if(currentWelding.WeldingType == "EC")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-EC-welding.tstest");
                                 
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "SS")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-SS-welding.tstest");
                                 
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "IO")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-IO-welding.tstest");
                        
                        continue;
                    }
                    
                    if(currentWelding.WeldingType == "WD")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-WD-welding.tstest");
                                 
                        continue;
                    }
                                        
                    if(currentWelding.WeldingType == "UW")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-UW-welding.tstest");
                        
                        continue;
                    }
                } 
                
                System.Threading.Thread.Sleep(1000);
                
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
