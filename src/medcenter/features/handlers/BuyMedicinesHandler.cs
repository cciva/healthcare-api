using System.Threading;
using System.Threading.Tasks;
using Contracts;
using RestSharp;
using MediatR;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace MedCenter
{
    // V2 improvements :
    // added request handler that will deal with cross service calls - in this case
    // we are calling another REST api, in order to purchase medicaments needed for
    // patient's therapy
    // we will be using RestSharp for communication
    public class BuyMedicinesHandler : IRequestHandler<BuyMedicinesCommand, PharmacyInvoice>
    {
        private IRestClient _pharmacyApi = null;

        public BuyMedicinesHandler(IConfiguration conf)
        {
            _pharmacyApi = new RestClient(conf.GetSection("Pharmacy").GetValue<string>("url"));
        }

        public Task<PharmacyInvoice> Handle(BuyMedicinesCommand cmd, CancellationToken cancel)
        {
            var tcs = new TaskCompletionSource<PharmacyInvoice>();
            IRestRequest rq = new RestRequest(string.Format("shop/buy/{0}", cmd.TherapyId), Method.POST);

            _pharmacyApi.ExecuteAsync<PharmacyShopRs>(rq, cancel)
                .ContinueWith(tr => {
                    IRestResponse rs = tr.Result;
                    if(rs.ErrorException != null)
                        tcs.TrySetException(rs.ErrorException);
                    else
                    {
                        PharmacyShopRs shopRs = JsonConvert.DeserializeObject<PharmacyShopRs>(rs.Content);
                        tcs.TrySetResult(shopRs.Invoice);
                    }                        
                });

            return tcs.Task;
        }
    }
}