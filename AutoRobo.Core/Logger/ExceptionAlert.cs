using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.Logger
{
    public class ExceptionAlert
    {
        private Exception exception;

        public ExceptionAlert(Exception ex) {
            this.exception = ex;
        }

        public void Show() {
            var frm = new frmException
            {
                lblError = { Text = exception.Message },
                rtbStack = { Text = exception.StackTrace }
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var email = new EmailException();
                string strAddress = frm.txtEmail.Text.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(strAddress.ToUpper(), @"^[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$"))
                {
                    strAddress = "";
                }
                email.SendMail(exception, strAddress, frm.txtComments.Text, frm.chkCopy.Checked, false);
            }
        }
    }
}
