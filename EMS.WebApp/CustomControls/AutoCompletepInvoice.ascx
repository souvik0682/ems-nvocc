<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompletepInvoice.ascx.cs"
    Inherits="EMS.WebApp.CustomControls.AutoCompletePort" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../CustomControls/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .watermark1
    {
        text-transform: uppercase;
        color: #747862;
        height: 20px;
        border: 0.5px;
        padding: 1px 1px;
        margin-bottom: 0px;
    }
</style>
<%-- OnTextChanged="txtInvoice_TextChanged"--%>
<div>
    <asp:TextBox runat="server" ID="txtInvoice" autocomplete="off" CssClass="watermark1" ForeColor="#747862"
        MaxLength="20" Width="150px"  BorderWidth="1px"   AutoPostBack="true"  />
    <cc1:textboxwatermarkextender id="txtWMEName7" runat="server" targetcontrolid="txtInvoice"
        watermarktext="TYPE INVOICE NO." watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>
 
    <cc1:autocompleteextender runat="server"  ID="AutoInvoice"
        targetcontrolid="txtInvoice" servicepath="AutoComplete.asmx" servicemethod="GetInvoiceNo"
        minimumprefixlength="1" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";,:"
        showonlycurrentwordincompletionlistitem="true">
    </cc1:autocompleteextender>
 
</div>