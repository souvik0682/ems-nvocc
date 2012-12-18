using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Utilities
{
   public static class ExtentionClass
    {
       public static string GetImageFileName(this System.Web.UI.WebControls.FileUpload imgUpload)
       {
           
           string[] SupportedExtention = { ".jpeg", ".jpg", ".bmp", ".png",".gif" };

           if (!SupportedExtention.Contains(System.IO.Path.GetExtension(imgUpload.PostedFile.FileName)))
               return "";
           else
               return System.IO.Path.GetFileName(imgUpload.PostedFile.FileName);
         
       }

       public static decimal TryParseBlankAsZero(string value) // this is not an extention
       {
           decimal returnVal=decimal.Zero;
           Decimal.TryParse(value, out returnVal);
            return returnVal;
       }
    }
}
