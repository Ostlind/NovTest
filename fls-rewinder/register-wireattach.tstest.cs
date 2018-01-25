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

    public class register_wireattach : BaseWebAiiTest
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
        public void registerwireattach_CodedStep()
        {
            //var currentLotName = GetExtractedValue("currentLotName") as string;
            //var currentLot = GetExtractedValue("currentLot") as Lot;
            //var welding = Helper.GetWeldings(currentLot.Name).FirstOrDefault();
            
         var welding = Helper.GetWeldings("kal-10").FirstOrDefault();
        if(welding.WeldingType == "WA")
        {
        
            SetExtractedValue("cutoutLength", welding.CutoutLength);
            this.ExecuteTest("fls-rewinder//register-wireattach-1.tstest");
                        
        }
        
        if(welding.WeldingType == "WA")
        {
        
            SetExtractedValue("cutoutLength", welding.CutoutLength);
            this.ExecuteTest("fls-rewinder//register-wireattach-1.tstest");
                        
        }
        
        SetExtractedValue("currentWelding" , welding);
        SetExtractedValue("weldingType", welding.WeldingType);
        SetExtractedValue("weldingName", welding.Name);
        SetExtractedValue("equipmentId", welding.EquipmentId );
            
        
            
            
        }
    }
}
