using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Project.Model;
using Project.Data.Infrastructure;
using Project.WebApi.Models;
using Project.WebApi.Utility;

namespace Project.WebApi.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : BaseController
    {
        [AcceptVerbs("POST")]
        [Route("authenticateUser")]
        public async Task<string> AuthenticateUser([FromBody] USER model)
        {
            JSONViewModel data = new JSONViewModel();
            if (string.IsNullOrEmpty(model.USERNAME))
            {
                data.ResponseMessage = "Username is required";
                data.StatusCode = "101";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            if (string.IsNullOrEmpty(model.PASSWORD))
            {
                data.ResponseMessage = "Confirm Password is required";
                data.StatusCode = "101";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            try
            {
                var user = new USER();
                var dbFactory = new DbFactory();
                using (var ctx = dbFactory.Init())
                {
                    //Encrypt password stored on the database
                    var pword = KeyManager.Encryption(model.PASSWORD, System.Configuration.ConfigurationManager.AppSettings["secretKey"].ToString());
                    //Check if user exists
                    user = ctx.USERS.Where(x => x.USERNAME == model.USERNAME && x.PASSWORD == pword).FirstOrDefault();
                    if (user != null)
                    {
                        if (!user.ISACTIVE)
                        {
                            data.ResponseMessage = "User is deactivated";
                            data.StatusCode = "102";
                            return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        }
                        USER_TOKEN token = GenerateNewTokenForUser(user.USER_ID);
                        ctx.USER_TOKENS.Add(token);
                        user.TOKEN = token.TOKEN;
                        await ctx.SaveChangesAsync();
                    }
                    else
                    {

                        data.ResponseMessage = "Authentication Failed";
                        data.StatusCode = "101";
                        return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                }
                data.Data = new USER();
                data.Data = user;
                data.ResponseMessage = "Success";
                data.StatusCode = "100";
                var serializedObject = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return serializedObject;
            }
            catch (Exception ex)
            {
                data.ResponseMessage = ex.Message;
                data.StatusCode = "103";
                return JsonConvert.SerializeObject(ex.Message, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }
        [AcceptVerbs("POST")]
        [Route("createUser")]
        public async Task<string> CreateUser([FromBody] USER model)
        {
            JSONViewModel data = new JSONViewModel();
            if (string.IsNullOrEmpty(model.USERNAME))
            {
                data.ResponseMessage = "Username is required";
                data.StatusCode = "101";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            if (string.IsNullOrEmpty(model.CONFIRM_PASSWORD))
            {
                data.ResponseMessage = "Confirm Password is required";
                data.StatusCode = "101";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            if (string.IsNullOrEmpty(model.PASSWORD))
            {
                data.ResponseMessage = "Password is required";
                data.StatusCode = "101";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            else
            {
                if (model.PASSWORD != model.CONFIRM_PASSWORD)
                {
                    data.ResponseMessage = "Passwords do not matched";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
            }
            try
            {
                var dbfactory = new DbFactory();
                using (var ctx = dbfactory.Init())
                {
                    //check if username exists
                    var existingUser = await Task.Run(() => ctx.USERS.Where(x => x.USERNAME == model.USERNAME.Trim()).FirstOrDefault());
                    if (existingUser == null)
                    {
                        //Encrypt password to be stored on the database
                        string pword = KeyManager.Encryption(model.PASSWORD, System.Configuration.ConfigurationManager.AppSettings["secretKey"].ToString());
                        USER user = new USER
                        {
                            USERNAME = model.USERNAME,
                            ISACTIVE = true,
                            PASSWORD = pword
                        };
                        ctx.USERS.Add(user);
                        await ctx.SaveChangesAsync();
                    }
                    else
                    {
                        data.ResponseMessage = "Username already exists";
                        data.StatusCode = "102";
                        return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    }

                }
                data.ResponseMessage = "Success";
                data.StatusCode = "100";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                data.ResponseMessage = ex.Message;
                data.StatusCode = "103";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }
    }
}
