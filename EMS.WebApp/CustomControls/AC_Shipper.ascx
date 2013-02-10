<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AC_Shipper.ascx.cs" Inherits="EMS.WebApp.CustomControls.AC_Shipper" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../CustomControls/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .watermark1
    {
        text-transform: uppercase;
        color: #747862;
        height: 20px;
        border: 0.5;
        padding: 1px 1px;
        margin-bottom: 0px;
    }
</style>

<div>
    <asp:TextBox runat="server" ID="txtShipper" autocomplete="off" 
        CssClass="textboxuppercase" MaxLength="250" Width="450px"
        AutoPostBack="true" ontextchanged="txtShipper_TextChanged" />
    <cc1:textboxwatermarkextender id="txtWMEName6" runat="server" targetcontrolid="txtShipper"
        watermarktext="TYPE SHIPPER" watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>
 
    <cc1:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtShipper" servicepath="AutoComplete.asmx" servicemethod="GetShipper"
        minimumprefixlength="1" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";, :"
        showonlycurrentwordincompletionlistitem="true">
    </cc1:autocompleteextender>
 
</div>