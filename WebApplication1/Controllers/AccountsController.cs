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
    [RoutePrefix("api/Accounts")]
    public class AccountsController : BaseController
    {
        [AcceptVerbs("POST")]
        [Route("create")]
        public async Task<string> CreateAccount([FromBody] ACCOUNT model)
        {
            JSONViewModel data = new JSONViewModel();
            //Validate User ID and Token coming from Authentication/Login to avoid multiple logins
            //Token has also expiration date. If Token value expired, user may require to login again to generate another token
            if (ValidateToken(model.USER_ID, model.TOKEN))
            {
                if (string.IsNullOrEmpty(model.ACCOUNT_NO))
                {
                    data.ResponseMessage = "Account No. is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if (string.IsNullOrEmpty(model.FIRSTNAME))
                {
                    data.ResponseMessage = "First Name is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if (string.IsNullOrEmpty(model.LASTNAME))
                {
                    data.ResponseMessage = "Last Name is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if ((model.ACCOUNT_BALANCE <= 0))
                {
                    data.ResponseMessage = "Account Balance must be greater than zero(0)";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                try
                {
                    var dbfactory = new DbFactory();
                    using (var ctx = dbfactory.Init())
                    {
                        //check if valid Account No.
                        var existingUser = await Task.Run(() => ctx.ACCOUNTS.Where(x => x.ACCOUNT_NO == model.ACCOUNT_NO).FirstOrDefault());
                        if (existingUser == null)
                        {
                            ACCOUNT acct = new ACCOUNT
                            {
                                ACCOUNT_NO = model.ACCOUNT_NO,
                                USER_ID = model.USER_ID,
                                STATUS = "ACTIVE",
                                FIRSTNAME = model.FIRSTNAME,
                                LASTNAME = model.LASTNAME,
                                MIDDLENAME = model.MIDDLENAME,
                                ACCOUNT_BALANCE = model.ACCOUNT_BALANCE,
                                REMARKS = model.REMARKS
                            };
                            ctx.ACCOUNTS.Add(acct);
                            await ctx.SaveChangesAsync();
                        }
                        else
                        {
                            data.ResponseMessage = "Account Number is already exists";
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
            else
            {
                data.ResponseMessage = "Unauthorized access! Invalid user id or token.";
                data.StatusCode = "104";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
        }

        [AcceptVerbs("POST")]
        [Route("update")]
        public async Task<string> UpdateAccount([FromBody] ACCOUNT model)
        {
            JSONViewModel data = new JSONViewModel();
            //Validate User ID and Token coming from Authentication/Login to avoid multiple logins
            //Token has also expiration date. If Token value expired, user may require to login again to generate another token
            if (ValidateToken(model.USER_ID, model.TOKEN))
            {
                if (string.IsNullOrEmpty(model.ACCOUNT_NO))
                {
                    data.ResponseMessage = "Account No. is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if (string.IsNullOrEmpty(model.FIRSTNAME))
                {
                    data.ResponseMessage = "First Name is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if (string.IsNullOrEmpty(model.LASTNAME))
                {
                    data.ResponseMessage = "Last Name is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                else if ((model.STATUS.ToUpper() == "CLOSED"))
                {
                    if (string.IsNullOrEmpty(model.REMARKS))
                    {
                        data.ResponseMessage = "Site a reason in remarks why status of the account is closed.";
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
                        //check if valid Account No.
                        var existingUser = await Task.Run(() => ctx.ACCOUNTS.Where(x => x.ACCOUNT_NO == model.ACCOUNT_NO).FirstOrDefault());
                        if (existingUser != null)
                        {
                            existingUser.ACCOUNT_NO = model.ACCOUNT_NO;
                            existingUser.USER_ID = model.USER_ID;
                            existingUser.STATUS = model.STATUS;
                            existingUser.FIRSTNAME = model.FIRSTNAME;
                            existingUser.LASTNAME = model.LASTNAME;
                            existingUser.MIDDLENAME = model.MIDDLENAME;
                            existingUser.REMARKS = model.REMARKS;
                            await ctx.SaveChangesAsync();
                        }
                        else
                        {
                            data.ResponseMessage = "Invalid Account Number";
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
            else
            {
                data.ResponseMessage = "Unauthorized access! Invalid user id or token.";
                data.StatusCode = "104";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
        }

        [AcceptVerbs("POST")]
        [Route("get")]
        public async Task<string> GetAccount([FromBody] SearchViewModel model)
        {
            JSONViewModel data = new JSONViewModel();
            //Validate User ID and Token coming from Authentication/Login to avoid multiple logins
            //Token has also expiration date. If Token value expired, user may require to login again to generate another token
            if (ValidateToken(model.USER_ID, model.TOKEN))
            {
                try
                {
                    var acct = new ACCOUNT();
                    var dbFactory = new DbFactory();
                    using (var ctx = dbFactory.Init())
                    {
                        //check if valid Account No.
                        acct = await Task.Run(() => ctx.ACCOUNTS.Where(x => x.USER_ID == model.USER_ID && x.ACCOUNT_NO == model.ACCOUNT_NO).FirstOrDefault());
                        if (acct == null)
                        {
                            data.ResponseMessage = "Invalid Account Number";
                            data.StatusCode = "101";
                            return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        }

                        data.Data = acct;
                        data.ResponseMessage = "Success";
                        data.StatusCode = "100";
                    }
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
            else
            {
                data.ResponseMessage = "Unauthorized access! Invalid user id or token.";
                data.StatusCode = "104";
                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
        }

    }
}
