using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageHooks.Models;
using System.Net;
using AutoRobo.WebApi.Entities;
using AutoRobo.DataMapping;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageServices
{
    public class CustomIdentity : ICustomIdentity
    {
        private MockUser mockUser;
        /// <summary>
        /// 客户端验证登录传入的值，
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            set;
        }

        public MockUser MockUser {
            get { return mockUser; }
            set { 
                mockUser = value;
                SecurityDataMap.Instance.UpdateValues(mockUser.UserName,
                mockUser.Password,
                mockUser.Email,
                mockUser.Tel,
                mockUser.Mobile,
                mockUser.CompanyName,
                mockUser.Description);
            }
        }
        /// <summary>
        /// 客户端验证登录传入的值， 通过该值获取MockUser对象
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端验证登录传入的值，
        /// </summary>
        public string Ticket
        {
            get;
            set;
        }
    }
}
