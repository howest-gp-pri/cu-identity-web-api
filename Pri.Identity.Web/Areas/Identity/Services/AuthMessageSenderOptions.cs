using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Identity.Web.Areas.Identity.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendgridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
