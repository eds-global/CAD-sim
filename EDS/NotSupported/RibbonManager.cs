using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ZwSoft.ZwCAD.Windows;


namespace EDS
{
    /**
*  This a Ribbon Helper class. 
*  Creating a button with tab panel in autoCAD and convert the jpg image in BitmapImage       
* */
    public class RibbonManager
    {
        /// <summary>
        /// Method is used for getting BitmapImage with pixel via bitmap[Bitmap], height[int], width[int].
        /// <param name = "bitmap"> bitmap[Bitmap] </param>
        /// <param name = "height"> height[int] </param>
        /// <param name = "width"> width[int] </param>
        /// </summary>
        /// <returns>
        /// BitmapImage = used to add image on button
        /// </returns>
        BitmapImage getBitmap(Bitmap bitmap, int height, int width)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(stream.ToArray());
            bmp.DecodePixelHeight = height;
            bmp.DecodePixelWidth = width;
            bmp.EndInit();
            return bmp;
        }


        /// <summary>
        /// Method is used to creating ribbon panel with button.   
        /// </summary>
        public void createRibbon()
        {
            //RibbonControl rbnCntrl = ComponentManager.Ribbon;
            //RibbonTab rbnTab = new RibbonTab();
            //rbnTab.Title = "EDS Group";
            //rbnTab.Id = "EDSQWSAWSAA_Id";
            //rbnCntrl.Tabs.Add(rbnTab);

            //// Add ribbon panel source
            //RibbonPanelSource rbnPnlSrc = new RibbonPanelSource();
            //rbnPnlSrc.Title = " ";

            //// Add custom ribbon panel
            //RibbonPanel rbnPnl = new RibbonPanel();
            //rbnPnl.Source = rbnPnlSrc;
            //rbnTab.Panels.Add(rbnPnl);
            //rbnTab.IsActive = true;

            //////Export button     
            //Autodesk.Windows.RibbonButton mrkngButton = new RibbonButton();
            //string exportTooltip = "";
            //string exportBtnText = "\nExport";
            //string exportBtnCommand = "EEXXPPOORRTT ";
            //Bitmap exportBitmap = Properties.Resources.Export;
            //createButton(rbnPnlSrc, mrkngButton, exportTooltip, exportBtnText, exportBtnCommand, exportBitmap);
        }

        ///<summary>
        /// Method is used to assign multiple properties of button.
        ///</summary>
        ///<param name = "rbnPnlSource">A RibbonPanelSource is used to stored a button</param>
        ///<param name = "button">A RibbonButton is used to assign properties</param>
        ///<param name = "toolTip">A string used to assign tooltip in button</param>
        ///<param name = "bttnText">A string used to assign buttonText in button</param>
        ///<param name = "command">A string used to assign btton click event perform</param>
        ///<param name = "bitmap">A Bitmap is used to assign image in button</param>
        //private void createButton(RibbonPanelSource rbnPnlSource, RibbonButton button, string toolTip, string bttnText, string command, Bitmap bitmap)
        //{
        //    //button.ToolTip = toolTip;
        //    //button.Text = bttnText;
        //    //button.CommandParameter = command;
        //    //button.Size = RibbonItemSize.Large;
        //    //button.Orientation = System.Windows.Controls.Orientation.Vertical;
        //    //button.ShowText = true;
        //    //button.ShowImage = true;
        //    //button.Image = getBitmap(bitmap, 16, 16);
        //    //button.LargeImage = getBitmap(bitmap, 32, 32);
        //    //button.CommandHandler = new AdskCommandHandler();

        //    ////Add the buttons are in ribbon panel source
        //    rbnPnlSource.Items.Add(button);
        //}
    }

    /**
*  This a Command Handler class. 
*  Creating the method of button event handler.     
* */
    internal class AdskCommandHandler : ICommand
    {
        // default interface method implemented here
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //RibbonButton rbnBtn = parameter as RibbonButton;
            //if (rbnBtn != null)
            //{
            //    Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute((String)rbnBtn.CommandParameter, true, false, true);
            //}
        }
    }
}
