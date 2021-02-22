using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Optimization;
//using System.Web.Optimization.React;

namespace NeoSistem.MakinaTurkiye.Web.App_Start
{
	public class BundleConfig
	{

		public static void RegisterBundles(BundleCollection bundles)
		{

			var bundle = new ScriptBundle("~/js").Include(
					"~/Content/V2/assets/js/jquery.min.js",
					"~/Content/V2/assets/js/jquery.autocomplete.js",
					"~/Content/v2/assets/js/bootstrap.min.js",
					"~/Content/v2/assets/jquery-ui-1.12.1.custom/jquery-ui.min.js",
					"~/Content/v2/assets/js/jquery.validationEngine.js",
					"~/Content/v2/assets/js/jquery.validationEngine-tr.js",
					"~/Content/v2/assets/js/jquery.unobtrusive-ajax.min.js",
					"~/Content/v2/assets/js/jquery.zoom.min.js",
					"~/Content/v2/assets/js/ResizeSensor.js",
					"~/Content/v2/assets/js/jquery.sticky-sidebar.js",
					"~/Content/v2/assets/js/CKEditor/ckeditor.js",
					"~/Content/v2/assets/js/owl.carousel.js",
					"~/Content/v2/assets/js/summernote.min.js",
					"~/Content/v2/assets/js/jquery.autocomplete.js",
					"~/Content/v2/assets/js/bootbox.min.js",
					"~/Content/v2/assets/js/jquery.mask.min.js",
					"~/Content/v2/assets/js/lightbox.min.js",
					"~/Content/v2/assets/js/bootstrapValidator.js",
					"~/Content/jQuery-Cookie-Disclaimer/dist/jquery-cookie/jquery.cookie.js",
					"~/Content/jQuery-Cookie-Disclaimer/dist/jquery.cookieDisclaimer.js",
					"~/Content/v2/assets/js/FaceBoxMessage/facebox.js",
					"~/Content/FlvPlayer/flowplayer-3.2.4.js",
					"~/Content/v2/assets/js/bootstrap-validate.js",
					"~/Content/v2/assets/js/bootstrap-tagsinput.min.js",
					"~/Content/v2/assets/js/JQuery-dropdowncascading.js",
					"~/Content/v2/assets/js/JQuery-Post-dropdowncascading.js",
					"~/Content/v2/assets/js/jquery.maskMoney.js",
					"~/Content/v2/assets/js/jquery.metadata.js",
					"~/Content/v2/assets/js/jquery.validate.min.js",
					"~/Content/v2/assets/js/jquery.magnific-popup.min.js",
					"~/Content/v2/assets/js/bootstrap-select.min.js",
					"~/Content/v2/assets/js/site.js",
					"~/Content/v2/assets/js/MtFormsValidate.js",
					"~/Content/v2/assets/js/newmembership.js",
					"~/Content/v2/assets/js/MtAjaxCalls.js",
					"~/Content/v2/assets/js/membership.js"
					//,"~/Content/V2/assets/js/jquery-ui-1.9.2.custom.js"
				);
			bundle.Orderer = new NonOrderingBundleOrderer();
			bundles.Add(bundle);
			var StyleBundle =new StyleBundle("~/css").Include(
				"~/Content/v2/assets/css/bootstrap.css",
				"~/Content/v2/assets/css/font-awesome.css",
				"~/Content/v2/assets/css/flaticon.css",
				"~/Content/v2/assets/css/owl.carousel.css",
				"~/Content/v2/assets/css/makinaturkiye_fonticons.css",
				"~/Content/v2/assets/jquery-ui-1.12.1.custom/jquery-ui.min.css",
				"~/Content/v2/assets/css/validationEngine.jquery.css",
				"~/Content/v2/assets/css/summernote-bs3.css",
				"~/Content/v2/FaceBoxMessage/faceboxstoreconnect.css",
				"~/Content/v2/assets/css/site.css",
				"~/Content/v2/assets/css/style-v2.css",
				"~/Content/v2/assets/css/company-list.css",
				"~/Content/v2/assets/css/bootstrapValidator.css",
				"~/Content/v2/assets/css/product-list.css",
				"~/Content/v2/assets/css/header.css",
				"~/Content/v2/assets/css/fast-access-bar.css",
				"~/Content/v2/assets/css/store-profile-header.css",
				"~/Content/v2/assets/css/lightbox.css",
				"~/Content/v2/assets/css/cookieDisclaimer.css",
				"~/Content/v2/assets/css/bootstrap-tagsinput.css",
				"~/Content/v2/assets/css/bootstrap-datepicker.min.css",
				"~/Content/v2/assets/css/magnific-popup.css",
				"~/Content/v2/assets/css/bootstrap-select.min.css",
				"~/Content/V2/assets/css/autocomplete.css",
				"~/Content/V2/assets/css/jquery.autocomplete.css?v=5"
				);
			bundle.Orderer = new NonOrderingBundleOrderer();
			bundles.Add(StyleBundle);

			bundles.UseCdn = true;
			#if DEBUG
				BundleTable.EnableOptimizations = false;
			#else
				BundleTable.EnableOptimizations = true;
			#endif
		}
	}
}