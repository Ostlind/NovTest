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

    public class download_bobbin_order_to_fls : BaseWebAiiTest
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
        
    
    
        [CodedStep(@"Download Bobbin Order")]
        public async void DownloadBobbinOrder()
        {
           // string bobbinOrderName = Data["Name"].ToString();
            
            string bobbinOrderName = GetExtractedValue("bobbinOrderName").ToString();
            
            Log.WriteLine(string.Format("Downloading bobbin order: {0}", bobbinOrderName));

            var content = await Helper.DownloadBobbinOrder(bobbinOrderName);
        
        }
    
        [CodedStep(@"Download Bobbin Order Test")]
        public async void DownloadBobbinOrderTest()
        {
            
            string bobbinOrderName = "1731006-160";
            
            Log.WriteLine(string.Format("Downloading bobbin order: {0}", bobbinOrderName));

           string content = await Helper.DownloadBobbinOrder(bobbinOrderName);
        
        }
    }
}
