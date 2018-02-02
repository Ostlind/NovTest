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

    public class register_source : BaseWebAiiTest
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
    
        [CodedStep(@"Set Lot Variables")]
        public void SetLotVariables()
        {
            var currentLot = GetExtractedValue("currentLot") as Lot;
           
            Log.WriteLine(currentLot.Name);
            Log.WriteLine(currentLot.MaterialLeft.ToString());
            Log.WriteLine(currentLot.BatchNumber.ToString());
            Log.WriteLine(currentLot.CoilNumber.ToString());
            Log.WriteLine(currentLot.Length.ToString());
            Log.WriteLine(currentLot.Comment.ToString());
            
            
            
            SetExtractedValue("lotName", currentLot.Name);
            SetExtractedValue("materialLeft", currentLot.MaterialLeft);
            SetExtractedValue("batchNumber" , currentLot.BatchNumber);
            SetExtractedValue("coilNumber",currentLot.CoilNumber);
            SetExtractedValue("lotLength" , currentLot.Length);
            SetExtractedValue("comment" , currentLot.Comment);
            
        }
    
        [CodedStep(@"radwatermarktextbox: Type 'test-lot-02' into SearchTextBoxRadwatermarktextbox - DataDriven: [$(lotName)]")]
        public void EnterLotName()
        {
            
           var random = new Random().Next(1,12323).ToString();
            
            // radwatermarktextbox: Type 'test-lot-02' into SearchTextBoxRadwatermarktextbox
            Pages.FormFlexLineSystem.SilverlightApp.SearchTextBoxRadwatermarktextbox.SetText(true, random + ((string)(System.Convert.ChangeType(Data["lotName"], typeof(string)))), 10, 100, false, true);
            
        }
    }
}
