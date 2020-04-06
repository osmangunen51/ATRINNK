﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
<style type="text/css">
  .small_input
  {
    width: 120px;
    font-size: 12px;
    padding-left: 5px;
  }
  .big_input
  {
    width: 180px;
    font-size: 12px;
    padding-left: 5px;
  }
  .btnSave
  {
    background-image: url('/Content/Images/btnSave.gif');
    width: 70px;
    height: 24px;
    border: none;
    cursor: pointer;
  }
  .btnAdd
  {
    background-image: url('/Content/Images/btnAdd.gif');
    width: 55px;
    height: 24px;
    border: none;
    cursor: pointer;
  }
  .fileUpload
  {
    font-size: 12px;
    width: 180px;
    height: 20px;
    border: solid 1px #bababa;
  }
  .profileBg
  {
    width: 967px;
    height: auto;
    background-image: url('/Content/Images/profileBg.gif');
    border: solid 1px #cbdfed;
    float: left;
    background-repeat: repeat-x;
    padding-bottom: 15px;
  }
  .profileStartLink
  {
    color: #727679;
    text-decoration: underline;
    font-size: 12px;
  }
  .profileStartLink:hover
  {
    color: #727679;
    font-size: 12px;
    text-decoration: underline;
  }
  .profileStartLink:visited
  {
    color: #727679;
    text-decoration: underline;
    font-size: 12px;
  }
  .speedStep
  {
    width: 900px;
    height: 100px;
    margin-left: 33px;
    margin-top: 60px;
  }
  .speedStepContent
  {
    width: auto;
    height: auto;
    float: left;
    margin-left: 250px;
    font-size: 12px;
    font-weight: bold;
    text-align: center;
  }
  .accordionHeader
  {
    background-image: url('/Content/Images/profilePanelBg.gif');
    border-bottom: solid 1px #aebecd;
  }
  .dropdownBig
  {
    width: 216px;
    height: 20px;
    border: solid 1px #bababa;
  }
  .dropdownBig select
  {
    width: 216px;
    height: 20px;
  }
  
  .textBig
  {
    width: 204px;
    height: 18px;
    border: solid 1px #bababa;
    padding-left: 12px;
    background-color: #fff;
    padding-top: 2px;
  }
  .textBig input
  {
    width: 190px;
    background-color: transparent;
    border: none;
  }
  
  .textBigArea
  {
    width: 204px;
    height: 58px;
    border: solid 1px #bababa;
    padding-left: 12px;
    background-color: #fff;
    padding-top: 4px;
  }
  .textBigArea textarea
  {
    width: 200px;
    height: 50px;
    background-color: transparent;
    border: none;
  }
  
  .textMedium
  {
    width: 64px;
    height: 18px;
    border: solid 1px #bababa;
    padding-left: 12px;
    background-color: #fff;
    padding-top: 2px;
    float: left;
  }
  .textMedium input
  {
    width: 50px;
    background-color: transparent;
    border: none;
  }
  
  .textSmall
  {
    width: 30px;
    height: 18px;
    border: solid 1px #bababa;
    padding-left: 2px;
    background-color: #fff;
    padding-top: 2px;
    float: left;
  }
  .textSmall input
  {
    width: 24px;
    background-color: transparent;
    border: none;
  }
  #wrapper
  {
    width: 800px;
    margin-left: auto;
    margin-right: auto;
  }
  
  .accordionButton
  {
    width: 780px;
    float: left;
    border: 1px solid #d3d3d3;
    cursor: pointer;
    height: 25px;
    color: #000;
    font-size: 13px;
    padding-left: 20px;
    padding-top: 5px;
  }
  
  .accordionContent
  {
    width: 800px;
    float: left;
    display: none;
    border: 1px solid #d3d3d3;
    background-color: #ccc;
  }
  .divTab
  {
    height: 21px;
    float: left;
    margin: 0px 0px 0px 2px;
    padding: 4px 10px 0px 10px;
    cursor: pointer;
  }
  .divTabActive
  {
    height: 21px;
    float: left;
    margin: 0px 0px 0px 2px;
    padding: 4px 10px 0px 10px;
    cursor: pointer;
    border-bottom: none;
  }
  .dropdownAddress
  {
    width: auto;
    height: 21px;
    margin-top: 3px;
    float: left;
    border: #c9e6e2 1px solid;
    border-top: #c9e6e2 2px solid;
    margin-left: 4px;
  }
</style>
