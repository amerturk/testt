using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Signbook.Constants
{
    public static class Consts
    {
        /// <summary>
        /// App ID from https://dashboard.agora.io/
        /// </summary>
        public static string AgoraAPI
        {
            get
            {
                return  "3de12c09ad1642f19714c7fdeda479f0";
            }
            set { }
        }
        /// <summary>
        /// Temp token generated in https://dashboard.agora.io/ or Token from your API
        /// </summary>
        public static string Token
        {
            get
            {
                return null;
            }
        }
    }
}
