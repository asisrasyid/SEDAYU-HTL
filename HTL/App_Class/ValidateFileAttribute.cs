using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DusColl
{
    public class ValidateFileAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {

            int maxcontentlenght = 1024 * 1024 * 1;

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            else if (file.ContentLength > maxcontentlenght)
            {

                ErrorMessage = "Your File is too large, maximum allowed size is :" + (maxcontentlenght / 1024).ToString() + "MB";
                return false;
            }
            else
                return true;
        }
    }
}