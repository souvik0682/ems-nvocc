<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompletepPort.ascx.cs"    Inherits="EMS.WebApp.CustomControls.AutoCompletePort" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
    <asp:TextBox runat="server" ID="txtPort" Width="98%" autocomplete="off" />
    <cc2:textboxwatermarkextender id="txtWMEName1" runat="server" targetcontrolid="txtPort"
        watermarktext="Type Port" watermarkcssclass="watermark1"></cc2:textboxwatermarkextender>
 
    <cc2:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtPort" servicepath="AutoComplete.asmx" servicemethod="GetPortList"
        minimumprefixlength="2" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";, :"
        showonlycurrentwordincompletionlistitem="true">
       <%-- <Animations>
                    <OnShow>
                        <Sequence>
                           
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find(AutoPort;);
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            
                            <Parallel Duration=".4">
                                <FadeIn />--%>
                         <%--       <%--<Length PropertyKey="height" StartValue="0" EndValueScript="$get('<%=RandNo %>').._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                      
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('<%=RandNo %>')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
        </Animations>--%>
    </cc2:autocompleteextender>
 
</div>
<%--<script type="text/javascript">
    function GetID() {
        return '<%=RandNo %>';
    }
    function GetEID() {
        var ed = '<%=RandNo %>';
        return ed;
    }
    var rid = GetID();
    alert(Math.random());
</script>--%>