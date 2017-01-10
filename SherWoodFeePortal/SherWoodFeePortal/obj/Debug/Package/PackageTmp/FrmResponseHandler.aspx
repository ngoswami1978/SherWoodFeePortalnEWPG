<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmResponseHandler.aspx.cs" Inherits="SherWoodFeePortal.FrmResponseHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function noBack() {
            window.history.forward()
        }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
        window.onunload = function () { void (0); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="showtext">
                    
    </div>
    <div>    
    </div>
    </form>
</body>
</html>
