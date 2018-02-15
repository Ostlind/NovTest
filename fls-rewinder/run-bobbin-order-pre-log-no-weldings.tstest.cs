using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
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

    public class run_bobbin_order_pre_log_no_weldnings : BaseWebAiiTest
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
    
        [CodedStep(@"Set Bobbin Name Environment Variable")]
        public void SetBobbinNameEnvironmentVariable()
        {
                        
            var bobbinOrderName = Data["Name"].ToString();
            var bobbin = Helper.GetBobbinsByBobbinOrderName(bobbinOrderName).FirstOrDefault();
            
            SetExtractedValue("bobbinOrderName", bobbinOrderName);
            SetExtractedValue("currentBobbin", bobbin);
            SetExtractedValue("bobbinName", bobbin.Name);
            
        }
        
        [CodedStep(@"Process Lots")]
        public void ProcessLots()
        {

            var bobbinOrderName = Data["Name"].ToString();
            
            Helper.WriteLogAsync("##################################");
            
            Helper.WriteLogAsync(string.Format("Starting processing bobbin order: '{0}' ",bobbinOrderName));
       
            SetExtractedValue("bobbinOrderName", bobbinOrderName);

            var currentBobbin = Helper.GetBobbinsByBobbinOrderName(bobbinOrderName).FirstOrDefault();
            
            Helper.WriteLogAsync(string.Format("Using bobbin: '{0}'", currentBobbin.Name));
            
            SetExtractedValue("currentBobbin", currentBobbin);
            
            var lots = Helper.GetLotsByBobbinName(currentBobbin.Name).OrderBy( lot => lot.Id).ToList();
            
            Helper.WriteLogAsync(string.Format("Proccessing lots, number of lots :'{0}'", lots.Count));
            
            foreach( var lot in lots)
            {
                
                Helper.WriteLogAsync(string.Format("Starting proccessing lot; Name:'{0}', Bobbin Order:'{1}', Bobbin name:'{2}'", lot.Name, bobbinOrderName, currentBobbin.Name));
                
                Helper.WriteLogAsync(string.Format("Done proccessing lot; Name:'{0}', Bobbin Order:'{1}', Bobbin name:'{2}'", lot.Name, bobbinOrderName, currentBobbin.Name));
                
                SetExtractedValue("currentLot", lot);
                
            }
            
            Helper.WriteLogAsync(string.Format("Done proccessing lots"));

            Helper.WriteLogAsync(string.Format("Done proccessing bobbine order:'{0}'", bobbinOrderName ));
            
        }
    }
}
