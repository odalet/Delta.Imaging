/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.CertXplorer.UI.Actions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class StandardActionAttribute : Attribute { }

    public class UpdatablePropertyAttribute : Attribute { }
}