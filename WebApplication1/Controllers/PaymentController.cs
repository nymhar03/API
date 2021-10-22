using Newtonsoft.Json;
using Project.Data.Infrastructure;
using Project.Model;
using Project.WebApi.Models;
using Project.WebApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Project.WebApi.Controllers
{
    [RoutePrefix("api/Payments")]
    public class PaymentsController : BaseController
    {
        [AcceptVerbs("POST")]
        [Route("create")]
        public async Task<string> CreatePayment([FromBody] PAYMENT model)
        {
            JSONViewModel data = new JSONViewModel();
            //Validate User ID and Token coming from Authentication/Login to avoid multiple logins
            //Token has also expiration date. If Token value expired, user may require to login again to generate another token
            if (ValidateToken(model.USER_ID, model.TOKEN))
            {
                //Account No. is required
                if (string.IsNullOrEmpty(model.ACCOUNT_NO))
                {
                    data.ResponseMessage = "Account No. is required";
                    data.StatusCode = "101";
                    return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }
                //Amount must be greater than zero(0)
                if (model.AMOUNT <= 0)
                {
                    data.ResponseMessage = "Amount must be greater than zero(0)";
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
                        var existingAccount = await Task.Run(() => ctx.ACCOUNTS.Where(x => x.ACCOUNT_NO == model.ACCOUNT_NO.Trim()).FirstOrDefault());
                        if (existingAccount != null)
                        {
                            //Check Balance if sufficient
                            if (model.AMOUNT > existingAccount.ACCOUNT_BALANCE)
                            {
                                data.ResponseMessage = "Insufficient Balance.";
                                data.StatusCode = "102";
                                return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
                            }
                            else
                            {
                                PAYMENT payments = new PAYMENT
                                {
                                    ACCOUNT_NO = model.ACCOUNT_NO,
                                    AMOUNT = model.AMOUNT,
                                    REMARKS = model.REMARKS,
                                };
                                existingAccount.ACCOUNT_BALANCE -= model.AMOUNT;
                                ctx.PAYMENTS.Add(payments);
                                await ctx.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            data.ResponseMessage = "Account No. is invalid";
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
        [Route("getByAccountNo")]
        public async Task<string> GetPaymentsByAccountNo([FromBody] SearchViewModel model)
        {
            JSONViewModel data = new JSONViewModel();
            //Validate User ID and Token coming from Authentication/Login to avoid multiple logins
            //Token has also expiration date. If Token value expired, user may require to login again to generate another token
            if (ValidateToken(model.USER_ID, model.TOKEN))
            {
                try
                {
                    var acct = new List<PAYMENT>();
                    var dbFactory = new DbFactory();
                    using (var ctx = dbFactory.Init())
                    {
                        //check if valid Account No.
                        acct = await Task.Run(() => ctx.PAYMENTS.Where(x => x.ACCOUNT_NO == model.ACCOUNT_NO).OrderByDescending(x=>x.DATE).ToList());
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
