using Project.Data.Infrastructure;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Project.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        public bool ValidateToken(int USER_ID, string TOKEN)
        {
            if (USER_ID == 0 || string.IsNullOrEmpty(TOKEN))
            {
                return false;
            }
            else
            {
                var dbFactory = new DbFactory();
                using (var ctx = dbFactory.Init())
                {
                    var latestToken = ctx.USER_TOKENS.Where(t => t.TOKEN == TOKEN && t.USER_ID == USER_ID).OrderByDescending(t => t.EXPIRES_ON).FirstOrDefault();
                    return latestToken != null && latestToken.EXPIRES_ON > DateTime.Now;
                }
            }
        }
        public USER_TOKEN GenerateNewTokenForUser(int USER_ID)
        {
            //Delete previous tokens if exists
            var dbFactory = new DbFactory();
            using (var ctx = dbFactory.Init())
            {
                var tokens = ctx.USER_TOKENS.Where(x => x.USER_ID == USER_ID);
                ctx.USER_TOKENS.RemoveRange(tokens);
                ctx.SaveChanges();
            }
            var token = new USER_TOKEN()
            {
                TOKEN = Guid.NewGuid().ToString().Substring(0, 32),
                GRANTED_ON = DateTime.Now,
                EXPIRES_ON = DateTime.Now.AddDays(30),
                USER_ID = USER_ID
            };

            return token;
        }
    }
}