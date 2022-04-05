using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public static class ProductExtensions
    {
        //test yorum saıtır
        public static string GetProductTypeText(this Product product)
        {
            if (string.IsNullOrEmpty(product.ProductType))
                return string.Empty;
            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            var constant = _constantService.GetConstantByConstantId(short.Parse(product.ProductType));
            if (constant != null)
                return constant.ConstantName;
            return string.Empty;
        }

        public static string GetProductStatuText(this Product product)
        {
            if (string.IsNullOrEmpty(product.ProductStatu))
                return string.Empty;
            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            var constant = _constantService.GetConstantByConstantId(short.Parse(product.ProductStatu));
            if (constant != null)
                return constant.ConstantName;
            return string.Empty;
        }

        public static string GetBriefDetailText(this Product product)
        {
            if (string.IsNullOrEmpty(product.BriefDetail) || product.BriefDetail == ",")
                return string.Empty;
            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            List<string> productBriefDetails = product.BriefDetail.Split(',').ToList();
            productBriefDetails.RemoveAll(s => s == string.Empty);
            List<Constant> constants = _constantService.GetConstantsByConstantIds(productBriefDetails.ConvertAll(s => Int16.Parse(s))).ToList();
            if (constants.Count > 0)
            {
                string result = string.Empty;
                foreach (var item in constants)
                {
                    result += item.ConstantName + ", ";
                }
                result = result.Substring(0, result.Length - 2);
                return result;
            }
            return string.Empty;
        }

        public static string GetProductSalesTypeText(this Product product)
        {
            if (string.IsNullOrEmpty(product.ProductSalesType) || product.ProductSalesType == ",")
                return string.Empty;
            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            List<string> productSalesTypes = product.ProductSalesType.Split(',').ToList();
            productSalesTypes.RemoveAll(s => s == string.Empty);
            List<Constant> constants = _constantService.GetConstantsByConstantIds(productSalesTypes.ConvertAll(s => Int16.Parse(s))).ToList();
            if (constants.Count > 0)
            {
                string result = string.Empty;
                foreach (var item in constants)
                {
                    result += item.ConstantName + ", ";
                }
                result = result.Substring(0, result.Length - 2);
                return result;
            }
            return string.Empty;
        }

        public static string GetMenseiText(this Product product)
        {
            if (!product.MenseiId.HasValue)
                return string.Empty;

            if (product.MenseiId.Value <= 0)
                return string.Empty;

            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            var constant = _constantService.GetConstantByConstantId((short)product.MenseiId.Value);
            if (constant != null)
                return constant.ConstantName;
            return string.Empty;
        }

        public static string GetOrderStatusText(this Product product)
        {
            if (!product.OrderStatus.HasValue)
                return string.Empty;

            if (product.OrderStatus.Value <= 0)
                return string.Empty;

            IConstantService _constantService = EngineContext.Current.Resolve<IConstantService>();
            var constant = _constantService.GetConstantByConstantId((short)product.OrderStatus.Value);
            if (constant != null)
                return constant.ConstantName;
            return string.Empty;
        }


    }
}
