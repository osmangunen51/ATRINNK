﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Common.MetaTagModel>" %>
<meta charset="utf-8" />
<title><%:Html.Raw(Model.Title)%></title>
<meta http-equiv="x-dns-prefetch-control" content="on" />
<meta name="description" content="<%= Model.Description %>" /><%--<meta name="keywords" content="<%= Model.Keywords %>" />--%>
<meta name="robots" content="<%= Model.Robots %>" />
<meta title="title" content="<%=Model.Title %>" />  <!-- Social: Facebook / Open Graph -->
<meta name="og:description" content="<%=Model.Description %>" />
<meta name="og:title" content="<%=Model.Title %>" />
<meta name="og:url" content="<%=Model.Url%>" />
<meta name="og:image" content="<%=Model.Image%>" />
<meta property="og:type" content="website"/>
<meta property="article:author" content="https://www.facebook.com/makinaturkiyecom">
<meta property="article:publisher" content="https://www.facebook.com/makinaturkiyecom"><!-- Social: Twitter -->
<meta name="twitter:card" content="summary_large_image">
<meta name="twitter:site" content="@MakinaTurkiye">
<meta name="twitter:creator" content="Makina Türkiye">
<meta name="twitter:title" content="<%:Model.Title %>">
<meta http-equiv="X-UA-Compatible" content="IE=10;IE=9; IE=8; IE=7; IE=EDGE" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="google-site-verification" content="jpeiLIXc-vAKBB2vjRZg3PluGG3hsty0n6vSXUr_C-A" />
<meta name="y_key" content="edbb7e17ada4b80f" />
<meta name="alexaVerifyID" content="u8sVDfy1ch6DMlQb9Mwm7Ts0Hik" />
<meta name="p:domain_verify" content="1c2be74015d798364692eae5aaa17c32"/>
<meta name = "yandex-doğrulama" content = "0585f5b37b494a5f" />