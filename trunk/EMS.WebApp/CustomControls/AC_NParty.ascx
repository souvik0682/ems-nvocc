<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AC_NParty.ascx.cs" Inherits="EMS.WebApp.CustomControls.AC_NParty" %>

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
    <asp:TextBox runat="server" ID="txtNParty" autocomplete="off" CssClass="textboxuppercase"
        ForeColor="#747862" MaxLength="250" Width="450px" 
        AutoPostBack="true" ontextchanged="txtNParty_TextChanged" />
    <%--<cc1:textboxwatermarkextender id="txtWMEName6" runat="server" targetcontrolid="txtNParty"
        watermarktext="TYPE NOTIFYING PARTY" watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>--%>
 
    <cc1:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtNParty" servicepath="AutoComplete.asmx" servicemethod="GetNParty"
        minimumprefixlength="1" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";,:"
        showonlycurrentwordincompletionlistitem="true">
    </cc1:autocompleteextender>
 
</div>