using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using AutoRobo.WebApi.Entities;

namespace AutoRobo.Core
{

    public interface ICustomIdentity
    {
        bool IsAuthenticated
        {
            get;
            set;
        }

        MockUser MockUser { get; set; }

        string UserId { get; set; }
  
      
        /// <summary>
        /// cookie value
        /// </summary>
        string Ticket { get; set; }
    }
}
