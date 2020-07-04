using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.Seo;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class SeoDefinitionController : BaseController
    {
        public ActionResult Edit(int entityId, int entityTypeId)
        {
            var model = new SeoDefinitionModel();
            var seoDefinition = entities.SeoDefinitions.FirstOrDefault(sd => sd.EntityId == entityId && sd.EntityTypeId == entityTypeId);
            if (seoDefinition != null)
            {
                model.SeoDefinitionId = seoDefinition.SeoDefinitionId;
                model.EntityId = seoDefinition.EntityId;
                model.EntityTypeId = seoDefinition.EntityTypeId;
                model.Description = seoDefinition.Description;
                model.Enabled = seoDefinition.Enabled;
                model.SeoContent = seoDefinition.SeoContent;
                model.Title = seoDefinition.Title;
                using (ClientProcessor Analyzer=new ClientProcessor())
                {
                    //model.Keywords=Analyzer.GetUrlAnalizWithTxt(seoDefinition.SeoContent);
                    var categoryKeyword = entities.Categories.FirstOrDefault(c => c.CategoryId == entityId);
                    string Url = "";
                    if (entityTypeId == (byte)SeoDefinitionType.Category) {
                        Url = UrlBuilder.GetCategoryUrl(categoryKeyword.CategoryId, categoryKeyword.CategoryName, null, null);
                    }
                    else
                    {
                        string storePageTitle = "";
                        if (!string.IsNullOrEmpty(categoryKeyword.StorePageTitle))
                        {
                            if (categoryKeyword.StorePageTitle.Contains("Firma"))
                            {
                                storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryKeyword.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                            }
                            else
                            {
                                storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryKeyword.StorePageTitle, CategorySyntaxType.Store);

                            }
                        }
                        else if (!string.IsNullOrEmpty(categoryKeyword.CategoryContentTitle))
                            storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryKeyword.CategoryContentTitle, CategorySyntaxType.Store);
                        else
                            storePageTitle = FormatHelper.GetCategoryNameWithSynTax(categoryKeyword.CategoryName, CategorySyntaxType.Store);
                        Url = UrlBuilder.GetStoreCategoryUrl(categoryKeyword.CategoryId, storePageTitle);
                    }
#if DEBUG
                    Url = "https://www.makinaturkiye.com" + Url;
#endif
                    model.KeywordAnalysis = Analyzer.GetUrlAnalizWithUrl(Url);
                }
            }
            else
            {
                model.EntityTypeId = entityTypeId;
                model.SeoContent = string.Empty;
                model.Enabled = true;

            }

            //category Name
            var category = entities.Categories.FirstOrDefault(c => c.CategoryId == entityId);
            if (category != null)
            {
                model.CategoryName = category.CategoryName;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SeoDefinitionModel model)
        {
            try
            {

                if (model.SeoDefinitionId == 0)
                {
                    var seoDefinition = new SeoDefinition();
                    seoDefinition.Description = model.Description;
                    if (model.SeoContent != null)
                    {
                        seoDefinition.SeoContent = model.SeoContent;
                    }
                    else
                        seoDefinition.SeoContent = "";
                    seoDefinition.Title = model.Title;
                    seoDefinition.CreatedDate = DateTime.Now;
                    seoDefinition.UpdatedDate = DateTime.Now;
                    seoDefinition.Enabled = model.Enabled;
                    seoDefinition.EntityId = model.EntityId;
                    seoDefinition.EntityTypeId = model.EntityTypeId;
                
                    entities.SeoDefinitions.AddObject(seoDefinition);
                }
                else
                {
                    var seoDefinition = entities.SeoDefinitions.FirstOrDefault(sd => sd.SeoDefinitionId == model.SeoDefinitionId);
                    if (seoDefinition != null)
                    {
                        if (string.IsNullOrEmpty(model.SeoContent))
                        {
                            entities.SeoDefinitions.DeleteObject(seoDefinition);
                        }
                        else
                        {
                                
                            seoDefinition.SeoContent = model.SeoContent;
                            seoDefinition.UpdatedDate = DateTime.Now;
                        }
                    }
                }
                entities.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Edit", new { entityId = model.EntityId, entityTypeId = model.EntityTypeId });
        }

        public ActionResult StoreCategoryEdit(int entityId, int entityTypeId)
        {

            return View();
        }
    }
}
