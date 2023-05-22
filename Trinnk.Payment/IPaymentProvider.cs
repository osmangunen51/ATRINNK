using Trinnk.Payment.Requests;
using Trinnk.Payment.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Trinnk.Payment
{
    public interface IPaymentProvider
    {
        Task<PaymentGatewayResult> ThreeDGatewayRequest(PaymentGatewayRequest request);
        Task<VerifyGatewayResult> VerifyGateway(VerifyGatewayRequest request, PaymentGatewayRequest gatewayRequest, FormCollection form);
        Task<CancelPaymentResult> CancelRequest(CancelPaymentRequest request);
        Task<RefundPaymentResult> RefundRequest(RefundPaymentRequest request);
        Task<PaymentDetailResult> PaymentDetailRequest(PaymentDetailRequest request);
        Dictionary<string, string> TestParameters { get; }
    }
}