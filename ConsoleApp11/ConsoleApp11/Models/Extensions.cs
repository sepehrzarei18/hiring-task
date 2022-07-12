using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ConsoleApp11.Models
{
    public static class Extensions
    {
        public static string CorrectFormat(this double arg1) => String.Format("{0:00.0}", arg1);
    }
}
