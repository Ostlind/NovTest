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

    public class execute_lots_and_weldings : BaseWebAiiTest
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
        public void executelotsandweldings_CodedStep()
        {
      
              
            
        
            
        // 1. donwload order (get bobbinId)

        // 2. login fls
        // 3. start order
        // 4. open mom
        // 5. add destination bobbinId
            // Iterate lots
            
            var bobbinOrderName = "1731006-126";
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
              
                    SetExtractedValue("currentWelding", currentWelding);

                    SetExtractedValue("weldingName", currentWelding.Name);

                    
                    /*if(currentWelding.WeldingType == "CO")
                    {    
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\register-visual-test.tstest");
                   
                        this.ExecuteTest("fls-rewinder\\register-mpi.tstest");
                    
                        this.ExecuteTest("fls-rewinder\\finish-welding");
                        
                        continue;
                    }*/
           
                
                    if(currentWelding.IsFirstWelding.Value && currentWelding.WeldingType == "WA")
                    {
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\register-wa-flash-welding.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\register-visual-test.tstest");
                   
                        this.ExecuteTest("fls-rewinder\\register-mpi.tstest");
                    
                        this.ExecuteTest("fls-rewinder\\finish-welding");
                    }
           
                             
            /*        if(currentWelding.WeldingType == "CS")
                    {   
                        this.ExecuteTest("fls-rewinder\\register-cut-out.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\weldings\\register-CS-welding.tstest");
                                 
                        this.ExecuteTest("fls-rewinder\\register-visual-test.tstest");
                   
                        this.ExecuteTest("fls-rewinder\\register-mpi.tstest");
                    
                        this.ExecuteTest("fls-rewinder\\finish-welding.tstest");
                        
                        this.ExecuteTest("fls-rewinder\\unregister-source.tstest");
                        
                        break;
                    }
                */} 
                
                System.Threading.Thread.Sleep(1000);
             
             this.ExecuteTest("fls-rewinder\\unregister-source-1");
                
            }
    }

        [CodedStep(@"New Coded Step")]
        public void executelotsandweldings_CodedStep1()
        {
            
        }
    }
}